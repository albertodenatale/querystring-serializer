namespace QuerystringSerializer.Pairing
{
    public class StandardPairer : IPairer
    {
        public string Pair(string propertyName, string value)
        {
            return string.Concat("&", propertyName, "=", value);
        }
    }
}
