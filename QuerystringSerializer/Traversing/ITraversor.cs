using System;
using System.Collections.Generic;

namespace QuerystringSerializer.Traversing
{
    public interface ITraversor
    {
        IEnumerable<Node> GetPairs();
    }
}