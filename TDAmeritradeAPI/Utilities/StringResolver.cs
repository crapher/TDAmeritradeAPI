using System;
using System.Text;
using Utf8Json;

namespace TDAmeritradeAPI.Utilities
{
    public class StringResolver : IJsonFormatter<string>
    {
        public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public string Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            try
            {
                var buffer = reader.GetBufferUnsafe();

                var startOffset = reader.GetCurrentOffsetUnsafe();
                reader.ReadNextBlock();
                var endOffset = reader.GetCurrentOffsetUnsafe();

                byte[] rawResult = new byte[endOffset - startOffset];
                Array.Copy(buffer, startOffset, rawResult, 0, endOffset - startOffset);

                return Encoding.UTF8.GetString(rawResult);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
