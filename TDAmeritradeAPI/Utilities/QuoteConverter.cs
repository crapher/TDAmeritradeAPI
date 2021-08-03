using System.Collections.Generic;
using TDAmeritradeAPI.Models.API.Quotes;
using Utf8Json;

namespace TDAmeritradeAPI.Utilities
{
    public class QuoteConverter : IJsonFormatter<QuoteList>
    {
        public void Serialize(ref JsonWriter writer, QuoteList value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBeginObject();

            foreach (var p in value.Quotes)
            {
                writer.WritePropertyName(p.symbol);
                formatterResolver.GetFormatterWithVerify<Quote>().Serialize(ref writer, p, formatterResolver);
            }

            writer.WriteEndObject();
        }

        public QuoteList Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var quotesBySymbol = formatterResolver.GetFormatterWithVerify<Dictionary<string, Quote>>().Deserialize(ref reader, formatterResolver);

            var result = new QuoteList();
            var quotes = new List<Quote>();

            foreach (var k in quotesBySymbol.Keys)
            {
                var p = quotesBySymbol[k];
                // set name from property name
                p.symbol = k;
                quotes.Add(p);
            }

            result.Quotes = quotes.ToArray();
            return result;
        }
    }
}
