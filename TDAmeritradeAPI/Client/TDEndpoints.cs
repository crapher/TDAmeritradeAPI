using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TDAmeritradeAPI.Models.API.AccountsTrading;
using TDAmeritradeAPI.Models.API.Instruments;
using TDAmeritradeAPI.Models.API.MarketHours;
using TDAmeritradeAPI.Models.API.Movers;
using TDAmeritradeAPI.Models.API.Options;
using TDAmeritradeAPI.Models.API.PriceHistory;
using TDAmeritradeAPI.Models.API.Quotes;
using TDAmeritradeAPI.Models.API.TransactionHistory;
using TDAmeritradeAPI.Models.API.UserInfoPreferences;
using TDAmeritradeAPI.Props;

namespace TDAmeritradeAPI.Client
{
    public partial class TDClient
    {
        /// <summary>
        ///  TD API Endpoints
        /// </summary>
        private string
            // Accounts & Trading (There are a few more order apis to add)
            _Accounts = "https://api.tdameritrade.com/v1/accounts",
            _AccountById = "https://api.tdameritrade.com/v1/accounts/{accountId}",
            _CancelOrder = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders/{orderId}",
            _GetOrder = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders/{orderId}",
            _GetOrdersByPath = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders",
            _GetOrdersByQuery = "https://api.tdameritrade.com/v1/orders",
            _PlaceOrder = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders",
            _ReplaceOrder = "https://api.tdameritrade.com/v1/accounts/{accountId}/orders/{orderId}",
            _SavedOrder = "https://api.tdameritrade.com/v1/accounts/{accountId}/savedorders",
            // Instruments
            _SearchInstruments = "https://api.tdameritrade.com/v1/instruments?symbol={symbol}&projection={projection}",
            _GetInstruments = "https://api.tdameritrade.com/v1/instruments/{cusip}",
            // Market Hours
            _MultipleMarketHours = "https://api.tdameritrade.com/v1/marketdata/hours",
            _SingleMarketHours = "https://api.tdameritrade.com/v1/marketdata/{market}/hours",
            // Movers
            _GetMovers = "https://api.tdameritrade.com/v1/marketdata/{index}/movers",
            // Option Chains
            _GetOptionChain = "https://api.tdameritrade.com/v1/marketdata/chains",
            // Price History
            _GetPriceHistory = "https://api.tdameritrade.com/v1/marketdata/{symbol}/pricehistory",
            // Quotes
            _GetQuote = "https://api.tdameritrade.com/v1/marketdata/{symbol}/quotes",
            _GetQuotes = "https://api.tdameritrade.com/v1/marketdata/quotes",
            // Transaction History
            _GetTransaction = "https://api.tdameritrade.com/v1/accounts/{accountId}/transactions/{transactionId}",
            _GetTransactions = "https://api.tdameritrade.com/v1/accounts/{accountId}/transactions",
            // User Info & Preferences
            _GetPreferences = "https://api.tdameritrade.com/v1/accounts/{accountId}/preferences",
            _GetStreamerSubKeys = "https://api.tdameritrade.com/v1/userprincipals/streamersubscriptionkeys",
            _GetUserPrincipals = "https://api.tdameritrade.com/v1/userprincipals",
            _UpdatePreferences = "https://api.tdameritrade.com/v1/accounts/{accountId}/preferences"
            // Watchlist (Not implementd yet)
            ;

