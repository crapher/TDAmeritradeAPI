using System.Collections.Generic;
using TDAmeritradeAPI.Models.Quotes;
using Utf8Json;

namespace TDAmeritradeAPI.Utilities
{
    public class QuoteConverter : IJsonFormatter<QuoteList>
    {
        public void Serialize(ref JsonWriter writer, QuoteList value, IJsonFormatterResolver formatterResolver)
        {
            var list = (QuoteList)value;

            writer.WriteBeginObject();

            foreach (var p in list.Quotes)
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
            result.Quotes = new List<Quote>();

            foreach (var k in quotesBySymbol.Keys)
            {
                var p = quotesBySymbol[k];
                // set name from property name
                p.symbol = k;
                result.Quotes.Add(p);
            }

            return result;
        }
    }
}
