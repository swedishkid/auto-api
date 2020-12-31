using System;

namespace AutoApi
{
    public sealed class ClaimAttribute : Attribute
    {
        public string ClaimName { get; }

        public ClaimAttribute(string claimName)
        {
            if (string.IsNullOrWhiteSpace(claimName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(claimName));
            }

            ClaimName = claimName;
        }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}