using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640605
    public class LevelOneFutures : IUpdatableBySymbol<LevelOneFutures>
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
        public string AskId { get; set; }
        [DataMember(Name = "7")]
        public string BidId { get; set; }
        [DataMember(Name = "8")]
        public double? TotalVolume { get; set; }
        [DataMember(Name = "9")]
        public long? LastSize { get; set; }
        [DataMember(Name = "10")]
        public long? QuoteTime { get; set; }
        [DataMember(Name = "11")]
        public long? TradeTime { get; set; }
        [DataMember(Name = "12")]
        public double? HighPrice { get; set; }
        [DataMember(Name = "13")]
        public double? LowPrice { get; set; }
        [DataMember(Name = "14")]
        public double? ClosePrice { get; set; }
        [DataMember(Name = "15")]
        public string ExchangeId { get; set; }
        [DataMember(Name = "16")]
        public string Description { get; set; }
        [DataMember(Name = "17")]
        public string LastId { get; set; }
        [DataMember(Name = "18")]
        public double? OpenPrice { get; set; }
        [DataMember(Name = "19")]
        public double? NetChange { get; set; }
        [DataMember(Name = "20")]
        public double? FuturePercentChange { get; set; }
        [DataMember(Name = "21")]
        public string ExchangeName { get; set; }
        [DataMember(Name = "22")]
        public string SecurityStatus { get; set; }
        [DataMember(Name = "23")]
        public int? OpenInterest { get; set; }
        [DataMember(Name = "24")]
        public double? Mark { get; set; }
        [DataMember(Name = "25")]
        public double? Tick { get; set; }
        [DataMember(Name = "26")]
        public double? TickAmount { get; set; }
        [DataMember(Name = "27")]
        public string Product { get; set; }
        [DataMember(Name = "28")]
        public string FuturePriceFormat { get; set; }
        [DataMember(Name = "29")]
        public string FutureTradingHours { get; set; }
        [DataMember(Name = "30")]
        public bool? FutureIsTradable { get; set; }
        [DataMember(Name = "31")]
        public double? FutureMultiplier { get; set; }
        [DataMember(Name = "32")]
        public bool? FutureIsActive { get; set; }
        [DataMember(Name = "33")]
        public double? FutureSettlementPrice { get; set; }
        [DataMember(Name = "34")]
        public string FutureActiveSymbol { get; set; }
        [DataMember(Name = "35")]
        public long? FutureExpirationDate { get; set; }
        [DataMember(Name = "delayed")]
        public bool? Delayed { get; set; }

        public void Update(LevelOneFutures updatedObject)
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
            QuoteTime = updatedObject.QuoteTime ?? QuoteTime;
            TradeTime = updatedObject.TradeTime ?? TradeTime;
            HighPrice = updatedObject.HighPrice ?? HighPrice;
            LowPrice = updatedObject.LowPrice ?? LowPrice;
            ClosePrice = updatedObject.ClosePrice ?? ClosePrice;
            ExchangeId = updatedObject.ExchangeId ?? ExchangeId;
            Description = updatedObject.Description ?? Description;
            LastId = updatedObject.LastId ?? LastId;
            OpenPrice = updatedObject.OpenPrice ?? OpenPrice;
            NetChange = updatedObject.NetChange ?? NetChange;
            FuturePercentChange = updatedObject.FuturePercentChange ?? FuturePercentChange;
            ExchangeName = updatedObject.ExchangeName ?? ExchangeName;
            SecurityStatus = updatedObject.SecurityStatus ?? SecurityStatus;
            OpenInterest = updatedObject.OpenInterest ?? OpenInterest;
            Mark = updatedObject.Mark ?? Mark;
            Tick = updatedObject.Tick ?? Tick;
            TickAmount = updatedObject.TickAmount ?? TickAmount;
            Product = updatedObject.Product ?? Product;
            FuturePriceFormat = updatedObject.FuturePriceFormat ?? FuturePriceFormat;
            FutureTradingHours = updatedObject.FutureTradingHours ?? FutureTradingHours;
            FutureIsTradable = updatedObject.FutureIsTradable ?? FutureIsTradable;
            FutureMultiplier = updatedObject.FutureMultiplier ?? FutureMultiplier;
            FutureIsActive = updatedObject.FutureIsActive ?? FutureIsActive;
            FutureSettlementPrice = updatedObject.FutureSettlementPrice ?? FutureSettlementPrice;
            FutureActiveSymbol = updatedObject.FutureActiveSymbol ?? FutureActiveSymbol;
            FutureExpirationDate = updatedObject.FutureExpirationDate ?? FutureExpirationDate;
            Delayed = updatedObject.Delayed ?? Delayed;
        }
    }
}
