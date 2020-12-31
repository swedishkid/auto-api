using System;

namespace AutoApi
{
    public sealed class HeaderAttribute : Attribute
    {
        public string HeaderName { get; }

        public HeaderAttribute(string headerName)
        {
            if (string.IsNullOrWhiteSpace(headerName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(headerName));
            }

            HeaderName = headerName;
        }
    }
}