using System;
using QuerystringSerializer.Encoding;
using QuerystringSerializer.Pairing;
using QuerystringSerializer.Traversing;

namespace QuerystringSerializer
{
    public static class QuerystringConvert
    {
        private static ITraversor _traversor;
        private static IPairer _pairer;
        private static IEncoder _encoder;

        static QuerystringConvert()
        {
            _traversor = new PreorderTraversor();
            _pairer = new StandardPairer();
            _encoder = new StandardEncoder();
        }

        public static string Serialize(object input)
        {
            // quick proof of concept

            //string queryString = string.Empty;
            //_traversor.Tree = new Tree() { Root = new Node(string.Empty, input) };
            //foreach(var p in _traversor.GetPairs())
            //{
            //    string current=string.Concat(_pairer.Pair(_encoder.Encode(p.Name), _encoder.Encode(p.Value.ToString())));
            //    queryString = string.Format("{0}{1}",current, queryString);
            //}

            // return queryString;
            throw new NotImplementedException();
        }
    }
}
