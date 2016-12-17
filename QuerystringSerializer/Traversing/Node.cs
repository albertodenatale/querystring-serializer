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
            return _value != null ? IsPrimitive() : false;
        }


        private bool IsPrimitive()
        {
            return _value.GetType().IsPrimitive
                || _value is string
                || _value is DateTime;
        }

        public bool HasChildren()
        {
            return !IsLeaf() ? HasChildrenInternal() : false;
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

            if (IsDictionary())
            {
                Dictionary<string, string> d = _value as Dictionary<string, string>;

                foreach (KeyValuePair<string, string> kv in d)
                {
                    yield return new Node(kv.Key, kv.Value);
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
