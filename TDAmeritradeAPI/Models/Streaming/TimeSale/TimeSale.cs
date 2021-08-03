using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.TimeSale
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640628
    public class TimeSale
    {
        [DataMember(Name = "1")]
        public long TradeTime { get; set; }

        [DataMember(Name = "2")]
        public double Price { get; set; }

        [DataMember(Name = "3")]
        public double Size { get; set; }

        [DataMember(Name = "4")]
        public long LastSequence { get; set; }

        [DataMember(Name = "seq")]
        public long Sequence { get; set; }

        [DataMember(Name = "key")]
        public string Symbol { get; set; }
    }
}
