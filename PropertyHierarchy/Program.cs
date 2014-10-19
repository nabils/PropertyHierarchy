using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultProps = new List<PropertyHierarchy.Property>
                {
                    new PropertyHierarchy.Property("FontInfo:FontFamily", new FontFamily("Segoe UI")),
                    new PropertyHierarchy.Property("FontInfo:FontWeight", FontWeights.Bold),
                    new PropertyHierarchy.Property("FontInfo:FontSize", 12)
                };


            var propertyHierarchy = new PropertyHierarchy(defaultProps);
            propertyHierarchy.Add("FontInfo:FontFamily", new FontFamily("Times New Roman"));

            var x = propertyHierarchy["FontInfo:FontFamily"];

           

            var prop2 = new PropertyHierarchy(propertyHierarchy);

            prop2.Add("FontInfo:FontSize", 16);

            prop2.Remove("FontInfo:FontSize");

            foreach (var property in prop2)
            {
                Debug.WriteLine(property.Key + ": " + property.Value + (property.IsOverriden ? " (Overriden)" : string.Empty));
            }
        }
    }

    public class PropertyHierarchy : IEnumerable<PropertyHierarchy.Property>
    {
        private readonly Dictionary<string, Property> _baseProperties;
        private readonly Dictionary<string, Property> _overrides = new Dictionary<string, Property>();

        public PropertyHierarchy(IEnumerable<Property> baseProperties)
        {
            _baseProperties = baseProperties.ToDictionary(k => k.Key, v => v);
        }

        public void Add(string key, object value)
        {
            _overrides.Add(key, new Property(key, value, true));
        }

        public void Remove(string key)
        {
            _overrides.Remove(key);
        }

        public Property this[string key]
        {
            get
            {
                if (_overrides.ContainsKey(key))
                {
                    return _overrides[key];
                }

                return _baseProperties[key];
            }
        }

        public IEnumerator<Property> GetEnumerator()
        {
            foreach (var property in _baseProperties)
            {
                if (_overrides.ContainsKey(property.Key))
                {
                    yield return _overrides[property.Key];
                }
                else
                {
                    yield return property.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Property
        {
            private readonly string _key;
            private readonly object _value;
            private readonly bool _isOverriden;

            public Property(string key, object value)
            {
                _key = key;
                _value = value;
                _isOverriden = false;
            }

            public Property(string key, object value, bool isOverriden)
            {
                _key = key;
                _value = value;
                _isOverriden = isOverriden;
            }

            public bool IsOverriden
            {
                get { return _isOverriden; }
            }

            public string Key
            {
                get { return _key; }
            }

            public object Value
            {
                get { return _value; }
            }
        }

    }


}
