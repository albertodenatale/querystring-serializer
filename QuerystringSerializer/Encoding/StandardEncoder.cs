using System.Collections.Generic;
using System.Text;
using QuerystringSerializer.Validation;

namespace QuerystringSerializer.Encoding
{
    public class StandardEncoder : IEncoder
    {
        public string Encode(string value)
        {
            ThrowIf.IsNullOrEmpty(value);

            return EncodeAndMerge(value);
        }

        private static string EncodeAndMerge(string value)
        {
            StringBuilder builder = new StringBuilder();

            foreach(string s in EncodeInternal(value))
            {
                builder.Append(s);
            }

            return builder.ToString();
        }
        
        private static IEnumerable<string> EncodeInternal(string value)
        {
            foreach(char c in value)
            {
                yield return PercentEncoder.Encode(c);
            }
        }
    }
}
