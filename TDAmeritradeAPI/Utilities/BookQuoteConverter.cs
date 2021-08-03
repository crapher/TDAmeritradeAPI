using System;
using System.Collections.Generic;
using TDAmeritradeAPI.Models.Streaming.Book;
using Utf8Json;

namespace TDAmeritradeAPI.Utilities
{
    public class BookQuoteConverter : IJsonFormatter<List<BookQuote>>
    {
        public void Serialize(ref JsonWriter writer, List<BookQuote> value, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public List<BookQuote> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var result = new List<BookQuote>();

            var bookQuotes = formatterResolver.GetFormatterWithVerify<BookQuoteContainer[]>().Deserialize(ref reader, formatterResolver);

            var priceGroup = 1;
            foreach (var bookQuote in bookQuotes)
            {
                foreach (var marketQuote in bookQuote.MarketQuote)
                {
                    marketQuote.Price = bookQuote.Price;
                    marketQuote.PriceGroup = priceGroup;
                    result.Add(marketQuote);
                }

                priceGroup++;
            }

            return result;
        }
    }
}
