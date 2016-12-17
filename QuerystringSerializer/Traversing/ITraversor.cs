using System;
using System.Collections.Generic;

namespace QuerystringSerializer.Traversing
{
    public interface ITraversor
    {
        Tree Tree { get; set; }

        IEnumerable<Node> GetPairs();
    }
}