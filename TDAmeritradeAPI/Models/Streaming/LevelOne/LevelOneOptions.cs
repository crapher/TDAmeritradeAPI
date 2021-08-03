using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.LevelOne
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640602
    public class LevelOneOptions : IUpdatableBySymbol<LevelOneOptions>
    {
        [DataMember(Name = "key")]
        public string Symbol { get; set; }
        [DataMember(Name = "1")]
        public string Description { get; set; }
        [DataMember(Name = "2")]
        public float? BidPrice { get; set; }
        [DataMember(Name = "3")]
        public float? AskPrice { get; set; }
        [DataMember(Name = "4")]
        public float? LastPrice { get; set; }
        [DataMember(Name = "5")]
        public float? HighPrice { get; set; }
        [DataMember(Name = "6")]
        public float? LowPrice { get; set; }
        [DataMember(Name = "7")]
        public float? ClosePrice { get; set; }
        [DataMember(Name = "8")]
        public long? TotalVolume { get; set; }
        [DataMember(Name = "9")]
        public int? OpenInterest { get; set; }
        [DataMember(Name = "10")]
        public float? Volatility { get; set; }
        [DataMember(Name = "11")]
        public long? QuoteTime { get; set; }
        [DataMember(Name = "12")]
        public long? TradeTime { get; set; }
        [DataMember(Name = "13")]
        public float? MoneyIntrinsicValue { get; set; }
        [DataMember(Name = "14")]
        public int? QuoteDay { get; set; }
        [DataMember(Name = "15")]
        public int? TradeDay { get; set; }
        [DataMember(Name = "16")]
        public int? ExpirationYear { get; set; }
        [DataMember(Name = "17")]
        public float? Multiplier { get; set; }
        [DataMember(Name = "18")]
        public int? Digits { get; set; }
        [DataMember(Name = "19")]
        public float? OpenPrice { get; set; }
        [DataMember(Name = "20")]
        public float? BidSize { get; set; }
        [DataMember(Name = "21")]
        public float? AskSize { get; set; }
        [DataMember(Name = "22")]
        public float? LastSize { get; set; }
        [DataMember(Name = "23")]
        public float? NetChange { get; set; }
        [DataMember(Name = "24")]
        public float? StrikePrice { get; set; }
        [DataMember(Name = "25")]
        public string ContractType { get; set; }
        [DataMember(Name = "26")]
        public string Underlying { get; set; }
        [DataMember(Name = "27")]
        public int? ExpirationMonth { get; set; }
        [DataMember(Name = "28")]
        public string Deliverables { get; set; }
        [DataMember(Name = "29")]
        public float? TimeValue { get; set; }
        [DataMember(Name = "30")]
        public int? ExpirationDay { get; set; }
        [DataMember(Name = "31")]
        public int? DaystoExpiration { get; set; }
        [DataMember(Name = "32")]
        public float? Delta { get; set; }
        [DataMember(Name = "33")]
        public float? Gamma { get; set; }
        [DataMember(Name = "34")]
        public float? Theta { get; set; }
        [DataMember(Name = "35")]
        public float? Vega { get; set; }
        [DataMember(Name = "36")]
        public float? Rho { get; set; }
        [DataMember(Name = "37")]
        public string SecurityStatus { get; set; }
        [DataMember(Name = "38")]
        public float? TheoreticalOptionValue { get; set; }
        [DataMember(Name = "39")]
        public double? UnderlyingPrice { get; set; }
        [DataMember(Name = "40")]
        public string UVExpirationType { get; set; }
        [DataMember(Name = "41")]
        public double? Mark { get; set; }
        [DataMember(Name = "delayed")]
        public bool? Delayed { get; set; }

        public void Update(LevelOneOptions updatedObject)
        {
            Description = updatedObject.Description ?? Description;
            BidPrice = updatedObject.BidPrice ?? BidPrice;
            AskPrice = updatedObject.AskPrice ?? AskPrice;
            LastPrice = updatedObject.LastPrice ?? LastPrice;
            HighPrice = updatedObject.HighPrice ?? HighPrice;
            LowPrice = updatedObject.LowPrice ?? LowPrice;
            ClosePrice = updatedObject.ClosePrice ?? ClosePrice;
            TotalVolume = updatedObject.TotalVolume ?? TotalVolume;
            OpenInterest = updatedObject.OpenInterest ?? OpenInterest;
            Volatility = updatedObject.Volatility ?? Volatility;
            QuoteTime = updatedObject.QuoteTime ?? QuoteTime;
            TradeTime = updatedObject.TradeTime ?? TradeTime;
            MoneyIntrinsicValue = updatedObject.MoneyIntrinsicValue ?? MoneyIntrinsicValue;
            QuoteDay = updatedObject.QuoteDay ?? QuoteDay;
            TradeDay = updatedObject.TradeDay ?? TradeDay;
            ExpirationYear = updatedObject.ExpirationYear ?? ExpirationYear;
            Multiplier = updatedObject.Multiplier ?? Multiplier;
            Digits = updatedObject.Digits ?? Digits;
            OpenPrice = updatedObject.OpenPrice ?? OpenPrice;
            BidSize = updatedObject.BidSize ?? BidSize;
            AskSize = updatedObject.AskSize ?? AskSize;
            LastSize = updatedObject.LastSize ?? LastSize;
            NetChange = updatedObject.NetChange ?? NetChange;
            StrikePrice = updatedObject.StrikePrice ?? StrikePrice;
            ContractType = updatedObject.ContractType ?? ContractType;
            Underlying = updatedObject.Underlying ?? Underlying;
            ExpirationMonth = updatedObject.ExpirationMonth ?? ExpirationMonth;
            Deliverables = updatedObject.Deliverables ?? Deliverables;
            TimeValue = updatedObject.TimeValue ?? TimeValue;
            ExpirationDay = updatedObject.ExpirationDay ?? ExpirationDay;
            DaystoExpiration = updatedObject.DaystoExpiration ?? DaystoExpiration;
            Delta = updatedObject.Delta ?? Delta;
            Gamma = updatedObject.Gamma ?? Gamma;
            Theta = updatedObject.Theta ?? Theta;
            Vega = updatedObject.Vega ?? Vega;
            Rho = updatedObject.Rho ?? Rho;
            SecurityStatus = updatedObject.SecurityStatus ?? SecurityStatus;
            TheoreticalOptionValue = updatedObject.TheoreticalOptionValue ?? TheoreticalOptionValue;
            UnderlyingPrice = updatedObject.UnderlyingPrice ?? UnderlyingPrice;
            UVExpirationType = updatedObject.UVExpirationType ?? UVExpirationType;
            Mark = updatedObject.Mark ?? Mark;
        }
    }
}
