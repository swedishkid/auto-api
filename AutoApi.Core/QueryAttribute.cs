using System;

namespace AutoApi
{
    public sealed class QueryAttribute : Attribute
    {
        public string Key { get; }

        public QueryAttribute(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));
            }

            Key = key;
        }

        public QueryAttribute()
        {
        }
    }
}