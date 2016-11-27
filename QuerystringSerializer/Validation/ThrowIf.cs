using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerystringSerializer.Validation
{
    internal static class ThrowIf
    {
        public static void IsNullOrEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }
        }

        public static void IsInvalidState(object value, string message)
        {
            if (value == null)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
