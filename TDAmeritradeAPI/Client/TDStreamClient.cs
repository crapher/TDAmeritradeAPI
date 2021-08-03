using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TDAmeritradeAPI.Models.API.UserInfoPreferences;
using TDAmeritradeAPI.Models.Streaming;
using TDAmeritradeAPI.Models.Streaming.AccountActivity;
using TDAmeritradeAPI.Models.Streaming.Admin;
using TDAmeritradeAPI.Models.Streaming.Book;
using TDAmeritradeAPI.Models.Streaming.Chart;
using TDAmeritradeAPI.Models.Streaming.LevelOne;
using TDAmeritradeAPI.Models.Streaming.TimeSale;
using TDAmeritradeAPI.Props;
using Utf8Json;
using Utf8Json.Resolvers;
using Websocket.Client;

namespace TDAmeritradeAPI.Client
{
    public class TDStreamClient
    {
        private TDClient _client;

        private UserPrincipals _userPrincipals;
        private UserPrincipals.Account _account;

        private volatile Int32 _requestId = 0;
        private WebsocketClient _ws;

        private ConcurrentQueue<ResponseMessage> _queue;
        private CancellationTokenSource _cancellationToken;
        private ManualResetEvent _queueNewItems;

        public TDStreamClient(TDClient client, string accountId = null)
        {
            if (null == client)
                throw new ArgumentNullException("Client cannot be null");

            SetupClient(client, accountId);
        }

