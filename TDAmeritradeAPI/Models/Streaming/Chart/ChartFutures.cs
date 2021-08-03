using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.Chart
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640592
    public class ChartFutures
    {
        [DataMember(Name = "1")]
        public long ChartTime { get; set; }

        [DataMember(Name = "2")]
        public double OpenPrice { get; set; }

        [DataMember(Name = "3")]
        public double HighPrice { get; set; }

        [DataMember(Name = "4")]
        public double LowPrice { get; set; }

        [DataMember(Name = "5")]
        public double ClosePrice { get; set; }

        [DataMember(Name = "6")]
        public double Volume { get; set; }

        [DataMember(Name = "key")]
        public string Symbol { get; set; }
    }
}
