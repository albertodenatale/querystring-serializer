using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuerystringSerializer.Validation;

namespace QuerystringSerializer.Traversing
{
    public class Node
    {
        private object _value;
        private string _name;

        public object Value
        {
            get
            {
                return _value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Node(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public bool IsLeaf()
        {
            return IsQuerystringPrimitive();
        }
        
        private bool IsQuerystringPrimitive()
        {
            return _value == null
                || _value is char
                || _value is char?
                || _value is bool
                || _value is bool?
                || _value is sbyte
                || _value is sbyte?
                || _value is short
                || _value is short?
                || _value is ushort
                || _value is ushort?
                || _value is string
                || _value is DateTime
                // TODO temporary here, but list to be made explicit
                || _value.GetType().IsPrimitive;
        }

        public bool HasChildren()
        {
            return HasChildrenInternal();
        }

        private bool HasChildrenInternal()
        {
            return !IsLeaf() && 
                   // TODO found on stackoverflow, to be improved
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

            if (IsDictionary())
            {
                IDictionary<string, object> d = _value as IDictionary<string, object>;

                if(d == null)
                {
                    throw new ArgumentException("Only string keys can be contained");
                }

                foreach (KeyValuePair<string, object> kv in d)
                {
                    yield return new Node(kv.Key as string, kv.Value);
                }
            }
            else if (IsEnumerable())
            {
                IEnumerable<object> e = _value as IEnumerable<object>;

                foreach (var n in e.Select(x => new Node(string.Empty, x)))
                {
                    yield return n;
                }
            }
            else
            {
                foreach (var prop in _value.GetType().GetProperties())
                {
                    yield return new Node(prop.Name, prop.GetValue(_value, null));
                }
            }
        }

        private bool IsDictionary()
        {
            return typeof(IDictionary).IsAssignableFrom(_value.GetType());
        }

        private bool IsEnumerable()
        {
            return typeof(IEnumerable).IsAssignableFrom(_value.GetType());
        }
    }
}