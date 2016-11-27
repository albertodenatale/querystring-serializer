using System.Collections.Generic;
using System.Linq;
using QuerystringSerializer.Validation;

namespace QuerystringSerializer.Traversing
{
    public class Node
    {
        private object _value;
        private string _name;

        public Node(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public bool HasValue()
        {
            return _value != null ? IsPrimitive() : false;
        }


        private bool IsPrimitive()
        {
            return _value.GetType().IsPrimitive
                || _value is string;
        }

        public bool HasChildren()
        {
            return !HasValue() ? HasChildrenInternal() : false;
        }

        private bool HasChildrenInternal()
        {
            return _value!= null && 
                   _value.GetType()
                         .GetProperties()
                         .Select(p => p.GetValue(_value, null))
                         .Where(val => val != null)
                         .Select(val => val.ToString())
                         .Where(str => str.Length > 0)
                         .Any();
        }

        public IEnumerable<Node> Children()
        {
            ThrowIf.IsInvalidState(_value, "_value cannot be null");

            foreach (var prop in _value.GetType().GetProperties())
            {
                yield return new Node(prop.Name, prop.GetValue(_value, null));
            }
        }
    }
}
