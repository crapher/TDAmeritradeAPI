using System.Collections.Generic;
using TDAmeritradeAPI.Models.API.Instruments;
using Utf8Json;

namespace TDAmeritradeAPI.Utilities
{
    public class InstrumentConverter : IJsonFormatter<InstrumentList>
    {
        public void Serialize(ref JsonWriter writer, InstrumentList value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBeginObject();

            foreach (var p in value.Instruments)
            {
                writer.WritePropertyName(p.symbol);
                formatterResolver.GetFormatterWithVerify<Instrument>().Serialize(ref writer, p, formatterResolver);
            }

            writer.WriteEndObject();
        }

        public InstrumentList Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var instrumentsBySymbol = formatterResolver.GetFormatterWithVerify<Dictionary<string, Instrument>>().Deserialize(ref reader, formatterResolver);

            var result = new InstrumentList();
            var instruments = new List<Instrument>();

            foreach (var k in instrumentsBySymbol.Keys)
            {
                var p = instrumentsBySymbol[k];
                // set name from property name
                p.symbol = k;
                instruments.Add(p);
            }

            result.Instruments = instruments.ToArray();
            return result;
        }
    }
}
