using System;

namespace AutoApi
{
    public sealed class FormAttribute : Attribute
    {
        public string Key { get; }

        public FormAttribute(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));
            }

            Key = key;
        }

        public FormAttribute()
        {
        }
    }
}