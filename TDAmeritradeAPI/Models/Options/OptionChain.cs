using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Options
{
    public class OptionChain
    {
        [DataMember(Name = "symbol")]
        public string Symbol { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "underlying")]
        public Underlying underlying { get; set; }

        [DataMember(Name = "strategy")]
        public string Strategy { get; set; }

        [DataMember(Name = "interval")]
        public object Interval { get; set; }

        [DataMember(Name = "isDelayed")]
        public bool IsDelayed { get; set; }

        [DataMember(Name = "isIndex")]
        public bool IsIndex { get; set; }

        [DataMember(Name = "interestRate")]
        public double InterestRate { get; set; }

        [DataMember(Name = "underlyingPrice")]
        public double UnderlyingPrice { get; set; }

        [DataMember(Name = "volatility")]
        public long Volatility { get; set; }

        [DataMember(Name = "daysToExpiration")]
        public long DaysToExpiration { get; set; }

        [DataMember(Name = "numberOfContracts")]
        public long numberOfContracts { get; set; }
        public float[] intervals { get; set; }
        public MonthlyStrategyList[] monthlyStrategyList { get; set; }

        [DataMember(Name = "callExpDateMap")]
        public Dictionary<string, Dictionary<string, ExpDateMap[]>> CallExpDateMap { get; set; }

        [DataMember(Name = "putExpDateMap")]
        public Dictionary<string, Dictionary<string, ExpDateMap[]>> PutExpDateMap { get; set; }

        public class ExpDateMap
        {
            [DataMember(Name = "putCall")]
            public string PutCall { get; set; }

            [DataMember(Name = "symbol")]
            public string Symbol { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "exchangeName")]
            public string ExchangeName { get; set; }

            [DataMember(Name = "bid")]
            public double Bid { get; set; }

            [DataMember(Name = "ask")]
            public double Ask { get; set; }

            [DataMember(Name = "last")]
            public double Last { get; set; }

            [DataMember(Name = "mark")]
            public double Mark { get; set; }

            [DataMember(Name = "bidSize")]
            public long BidSize { get; set; }

            [DataMember(Name = "askSize")]
            public long AskSize { get; set; }

            [DataMember(Name = "lastSize")]
            public long LastSize { get; set; }

            [DataMember(Name = "highPrice")]
            public double HighPrice { get; set; }

            [DataMember(Name = "lowPrice")]
            public double LowPrice { get; set; }

            [DataMember(Name = "openPrice")]
            public long OpenPrice { get; set; }

            [DataMember(Name = "closePrice")]
            public double ClosePrice { get; set; }

            [DataMember(Name = "totalVolume")]
            public long TotalVolume { get; set; }

            [DataMember(Name = "tradeDate")]
            public object TradeDate { get; set; }

            [DataMember(Name = "tradeTimeInLong")]
            public long TradeTimeInLong { get; set; }

            [DataMember(Name = "quoteTimeInLong")]
            public long QuoteTimeInLong { get; set; }

            [DataMember(Name = "netChange")]
            public double NetChange { get; set; }

            [DataMember(Name = "volatility")]
            public double Volatility { get; set; }

            [DataMember(Name = "delta")]
            public double Delta { get; set; }

            [DataMember(Name = "gamma")]
            public double Gamma { get; set; }

            [DataMember(Name = "theta")]
            public double Theta { get; set; }

            [DataMember(Name = "vega")]
            public double Vega { get; set; }

            [DataMember(Name = "rho")]
            public double Rho { get; set; }

            [DataMember(Name = "openInterest")]
            public long OpenInterest { get; set; }

            [DataMember(Name = "timeValue")]
            public double TimeValue { get; set; }

            [DataMember(Name = "theoreticalOptionValue")]
            public double TheoreticalOptionValue { get; set; }

            [DataMember(Name = "theoreticalVolatility")]
            public long TheoreticalVolatility { get; set; }

            [DataMember(Name = "optionDeliverablesList")]
            public object OptionDeliverablesList { get; set; }

            [DataMember(Name = "strikePrice")]
            public double StrikePrice { get; set; }

            [DataMember(Name = "expirationDate")]
            public long ExpirationDate { get; set; }

            [DataMember(Name = "daysToExpiration")]
            public long DaysToExpiration { get; set; }

            [DataMember(Name = "expirationType")]
            public string ExpirationType { get; set; }

            [DataMember(Name = "lastTradingDay")]
            public long LastTradingDay { get; set; }

            [DataMember(Name = "multiplier")]
            public long Multiplier { get; set; }

            [DataMember(Name = "settlementType")]
            public string SettlementType { get; set; }

            [DataMember(Name = "deliverableNote")]
            public string DeliverableNote { get; set; }

            [DataMember(Name = "isIndexOption")]
            public object IsIndexOption { get; set; }

            [DataMember(Name = "percentChange")]
            public double PercentChange { get; set; }

            [DataMember(Name = "markChange")]
            public double MarkChange { get; set; }

            [DataMember(Name = "markPercentChange")]
            public double MarkPercentChange { get; set; }

            [DataMember(Name = "nonStandard")]
            public bool NonStandard { get; set; }

            [DataMember(Name = "inTheMoney")]
            public bool InTheMoney { get; set; }

            [DataMember(Name = "mini")]
            public bool Mini { get; set; }
        }

        public class MonthlyStrategyList
        {
            public string month { get; set; }
            public int year { get; set; }
            public int day { get; set; }
            public int daysToExp { get; set; }
            public string secondaryMonth { get; set; }
            public int secondaryYear { get; set; }
            public int secondaryDay { get; set; }
            public int secondaryDaysToExp { get; set; }
            public string type { get; set; }
            public string secondaryType { get; set; }
            public bool leap { get; set; }
            public OptionStrategyList[] optionStrategyList { get; set; }
            public bool secondaryLeap { get; set; }
        }

        public class OptionStrategyList
        {
            public PrimaryLeg primaryLeg { get; set; }
            public SecondaryLeg secondaryLeg { get; set; }
            public string strategyStrike { get; set; }
            public float strategyBid { get; set; }
            public float strategyAsk { get; set; }
        }

        public class PrimaryLeg
        {
            public string symbol { get; set; }
            public string putCallInd { get; set; }
            public string description { get; set; }
            public float bid { get; set; }
            public float ask { get; set; }
            public string range { get; set; }
            public float strikePrice { get; set; }
            public float totalVolume { get; set; }
        }

        public class SecondaryLeg
        {
            public string symbol { get; set; }
            public string putCallInd { get; set; }
            public string description { get; set; }
            public float bid { get; set; }
            public float ask { get; set; }
            public string range { get; set; }
            public float strikePrice { get; set; }
            public float totalVolume { get; set; }
        }

        public class Underlying
        {
            [DataMember(Name = "symbol")]
            public string Symbol { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "change")]
            public long Change { get; set; }

            [DataMember(Name = "percentChange")]
            public long PercentChange { get; set; }

            [DataMember(Name = "close")]
            public double Close { get; set; }

            [DataMember(Name = "quoteTime")]
            public long QuoteTime { get; set; }

            [DataMember(Name = "tradeTime")]
            public long TradeTime { get; set; }

            [DataMember(Name = "bid")]
            public double Bid { get; set; }

            [DataMember(Name = "ask")]
            public double Ask { get; set; }

            [DataMember(Name = "last")]
            public double Last { get; set; }

            [DataMember(Name = "mark")]
            public double Mark { get; set; }

            [DataMember(Name = "markChange")]
            public long MarkChange { get; set; }

            [DataMember(Name = "markPercentChange")]
            public long MarkPercentChange { get; set; }

            [DataMember(Name = "bidSize")]
            public long BidSize { get; set; }

            [DataMember(Name = "askSize")]
            public long AskSize { get; set; }

            [DataMember(Name = "highPrice")]
            public double HighPrice { get; set; }

            [DataMember(Name = "lowPrice")]
            public double LowPrice { get; set; }

            [DataMember(Name = "openPrice")]
            public double OpenPrice { get; set; }

            [DataMember(Name = "totalVolume")]
            public long TotalVolume { get; set; }

            [DataMember(Name = "exchangeName")]
            public string ExchangeName { get; set; }

            [DataMember(Name = "fiftyTwoWeekHigh")]
            public double FiftyTwoWeekHigh { get; set; }

            [DataMember(Name = "fiftyTwoWeekLow")]
            public double FiftyTwoWeekLow { get; set; }

            [DataMember(Name = "delayed")]
            public bool Delayed { get; set; }
        }
    }
}
