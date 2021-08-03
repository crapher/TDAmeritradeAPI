using System.Collections.Generic;
using System.Runtime.Serialization;
using TDAmeritradeAPI.Utilities;
using Utf8Json;

namespace TDAmeritradeAPI.Models.Streaming.Book
{
    // https://developer.tdameritrade.com/content/streaming-data#_Toc504640612
    // This is not part of the official document. 
    public class Book
    {
        [DataMember(Name = "1")]
        public long TradeTime { get; set; }

        [DataMember(Name = "2")]
        [JsonFormatter(typeof(BookQuoteConverter))]
        public List<BookQuote> Bids { get; set; }

        [DataMember(Name = "3")]
        [JsonFormatter(typeof(BookQuoteConverter))]
        public List<BookQuote> Asks { get; set; }

        [DataMember(Name = "key")]
        public string Symbol { get; set; }
    }

    public class BookQuote
    {
        [DataMember(Name = "0")]
        public string Market { get; set; }
        [DataMember(Name = "1")]
        public long Size { get; set; }
        [DataMember(Name = "2")]
        public long Sequence { get; set; }

        // Filled in deserialization to avoid hierarchy
        public double Price { get; set; }
        public int PriceGroup { get; set; }

        public override string ToString() => $"{Market} - Price: {Price} - Size: {Size} - Group: {PriceGroup}";
    }

    // Internal Used for faster deserialization
    public class BookQuoteContainer
    {
        [DataMember(Name = "0")]
        public double Price { get; set; }

        [DataMember(Name = "3")]
        public List<BookQuote> MarketQuote { get; set; }
    }
}
