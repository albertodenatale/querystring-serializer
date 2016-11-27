using System.Collections.Generic;

namespace QuerystringSerializer.Traversing
{
    public class PreorderTraversor : ITraversor
    {
        Node _root;

        public PreorderTraversor(Node root)
        {
            _root = root;
        }

        public IEnumerable<Node> GetPairs()
        {
            return GetPairsInternal(_root);
        }

        private IEnumerable<Node> GetPairsInternal(Node node)
        {
            if(node.HasValue())
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
