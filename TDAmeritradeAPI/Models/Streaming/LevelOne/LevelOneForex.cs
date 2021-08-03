using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640608
    public class LevelOneForex : IUpdatableBySymbol<LevelOneForex>
    {
        [DataMember(Name = "key")]
        public string Symbol { get; set; }
        [DataMember(Name = "1")]
        public double? BidPrice { get; set; }
        [DataMember(Name = "2")]
        public double? AskPrice { get; set; }
        [DataMember(Name = "3")]
        public double? LastPrice { get; set; }
        [DataMember(Name = "4")]
        public long? BidSize { get; set; }
        [DataMember(Name = "5")]
        public long? AskSize { get; set; }
        [DataMember(Name = "6")]
        public double? TotalVolume { get; set; }
        [DataMember(Name = "7")]
        public long? LastSize { get; set; }
        [DataMember(Name = "8")]
        public long? QuoteTime { get; set; }
        [DataMember(Name = "9")]
        public long? TradeTime { get; set; }
        [DataMember(Name = "10")]
        public double? HighPrice { get; set; }
        [DataMember(Name = "11")]
        public double? LowPrice { get; set; }
        [DataMember(Name = "12")]
        public double? ClosePrice { get; set; }
        [DataMember(Name = "13")]
        public string ExchangeId { get; set; }
        [DataMember(Name = "14")]
        public string Description { get; set; }
        [DataMember(Name = "15")]
        public double? OpenPrice { get; set; }
        [DataMember(Name = "16")]
        public double? NetChange { get; set; }
        [DataMember(Name = "17")]
        public double? PercentChange { get; set; }
        [DataMember(Name = "18")]
        public string ExchangeName { get; set; }
        [DataMember(Name = "19")]
        public int? Digits { get; set; }
        [DataMember(Name = "20")]
        public string SecurityStatus { get; set; }
        [DataMember(Name = "21")]
        public double? Tick { get; set; }
        [DataMember(Name = "22")]
        public double? TickAmount { get; set; }
        [DataMember(Name = "23")]
        public string Product { get; set; }
        [DataMember(Name = "24")]
        public string TradingHours { get; set; }
        [DataMember(Name = "25")]
        public bool? IsTradable { get; set; }
        [DataMember(Name = "26")]
        public string MarketMaker { get; set; }
        [DataMember(Name = "27")]
        public double? _52WkHigh { get; set; }
        [DataMember(Name = "28")]
        public double? _52WkLow { get; set; }
        [DataMember(Name = "29")]
        public double? Mark { get; set; }
        [DataMember(Name = "delayed")]
        public bool? Delayed { get; set; }

        public void Update(LevelOneForex updatedObject)
        {
            BidPrice = updatedObject.BidPrice ?? BidPrice;
            AskPrice = updatedObject.AskPrice ?? AskPrice;
            LastPrice = updatedObject.LastPrice ?? LastPrice;
            BidSize = updatedObject.BidSize ?? BidSize;
            AskSize = updatedObject.AskSize ?? AskSize;
            TotalVolume = updatedObject.TotalVolume ?? TotalVolume;
            LastSize = updatedObject.LastSize ?? LastSize;
            QuoteTime = updatedObject.QuoteTime ?? QuoteTime;
            TradeTime = updatedObject.TradeTime ?? TradeTime;
            HighPrice = updatedObject.HighPrice ?? HighPrice;
            LowPrice = updatedObject.LowPrice ?? LowPrice;
            ClosePrice = updatedObject.ClosePrice ?? ClosePrice;
            ExchangeId = updatedObject.ExchangeId ?? ExchangeId;
            Description = updatedObject.Description ?? Description;
            OpenPrice = updatedObject.OpenPrice ?? OpenPrice;
            NetChange = updatedObject.NetChange ?? NetChange;
            PercentChange = updatedObject.PercentChange ?? PercentChange;
            ExchangeName = updatedObject.ExchangeName ?? ExchangeName;
            Digits = updatedObject.Digits ?? Digits;
            SecurityStatus = updatedObject.SecurityStatus ?? SecurityStatus;
            Tick = updatedObject.Tick ?? Tick;
            TickAmount = updatedObject.TickAmount ?? TickAmount;
            Product = updatedObject.Product ?? Product;
            TradingHours = updatedObject.TradingHours ?? TradingHours;
            IsTradable = updatedObject.IsTradable ?? IsTradable;
            MarketMaker = updatedObject.MarketMaker ?? MarketMaker;
            _52WkHigh = updatedObject._52WkHigh ?? _52WkHigh;
            _52WkLow = updatedObject._52WkLow ?? _52WkLow;
            Mark = updatedObject.Mark ?? Mark;
            Delayed = updatedObject.Delayed ?? Delayed;
        }
    }
}