        #region Accounts & Trading
        /// <summary>
        /// Get Account balances, positions, and orders for all linked accounts.
        /// </summary>
        /// <param name="fields">Valid Fields: Positions and/or Orders</param>
        /// <returns></returns>
        public async Task<TDResponse<Accounts[]>> GetAccounts(string[] fields)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"fields", string.Join(',', fields)}
            };

            return await ExecuteEndPoint<Accounts[]>(_Accounts, requestParams, Method.GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<TDResponse<Accounts>> GetAccountById(string field, string accountId)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"fields", field}
            };

            return await ExecuteEndPoint<Accounts>(_AccountById.Replace("{accountId}", accountId), requestParams, Method.GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<TDResponse<Accounts>> GetAccountById(string[] fields, string accountId)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"fields", string.Join(',', fields)}
            };

            return await ExecuteEndPoint<Accounts>(_AccountById.Replace("{accountId}", accountId), requestParams, Method.GET);
        }

        /// <summary>
        /// Cancel a specific order for a specific account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<TDResponse<Accounts>> CancelOrder(string accountId, string orderId)
        {
            var url = _CancelOrder.Replace("{accountId}", accountId);
            url = url.Replace("{orderId}", orderId);

            return await ExecuteEndPoint<Accounts>(url, null, Method.DELETE);
        }

        /// <summary>
        /// Get a specific order for a specific account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> GetOrder(string accountId, string orderId)
        {
            var url = _GetOrder.Replace("{accountId}", accountId);
            url = url.Replace("{orderId}", orderId);

            return await ExecuteEndPoint<Orders>(url, null, Method.GET);
        }

        /// <summary>
        /// Get all orders for a specific account.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="maxResults"></param>
        /// <param name="fromEnteredTime"></param>
        /// <param name="toEnteredTime"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> GetOrdersSingleAccount(string accountId, int maxResults, string fromEnteredTime, string toEnteredTime, string orderStatus)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"maxResults", maxResults},
                {"fromEnteredTime", fromEnteredTime},
                {"toEnteredTime", toEnteredTime},
                {"status", orderStatus}
            };

            var url = _GetOrdersByPath.Replace("{accountId}", accountId);
            return await ExecuteEndPoint<Orders>(url, requestParams, Method.GET);
        }

        /// <summary>
        /// Get all orders for all linked accounts
        /// </summary>
        /// <param name="maxResults"></param>
        /// <param name="fromEnteredTime"></param>
        /// <param name="toEnteredTime"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> GetOrdersAllAccounts(int maxResults, string fromEnteredTime, string toEnteredTime, string orderStatus)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"maxResults", maxResults},
                {"fromEnteredTime", fromEnteredTime},
                {"toEnteredTime", toEnteredTime},
                {"status", orderStatus}
            };

            return await ExecuteEndPoint<Orders>(_GetOrdersByQuery, requestParams, Method.GET);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeOrder"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> PlaceOrder(string accountId, OrderSettings placeOrder) =>
            await ExecuteEndPoint<Orders>(_PlaceOrder.Replace("{accountId}", accountId), placeOrder, Method.POST);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="replaceOrder"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> ReplaceOrder(string accountId, string orderId, OrderSettings replaceOrder)
        {
            var url = _ReplaceOrder.Replace("{accountId}", accountId);
            url = url.Replace("{orderId}", orderId);

            return await ExecuteEndPoint<Orders>(url, replaceOrder, Method.PUT);
        }

        /// <summary>
        /// Save an order for a specific account.
        /// </summary>
        /// <param name="saveOrder"></param>
        /// <returns></returns>
        public async Task<TDResponse<Orders>> SaveOrder(string accountId, OrderSettings saveOrder)
        {
            var url = _SavedOrder.Replace("{accountId}", accountId);

            return await ExecuteEndPoint<Orders>(url, saveOrder, Method.POST);
        }
        #endregion

        #region Instruments
        /// <summary>
        /// Get Instrument by cusip
        /// </summary>
        /// <param name="cusip"></param>
        /// <returns></returns>
        public async Task<TDResponse<Instrument[]>> GetInstrument(string cusip)
        {
            return await ExecuteEndPoint<Instrument[]>(_GetInstruments.Replace("{cusip}", cusip), null, Method.GET);
        }

        /// <summary>
        /// Search instrument by projection (symbol-search, symbol-regex, desc-search, desc-regex, fundamental)
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public async Task<TDResponse<InstrumentList>> SearchInstruments(string symbol, string projection)
        {
            var url = _SearchInstruments.Replace("{symbol}", symbol);
            url = url.Replace("{projection}", projection);

            return await ExecuteEndPoint<InstrumentList>(url, null, Method.GET);
        }
        #endregion

        #region Get Movers
        /// <summary>
        /// Get Top 10 (up or down) movers by value or percent for a particular market
        /// </summary>
        /// <param name="index">The index symbol to get movers from. Can be $COMPX, $DJI, or $SPX.X.</param>
        /// <param name="direction"></param>
        /// <param name="changeType"></param>
        /// <returns></returns>
        public async Task<TDResponse<Mover>> GetMovers(string index, string direction, string changeType)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"direction", direction},
                {"change", changeType}
            };

            return await ExecuteEndPoint<Mover>(_GetMovers.Replace("{index}", index), requestParams, Method.GET);
        }
        #endregion

        #region Market Hours
        /// <summary>
        /// 
        /// </summary>
        /// <param name="markets">Valid Markets: EQUITY, OPTION, FUTURE, BOND, or FOREX</param>
        /// <param name="date">Valid Formats yyyy-MM-dd or yyyy-MM-dd'T'HH:mm:ssz</param>
        /// <returns></returns>
        public async Task<TDResponse<MarketHours>> GetHoursForMultipleMarkets(string[] markets, string date)
        {
            var requestParams = new Dictionary<string, object>
            {
                //This will return delayed data
                //{"client_id", clientId},
                {"markets", string.Join(',', markets)},
                {"date", date}
            };

            return await ExecuteEndPoint<MarketHours>(_MultipleMarketHours, requestParams, Method.GET);
        }

        public async Task<TDResponse<MarketHours>> GetHoursForSingleMarket(string market, string date)
        {
            var requestParams = new Dictionary<string, object>
            {
                //This will return delayed data
                //{"client_id", clientId},
                {"date", date}
            };

            return await ExecuteEndPoint<MarketHours>(_SingleMarketHours.Replace("{market}", market), requestParams, Method.GET);
        }
        #endregion

        #region Get Price History
        public async Task<TDResponse<PriceHistory>> GetPriceHistory(PriceHistorySettings priceHistory)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"periodType", priceHistory.periodType},
                {"period", priceHistory.period},
                {"frequencyType", priceHistory.frequencyType},
                {"frequency", priceHistory.frequency},
                //{"endDate", priceHistory.endDate},
                //{"startDate", priceHistory.startDate},
                {"needExtendedHoursData", priceHistory.needExtendedHoursData},
            };

            return await ExecuteEndPoint<PriceHistory>(_GetPriceHistory.Replace("{symbol}", priceHistory.symbol), requestParams, Method.GET);
        }
        #endregion

        #region Get Quotes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<TDResponse<QuoteList>> GetQuote(string symbol) =>
            await ExecuteEndPoint<QuoteList>(_GetQuote.Replace("{symbol}", symbol), null, Method.GET);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        public async Task<TDResponse<QuoteList>> GetQuotes(string[] symbols)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"symbol", string.Join(',', symbols)}
            };

            return await ExecuteEndPoint<QuoteList>(_GetQuotes, requestParams, Method.GET);
        }
        #endregion

        #region Transactions
        /// <summary>
        /// Get transactions for a specific account.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<TDResponse<Transactions>> GetTransactions(TransactionsSettings settings)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"type", settings.type},
                {"symbol", settings.symbol},
                {"startDate", settings.startDate},
                {"endDate", settings.endDate},
            };

            return await ExecuteEndPoint<Transactions>(_GetTransactions.Replace("{accountId}", settings.accountId.ToString()), requestParams, Method.GET);
        }

        public async Task<TDResponse<Transactions>> GetTransaction(string accountId, string transactionId)
        {
            //TODO: This throws a not found when executing
            var url = _GetTransaction.Replace("{accountId}", accountId);
            url = url.Replace("{transactionId}", transactionId);

            return await ExecuteEndPoint<Transactions>(url, null, Method.GET);
        }
        #endregion

        #region UserInfo and Preferences
        public async Task<TDResponse<Preferences>> GetPreferences(string accountId)
        {
            var url = _GetPreferences.Replace("{accountId}", accountId);

            return await ExecuteEndPoint<Preferences>(url, null, Method.GET);
        }

        public async Task<TDResponse<SubscriptionKey>> GetStreamerSubKeys(string[] accountIds)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"accountIds", string.Join(',', accountIds)},
            };

            return await ExecuteEndPoint<SubscriptionKey>(_GetStreamerSubKeys, requestParams, Method.GET);
        }

        public async Task<TDResponse<UserPrincipals>> GetUserPrincipals(string[] fields)
        {
            var requestParams = new Dictionary<string, object>
            {
                {"fields", string.Join(',', fields)},
            };

            return await ExecuteEndPoint<UserPrincipals>(_GetUserPrincipals, requestParams, Method.GET);
        }

        public async Task<TDResponse<UserPrincipals>> UpdatePreferences(string accountId, UpdatePreferencesSettings settings) =>
            await ExecuteEndPoint<UserPrincipals>(_UpdatePreferences.Replace("{accountId}", accountId), settings, Method.PUT);
        #endregion

        #region OptionChain
        public async Task<TDResponse<OptionChain>> GetOptionChain(OptionChainSettings settings)
        {
            var requestParams = settings.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(p => p.Name, p => p.GetValue(settings, null));

            return await ExecuteEndPoint<OptionChain>(_GetOptionChain, requestParams, Method.GET);
        }
        #endregion
    }
}
