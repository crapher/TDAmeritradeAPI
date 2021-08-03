using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.AccountActivity
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640582
    public class AccountActivity
    {
        [DataMember(Name = "0")]
        public string SubscriptionKey { get; set; }

        [DataMember(Name = "1")]
        public string Account { get; set; }

        [DataMember(Name = "2")]
        public string MessageType { get; set; }

        [DataMember(Name = "3")]
        public string MessageData { get; set; }
    }
}
