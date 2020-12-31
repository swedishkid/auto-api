using System;

namespace AutoApi
{
    public sealed class ServiceAttribute : Attribute
    {
        public string Alias { get; }

        public ServiceAttribute()
        {
        }
        
        public ServiceAttribute(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(alias));
            }

            Alias = alias;
        }
    }
}