namespace QuerystringSerializer.Pairing
{
    public interface IPairer
    {
        string Pair(string propertyName, string value);
    }
}
