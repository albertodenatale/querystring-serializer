using System;
using System.Collections.Generic;

namespace QuerystringSerializer.Traversing
{
    public class PreorderTraversor : ITraversor
    {
        public Tree Tree { get; set; }

        public IEnumerable<Node> GetPairs()
        {
            if(Tree == null)
            {
                throw new InvalidOperationException("The traversor should be initialised with a tree");
            }

            return GetPairsInternal(Tree.Root);
        }

        private IEnumerable<Node> GetPairsInternal(Node node)
        {
            if(node.IsLeaf())
            {
                yield return node;
            }
            else if (node.HasChildren())
            {
                foreach (var child in node.Children())
                {
                    foreach (var nephew in GetPairsInternal(child))
                    {
                        yield return nephew;
                    }
                }
            }
        }
    }
}
