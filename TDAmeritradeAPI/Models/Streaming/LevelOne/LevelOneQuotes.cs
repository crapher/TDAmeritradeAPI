using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640600
    public class LevelOneQuotes : IUpdatableBySymbol<LevelOneQuotes>
    {
        [DataMember(Name = "key")]
        public string Symbol { get; set; }
        [DataMember(Name = "1")]
        public float? BidPrice { get; set; }
        [DataMember(Name = "2")]
        public float? AskPrice { get; set; }
        [DataMember(Name = "3")]
        public float? LastPrice { get; set; }
        [DataMember(Name = "4")]
        public float? BidSize { get; set; }
        [DataMember(Name = "5")]
        public float? AskSize { get; set; }
        [DataMember(Name = "6")]
        public string AskId { get; set; }
        [DataMember(Name = "7")]
        public string BidId { get; set; }
        [DataMember(Name = "8")]
        public long? TotalVolume { get; set; }
        [DataMember(Name = "9")]
        public float? LastSize { get; set; }
        [DataMember(Name = "10")]
        public int? TradeTime { get; set; }
        [DataMember(Name = "11")]
        public int? QuoteTime { get; set; }
        [DataMember(Name = "12")]
        public float? HighPrice { get; set; }
        [DataMember(Name = "13")]
        public float? LowPrice { get; set; }
        [DataMember(Name = "14")]
        public string BidTick { get; set; }
        [DataMember(Name = "15")]
        public float? ClosePrice { get; set; }
        [DataMember(Name = "16")]
        public string ExchangeId { get; set; }
        [DataMember(Name = "17")]
        public bool? Marginable { get; set; }
        [DataMember(Name = "18")]
        public bool? Shortable { get; set; }
        [DataMember(Name = "19")]
        public float? IslandBid { get; set; }
        [DataMember(Name = "20")]
        public float? IslandAsk { get; set; }
        [DataMember(Name = "21")]
        public int? IslandVolume { get; set; }
        [DataMember(Name = "22")]
        public int? QuoteDay { get; set; }
        [DataMember(Name = "23")]
        public int? TradeDay { get; set; }
        [DataMember(Name = "24")]
        public float? Volatility { get; set; }
        [DataMember(Name = "25")]
        public string Description { get; set; }
        [DataMember(Name = "26")]
        public string LastId { get; set; }
        [DataMember(Name = "27")]
        public int? Digits { get; set; }
        [DataMember(Name = "28")]
        public float? OpenPrice { get; set; }
        [DataMember(Name = "29")]
        public float? NetChange { get; set; }
        [DataMember(Name = "30")]
        public float? _52WkHigh { get; set; }
        [DataMember(Name = "31")]
        public float? _52WkLow { get; set; }
        [DataMember(Name = "32")]
        public float? PERatio { get; set; }
        [DataMember(Name = "33")]
        public float? DividendAmount { get; set; }
        [DataMember(Name = "34")]
        public float? DividendYield { get; set; }
        [DataMember(Name = "35")]
        public int? IslandBidSize { get; set; }
        [DataMember(Name = "36")]
        public int? IslandAskSize { get; set; }
        [DataMember(Name = "37")]
        public float? NAV { get; set; }
        [DataMember(Name = "38")]
        public float? FundPrice { get; set; }
        [DataMember(Name = "39")]
        public string ExchangeName { get; set; }
        [DataMember(Name = "40")]
        public string DividendDate { get; set; }
        [DataMember(Name = "41")]
        public bool? RegularMarketQuote { get; set; }
        [DataMember(Name = "42")]
        public bool? RegularMarketTrade { get; set; }
        [DataMember(Name = "43")]
        public float? RegularMarketLastPrice { get; set; }
        [DataMember(Name = "44")]
        public float? RegularMarketLastSize { get; set; }
        [DataMember(Name = "45")]
        public int? RegularMarketTradeTime { get; set; }
        [DataMember(Name = "46")]
        public int? RegularMarketTradeDay { get; set; }
        [DataMember(Name = "47")]
        public float? RegularMarketNetChange { get; set; }
        [DataMember(Name = "48")]
        public string SecurityStatus { get; set; }
        [DataMember(Name = "49")]
        public double? Mark { get; set; }
        [DataMember(Name = "50")]
        public long? QuoteTimeinLong { get; set; }
        [DataMember(Name = "51")]
        public long? TradeTimeinLong { get; set; }
        [DataMember(Name = "52")]
        public long? RegularMarketTradeTimeinLong { get; set; }
        [DataMember(Name = "delayed")]
        public bool? Delayed { get; set; }

        public void Update(LevelOneQuotes updatedObject)
        {
            BidPrice = updatedObject.BidPrice ?? BidPrice;
            AskPrice = updatedObject.AskPrice ?? AskPrice;
            LastPrice = updatedObject.LastPrice ?? LastPrice;
            BidSize = updatedObject.BidSize ?? BidSize;
            AskSize = updatedObject.AskSize ?? AskSize;
            AskId = updatedObject.AskId ?? AskId;
            BidId = updatedObject.BidId ?? BidId;
            TotalVolume = updatedObject.TotalVolume ?? TotalVolume;
            LastSize = updatedObject.LastSize ?? LastSize;
            TradeTime = updatedObject.TradeTime ?? TradeTime;
            QuoteTime = updatedObject.QuoteTime ?? QuoteTime;
            HighPrice = updatedObject.HighPrice ?? HighPrice;
            LowPrice = updatedObject.LowPrice ?? LowPrice;
            BidTick = updatedObject.BidTick ?? BidTick;
            ClosePrice = updatedObject.ClosePrice ?? ClosePrice;
            ExchangeId = updatedObject.ExchangeId ?? ExchangeId;
            Marginable = updatedObject.Marginable ?? Marginable;
            Shortable = updatedObject.Shortable ?? Shortable;
            IslandBid = updatedObject.IslandBid ?? IslandBid;
            IslandAsk = updatedObject.IslandAsk ?? IslandAsk;
            IslandVolume = updatedObject.IslandVolume ?? IslandVolume;
            QuoteDay = updatedObject.QuoteDay ?? QuoteDay;
            TradeDay = updatedObject.TradeDay ?? TradeDay;
            Volatility = updatedObject.Volatility ?? Volatility;
            Description = updatedObject.Description ?? Description;
            LastId = updatedObject.LastId ?? LastId;
            Digits = updatedObject.Digits ?? Digits;
            OpenPrice = updatedObject.OpenPrice ?? OpenPrice;
            NetChange = updatedObject.NetChange ?? NetChange;
            _52WkHigh = updatedObject._52WkHigh ?? _52WkHigh;
            _52WkLow = updatedObject._52WkLow ?? _52WkLow;
            PERatio = updatedObject.PERatio ?? PERatio;
            DividendAmount = updatedObject.DividendAmount ?? DividendAmount;
            DividendYield = updatedObject.DividendYield ?? DividendYield;
            IslandBidSize = updatedObject.IslandBidSize ?? IslandBidSize;
            IslandAskSize = updatedObject.IslandAskSize ?? IslandAskSize;
            NAV = updatedObject.NAV ?? NAV;
            FundPrice = updatedObject.FundPrice ?? FundPrice;
            ExchangeName = updatedObject.ExchangeName ?? ExchangeName;
            DividendDate = updatedObject.DividendDate ?? DividendDate;
            RegularMarketQuote = updatedObject.RegularMarketQuote ?? RegularMarketQuote;
            RegularMarketTrade = updatedObject.RegularMarketTrade ?? RegularMarketTrade;
            RegularMarketLastPrice = updatedObject.RegularMarketLastPrice ?? RegularMarketLastPrice;
            RegularMarketLastSize = updatedObject.RegularMarketLastSize ?? RegularMarketLastSize;
            RegularMarketTradeTime = updatedObject.RegularMarketTradeTime ?? RegularMarketTradeTime;
            RegularMarketTradeDay = updatedObject.RegularMarketTradeDay ?? RegularMarketTradeDay;
            RegularMarketNetChange = updatedObject.RegularMarketNetChange ?? RegularMarketNetChange;
            SecurityStatus = updatedObject.SecurityStatus ?? SecurityStatus;
            Mark = updatedObject.Mark ?? Mark;
            QuoteTimeinLong = updatedObject.QuoteTimeinLong ?? QuoteTimeinLong;
            TradeTimeinLong = updatedObject.TradeTimeinLong ?? TradeTimeinLong;
            RegularMarketTradeTimeinLong = updatedObject.RegularMarketTradeTimeinLong ?? RegularMarketTradeTimeinLong;
        }
    }
}
