using System;

namespace AutoApi
{
    public sealed  class CookieAttribute : Attribute
    {
        public string Name { get; }

        public CookieAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
        }

        public CookieAttribute()
        {
        }
    }
}