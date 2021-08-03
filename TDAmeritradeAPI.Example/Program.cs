using System;
using TDAmeritradeAPI.Client;
using TDAmeritradeAPI.Fields;
using TDAmeritradeAPI.Props;

namespace TDAmeritradeAPI.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Client */
			string fileName = "{FILENAME}";
            string clientId = "{CONSUMER_KEY}";
            TDClient client = new TDClient(fileName, clientId);

            /* Get Market Hours */
            //var hours = client.GetHoursForMultipleMarkets(new[] {MarketType.Bond, MarketType.Equity}, "2020-03-23");
            /* Get Market Hour */
            //var hour = client.GetHoursForSingleMarket(MarketType.Equity, "2020-03-23");

            /* Get Accounts */
            var accounts = client.GetAccounts(new[] { "positions", "orders" }).Result.Data;
            /* Get Account by AccountById */

            /* Get Orders By Path */
            //var ordersByPath = client.GetOrdersAllAccounts(10, "2020-01-01", DateTime.Today.ToString("yyyy-MM-dd"), OrderStatus.Filled);

            /* Get Movers */
            //var movers = client.GetMovers("$DJI", Direction.Up.ToLower(), Change.Value.ToLower());

            /* Get Price History */
            var priceHistory = new PriceHistorySettings
            {
                symbol = "ROKU",
                periodType = PeriodType.Month,
                period = 6,
                frequencyType = FrequencyType.Daily,
                frequency = 1,
                needExtendedHoursData = true
            };
            //var historicalData = client.GetPriceHistory(priceHistory).Result.Data;

            /* Get Single Quote */
            //var quote = client.GetQuote("ROKU").Result.Data;
            /* Get Multiple Quotes */
            //var quotes = client.GetQuotes(new[] {"ROKU", "AAPL", "TSLA"});

            /* Get Transactions */
            var transactionSettings = new TransactionsSettings
            {
                accountId = 0,
                symbol = "ROKU",
                startDate = "2020-01-01",
                endDate = DateTime.Today.ToString("yyyy-MM-dd"),
                type = TransactionType.All
            };
            //var transactions = client.GetTransactions(transactionSettings);
            /* Get Transaction By TransactionId */
            //var transaction = client.GetTransaction("", "");

            /* Get Preferences */
            //var pref = client.GetPreferences("");

            /* Get Streamer Sub Keys */
            //var keys = client.GetStreamerSubKeys(new[] {""});

            /* Get User Principals */
            //var userPrincipals = client.GetUserPrincipals(new[] { "streamerSubscriptionKeys", "streamerConnectionInfo", "preferences", "surrogateIds" }).Result.Data;

            /* Update Preferences */
            var updatePreferencesSettings = new UpdatePreferencesSettings
            {
                expressTrading = true,
                defaultEquityOrderLegInstruction = Order.Instruction.Buy,
                defaultEquityOrderType = Order.Type.Limit,
                defaultEquityOrderPriceLinkType = Order.PriceLinkType.None,
                defaultEquityOrderDuration = Order.Duration.Day,
                defaultEquityOrderMarketSession = Order.MarketSession.Normal,
                defaultEquityQuantity = 100,
                mutualFundTaxLotMethod = TaxLotMethod.LIFO,
                optionTaxLotMethod = TaxLotMethod.LIFO,
                equityTaxLotMethod = TaxLotMethod.LIFO,
                defaultAdvancedToolLaunch = AdvancedToolLaunch.N,
                authTokenTimeout = AuthTokenTimeOut.FiftyFiveMinutes
            };
            //var update = client.UpdatePreferences(accounts[0].securitiesAccount.accountId, updatePreferencesSettings).Result.Success;

            /* Option Chain */
            var optionSettings = new OptionChainSettings
            {
                symbol = "ROKU",
                strikeCount = 3,
                strategy = Options.Strategy.Single
            };
            //var chain = client.GetOptionChain(optionSettings);

            /* Place Orders */
            /* Place Single Order */
            var order = new OrderSettings
            {
                orderType = Order.Type.Limit,
                price = 200.00,
                session = Order.MarketSession.Normal,
                duration = Order.Duration.GTC,
                orderStrategyType = Order.StrategyType.Single,
                orderLegCollection = new[]
                {
                    new OrderSettings.OrderLegCollection
                    {
                        instruction = Order.Instruction.Sell,
                        quantity = 300,
                        instrument = new OrderSettings.OrderLegCollection.Instrument
                        {
                            symbol = "ROKU",
                            assetType = Order.AssetType.Equity
                        }
                    }
                }
            };
            //var orderResult = client.PlaceOrder("", order).Result.Success;

            /* Place Option Vertical Spread */
            var order2 = new OrderSettings
            {
                orderType = Order.Type.NetCredit,
                session = Order.MarketSession.Normal,
                duration = Order.Duration.GTC,
                price = 4.50,
                orderStrategyType = Order.StrategyType.Single,
                orderLegCollection = new[]
                {
                    new OrderSettings.OrderLegCollection
                    {
                        instruction = Order.Instruction.SellToOpen,
                        quantity = 1,
                        instrument = new OrderSettings.OrderLegCollection.Instrument
                        {
                            symbol = "ROKU_091622C100",
                            assetType = Order.AssetType.Option
                        }
                    },
                    new OrderSettings.OrderLegCollection
                    {
                        instruction = Order.Instruction.BuyToOpen,
                        quantity = 1,
                        instrument = new OrderSettings.OrderLegCollection.Instrument
                        {
                            symbol = "ROKU_091622C105",
                            assetType = Order.AssetType.Option
                        }
                    }
                }
            };
            //var orderResult2 = client.PlaceOrder("", order2).Result.Success;

            /* Replace Order */
            var replaceOrder = new OrderSettings()
            {
                orderType = Order.Type.Limit,
                price = 200.01,
                session = Order.MarketSession.Normal,
                duration = Order.Duration.GTC,
                orderStrategyType = Order.StrategyType.Single,
                orderLegCollection = new[]
                {
                    new OrderSettings.OrderLegCollection
                    {
                        instruction = Order.Instruction.Sell,
                        quantity = 200,
                        instrument = new OrderSettings.OrderLegCollection.Instrument
                        {
                            symbol = "ROKU",
                            assetType = Order.AssetType.Equity
                        }
                    }
                }
            };
            //var replaceOrderResult = client.ReplaceOrder("", "", replaceOrder).Result.Success;

            /* Cancel Order */
            //var cancelOrder = client.CancelOrder("", "");

            /* Instruments */
            //var instSymbolSearch = client.SearchInstruments("X", Instruments.SymbolSearch).Result.Data;
            //var instSymbolRegex = client.SearchInstruments(@"T.*", Instruments.SymbolRegex).Result.Data;
            //var instDescSearch = client.SearchInstruments("ADR", Instruments.DescSearch).Result.Data;
            //var instDescRegex = client.SearchInstruments(@".*Bond.*", Instruments.DescRegex).Result.Data;
            //var instFundamentals = client.SearchInstruments("AAPL", Instruments.Fundamentals).Result.Data;

            //var instBycusip = client.GetInstrument("00206R102").Result.Data;
        }
    }
}
