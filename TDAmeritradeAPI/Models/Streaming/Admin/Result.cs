using System.Runtime.Serialization;

namespace TDAmeritradeAPI.Models.Streaming.Admin
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640565
    public class Result
    {
        // Filled in deserialization
        [DataMember(Name = "service")]
        public string Service { get; set; }

        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "msg")]
        public string Message { get; set; }

        public override string ToString() => $"{Service}: {Code} - {Message}";
    }
}