        private void SetupClient(TDClient client, string accountId)
        {
            _client = client;
            _userPrincipals = _client.GetUserPrincipals(new[] { "streamerSubscriptionKeys", "streamerConnectionInfo" }).Result.Data;

            if (null == _userPrincipals)
                throw new NullReferenceException("UserPrincipals cannot be retrieved");

            if (_userPrincipals.accounts.Length == 0)
                throw new ArgumentException("There are no active account for streaming");

            if (_userPrincipals.accounts.Length > 1 && string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("AccountId cannot be null if there is more than one account");

            if (null == accountId && _userPrincipals.accounts.Length == 1)
                _account = _userPrincipals.accounts[0];
            else
            {
                foreach (UserPrincipals.Account account in _userPrincipals.accounts)
                {
                    if (account.accountId == accountId)
                    {
                        _account = account;
                        break;
                    }
                }
            }

            if (null == _account)
                throw new NullReferenceException("Account not found");
        }

        public async Task Start()
        {
            if (null != _ws && _ws.IsRunning)
                return;

            _requestId = 0;

            var uri = new Uri($"wss://{_userPrincipals.streamerInfo.streamerSocketUrl}/ws");
            _ws = new WebsocketClient(uri, () =>
            {
                var ws = new ClientWebSocket();
                ws.Options.Proxy = TDClient.Proxy;
                return ws;
            });

            _ws.ReconnectTimeout = TimeSpan.FromSeconds(30);
            _ws.MessageReceived.Subscribe(MessageReceived);
            _ws.ReconnectionHappened.Subscribe((msg) => OnReconnect?.Invoke(this, new Result() { Code = 0, Message = msg.ToString(), Service = "RECONNECT" }));
            _ws.DisconnectionHappened.Subscribe((msg) => Console.WriteLine($"Disconnected: {msg.Type}"));

            _queue = new ConcurrentQueue<ResponseMessage>();
            _queueNewItems = new ManualResetEvent(false);
            _cancellationToken = new CancellationTokenSource();

            new Task(MessageProcessor, TaskCreationOptions.LongRunning | TaskCreationOptions.DenyChildAttach).Start();
            await _ws.Start();
        }

        public async Task Stop()
        {
            if (!_ws.IsRunning)
                return;

            await _ws.Stop(WebSocketCloseStatus.NormalClosure, "Normal Closure");
            _ws = null;

            _cancellationToken.Cancel();
            _queueNewItems.Set();
        }

        #region Common
        private StreamerSettings.Request GenerateRequest(string service, string command, StreamerSettings.Parameters parameters) =>
            new StreamerSettings.Request
            {
                service = service,
                command = command,
                account = _account.accountId,
                source = _userPrincipals.streamerInfo.appId,
                parameters = parameters
            };

        private void SendRequest(StreamerSettings.Request request)
        {
            request.requestid = _requestId++.ToString();

            var rawRequest = JsonSerializer.Serialize(request, StandardResolver.ExcludeNull);
            var strRequest = Encoding.UTF8.GetString(rawRequest);

            _ws.Send(strRequest);
        }

        private string GetCSVFieldsFromEnumList<T>(T[] fields, T[] requiredFields = null) where T : Enum
        {
            int[] valueList = null;

            if (null == fields)
                valueList = ((T[])Enum.GetValues(typeof(T))).Select(c => Convert.ToInt32(c)).ToArray();
            else
            {
                if (null != requiredFields)
                    foreach (var requiredField in requiredFields)
                        if (!fields.Contains(requiredField))
                            fields.Append(requiredField);

                valueList = Array.ConvertAll(fields, c => Convert.ToInt32(c));
                Array.Sort(valueList);
            }

            return string.Join(",", valueList);
        }

        private T[] GetUpdatedObjects<T>(Dictionary<string, T> objectCache, string jsonObject) where T : IUpdatableBySymbol<T>
        {
            var objectList = JsonSerializer.Deserialize<List<T>>(jsonObject);
            var result = new List<T>();

            foreach (var obj in objectList)
            {
                var key = obj.Symbol;

                if (objectCache.ContainsKey(key))
                    objectCache[key].Update(obj);
                else
                    objectCache[key] = obj;

                result.Add(objectCache[key]);
            }

            return result.ToArray();
        }

        private long MillisecondsFromEpoch(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan tokenEpoch = dateTime - epoch;
            return (long)Math.Floor(tokenEpoch.TotalMilliseconds);
        }
        #endregion

        #region Message Handling
        private void MessageProcessor()
        {
            while (!_cancellationToken.IsCancellationRequested && _queueNewItems.WaitOne())
            {
                while (!_cancellationToken.IsCancellationRequested && _queue.TryDequeue(out ResponseMessage msg))
                {
                    try
                    {
                        var response = JsonSerializer.Deserialize<ResponseData>(msg.Text);

                        if (null != response.Response)
                            HandleResponse(response.Response);
                        else if (null != response.Data)
                            HandleData(response.Data);
                        else if (null != response.Snapshot) { }
                        else if (null != response.Notify) { }
                        else
                            Console.WriteLine($"UNKNOWN message received: {msg.Text}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception [{ex.Message}] Processing Message: {msg.Text}");
                    }
                }

                _queueNewItems.Reset();
            }
        }

        private void MessageReceived(ResponseMessage msg)
        {
            _queue.Enqueue(msg);
            _queueNewItems.Set();
        }

        private void HandleResponse(List<Response> responseList)
        {
            foreach (var response in responseList)
            {
                try
                {
                    var result = JsonSerializer.Deserialize<Result>(response.Content);
                    result.Service = response.Service;

                    switch (response.Command)
                    {
                        case "LOGIN": OnLogin?.Invoke(this, result); break;
                        case "STREAM": OnStream?.Invoke(this, result); break;
                        case "QOS": OnQOS?.Invoke(this, result); break;
                        case "SUBS": OnSubscribe?.Invoke(this, result); break;
                        case "ADD": OnAdd?.Invoke(this, result); break;
                        case "UNSUBS": OnUnsubscribe?.Invoke(this, result); break;
                        case "VIEW": OnView?.Invoke(this, result); break;
                        case "LOGOUT": OnLogout?.Invoke(this, result); break;
                        default: Console.WriteLine($"UNKNOWN Response: {response}"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception [{ex.Message}] Processing Message: {response.Content}");
                }
            }
        }

        private void HandleData(List<Data> dataList)
        {
            foreach (var data in dataList)
            {
                try
                {
                    switch (data.Service)
                    {
                        // Account Activity
                        case "ACCT_ACTIVITY": OnAccountActivity?.Invoke(this, JsonSerializer.Deserialize<AccountActivity[]>(data.Content)); break;
                        // Level 1
                        case "QUOTE": OnLevelOneQuotes?.Invoke(this, GetUpdatedObjects(levelOneQuotesCache, data.Content)); break;
                        case "OPTION": OnLevelOneOptions?.Invoke(this, GetUpdatedObjects(levelOneOptionsCache, data.Content)); break;
                        case "LEVELONE_FUTURES": OnLevelOneFutures?.Invoke(this, GetUpdatedObjects(levelOneFuturesCache, data.Content)); break;
                        case "LEVELONE_FOREX": OnLevelOneForex?.Invoke(this, GetUpdatedObjects(levelOneForexCache, data.Content)); break;
                        case "LEVELONE_FUTURES_OPTIONS": OnLevelOneFuturesOptions?.Invoke(this, GetUpdatedObjects(levelOneFuturesOptionsCache, data.Content)); break;
                        // Chart
                        case "CHART_EQUITY": OnChartEquity?.Invoke(this, JsonSerializer.Deserialize<ChartEquity[]>(data.Content)); break;
                        case "CHART_FUTURES": OnChartFutures?.Invoke(this, JsonSerializer.Deserialize<ChartFutures[]>(data.Content)); break;
                        case "CHART_OPTIONS": OnChartOptions?.Invoke(this, JsonSerializer.Deserialize<ChartOptions[]>(data.Content)); break;
                        // Book
                        case "FUTURES_BOOK": OnFuturesBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        case "FOREX_BOOK": OnForexBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        case "FUTURES_OPTIONS_BOOK": OnFuturesOptionsBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        case "LISTED_BOOK": OnListedBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        case "NASDAQ_BOOK": OnNasdaqBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        case "OPTIONS_BOOK": OnOptionsBook?.Invoke(this, JsonSerializer.Deserialize<Book[]>(data.Content)); break;
                        // Time & Sales
                        case "TIMESALE_EQUITY": OnTimeSaleEquity?.Invoke(this, JsonSerializer.Deserialize<TimeSale[]>(data.Content)); break;
                        case "TIMESALE_FOREX": OnTimeSaleForex?.Invoke(this, JsonSerializer.Deserialize<TimeSale[]>(data.Content)); break;
                        case "TIMESALE_FUTURES": OnTimeSaleFutures?.Invoke(this, JsonSerializer.Deserialize<TimeSale[]>(data.Content)); break;
                        case "TIMESALE_OPTIONS": OnTimeSaleOptions?.Invoke(this, JsonSerializer.Deserialize<TimeSale[]>(data.Content)); break;
                        // Unknown
                        default: Console.WriteLine($"UNKNOWN data: {data}"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception [{ex.Message}] Processing Message: {data.Content}");
                }
            }
        }
        #endregion

        #region Admin
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640574
        public event ResultHandler OnLogin;
        public void Login()
        {
            long timeStamp = MillisecondsFromEpoch(Convert.ToDateTime(_userPrincipals.streamerInfo.tokenTimestamp).ToUniversalTime());

            var _credentials = new StreamerSettings.Credentials
            {
                userid = _account.accountId,
                token = _userPrincipals.streamerInfo.token,
                company = _account.company,
                segment = _account.segment,
                cddomain = _account.accountCdDomainId,
                usergroup = _userPrincipals.streamerInfo.userGroup,
                accesslevel = _userPrincipals.streamerInfo.accessLevel,
                authorized = "Y",
                timestamp = timeStamp,
                appid = _userPrincipals.streamerInfo.appId,
                acl = _userPrincipals.streamerInfo.acl
            };

            var credentialArr = _credentials.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).
                Select(p => new KeyValuePair<string, string>(p.Name, p.GetValue(_credentials, null).ToString()));

            var request = GenerateRequest("ADMIN", "LOGIN", new StreamerSettings.Parameters
            {
                credential = string.Join("&", credentialArr.Where(c => !string.IsNullOrWhiteSpace(c.Value)).Select(c => string.Format("{0}={1}", HttpUtility.UrlEncode(c.Key, Encoding.UTF8), HttpUtility.UrlEncode(c.Value, Encoding.UTF8)))),
                token = _userPrincipals.streamerInfo.token,
                version = "1.0",
                qoslevel = "0"
            });

            SendRequest(request);
        }

        public event ResultHandler OnStream;

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640578
        public event ResultHandler OnQOS;
        public void QOS(int qosLevel)
        {
            var request = GenerateRequest("ADMIN", "QOS", new StreamerSettings.Parameters
            {
                qoslevel = qosLevel.ToString()
            });

            SendRequest(request);
        }

        public event ResultHandler OnSubscribe;

        public event ResultHandler OnAdd;

        public event ResultHandler OnUnsubscribe;

        public event ResultHandler OnView;

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640576
        public event ResultHandler OnLogout;
        public void Logout()
        {
            var request = GenerateRequest("ADMIN", "LOGOUT", new StreamerSettings.Parameters { });

            SendRequest(request);
        }

        public event ResultHandler OnReconnect;
        #endregion

        #region Account Activity
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640581
        public event AccountActivityHandler OnAccountActivity;
        public void SubscribeAccountActivity(AccountActivityFields[] fields = null)
        {
            var key = _userPrincipals.streamerSubscriptionKeys?.keys.FirstOrDefault()?.key;

            if (null == key)
                return;

            var request = GenerateRequest("ACCT_ACTIVITY", "SUBS", new StreamerSettings.Parameters
            {
                keys = key,
                fields = GetCSVFieldsFromEnumList(fields, new[] { AccountActivityFields.SubscriptionKey })
            });

            SendRequest(request);
        }
        #endregion

        #region Level One
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640599
        private Dictionary<string, LevelOneQuotes> levelOneQuotesCache = new Dictionary<string, LevelOneQuotes>();
        public event LevelOneQuotesHandler OnLevelOneQuotes;
        public void SubscribeLevelOneQuotes(string[] symbols, LevelOneQuotesFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("QUOTE", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { LevelOneQuotesFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640602
        private Dictionary<string, LevelOneOptions> levelOneOptionsCache = new Dictionary<string, LevelOneOptions>();
        public event LevelOneOptionsHandler OnLevelOneOptions;
        public void SubscribeLevelOneOptions(string[] symbols, LevelOneOptionsFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("OPTION", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { LevelOneOptionsFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640604
        private Dictionary<string, LevelOneFutures> levelOneFuturesCache = new Dictionary<string, LevelOneFutures>();
        public event LevelOneFuturesHandler OnLevelOneFutures;
        public void SubscribeLevelOneFutures(string[] symbols, LevelOneFuturesFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("LEVELONE_FUTURES", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { LevelOneFuturesFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640607
        private Dictionary<string, LevelOneForex> levelOneForexCache = new Dictionary<string, LevelOneForex>();
        public event LevelOneForexHandler OnLevelOneForex;
        public void SubscribeLevelOneForex(string[] symbols, LevelOneForexFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("LEVELONE_FOREX", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { LevelOneForexFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640610
        private Dictionary<string, LevelOneFuturesOptions> levelOneFuturesOptionsCache = new Dictionary<string, LevelOneFuturesOptions>();
        public event LevelOneFuturesOptionsHandler OnLevelOneFuturesOptions;
        public void SubscribeLevelOneFuturesOptions(string[] symbols, LevelOneFuturesOptionsFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("LEVELONE_FUTURES_OPTIONS", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { LevelOneFuturesOptionsFields.Symbol })
            });

            SendRequest(request);
        }
        #endregion

        #region Chart
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640587
        public event ChartEquityHandler OnChartEquity;
        public void SubscribeChartEquity(string[] symbols, ChartEquityFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("CHART_EQUITY", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { ChartEquityFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640591
        public event ChartFuturesHandler OnChartFutures;
        public void SubscribeChartFutures(string[] symbols, ChartFuturesFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("CHART_FUTURES", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { ChartFuturesFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640591
        public event ChartOptionsHandler OnChartOptions;
        public void SubscribeChartOptions(string[] symbols, ChartOptionsFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("CHART_OPTIONS", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { ChartOptionsFields.Symbol })
            });

            SendRequest(request);
        }
        #endregion

        #region Book        
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnFuturesBook;
        public void SubscribeFuturesBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("FUTURES_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnForexBook;
        public void SubscribeForexBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("FOREX_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnFuturesOptionsBook;
        public void SubscribeFuturesOptionsBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("FUTURES_OPTIONS_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnListedBook;
        public void SubscribeListedBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("LISTED_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });
            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnNasdaqBook;
        public void SubscribeNasdaqBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("NASDAQ_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event BookHandler OnOptionsBook;
        public void SubscribeOptionsBook(string[] symbols, BookFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("OPTIONS_BOOK", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { BookFields.Symbol })
            });

            SendRequest(request);
        }
        #endregion

        #region Time & Sales
        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event TimeSaleHandler OnTimeSaleEquity;
        public void SubscribeTimeSaleEquity(string[] symbols, TimeSaleFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("TIMESALE_EQUITY", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { TimeSaleFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event TimeSaleHandler OnTimeSaleForex;
        public void SubscribeTimeSaleForex(string[] symbols, TimeSaleFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("TIMESALE_FOREX", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { TimeSaleFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event TimeSaleHandler OnTimeSaleFutures;
        public void SubscribeTimeSaleFutures(string[] symbols, TimeSaleFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("TIMESALE_FUTURES", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { TimeSaleFields.Symbol })
            });

            SendRequest(request);
        }

        // https://developer.tdameritrade.com/content/streaming-data#_Toc504640627
        public event TimeSaleHandler OnTimeSaleOptions;
        public void SubscribeTimeSaleOptions(string[] symbols, TimeSaleFields[] fields = null)
        {
            if (null == symbols || symbols.Length == 0)
                throw new ArgumentOutOfRangeException("Invalid Symbol List");

            var request = GenerateRequest("TIMESALE_OPTIONS", "SUBS", new StreamerSettings.Parameters
            {
                keys = string.Join(",", symbols.Select(n => n.ToUpper()).ToArray()),
                fields = GetCSVFieldsFromEnumList(fields, new[] { TimeSaleFields.Symbol })
            });

            SendRequest(request);
        }
        #endregion
    }
}
