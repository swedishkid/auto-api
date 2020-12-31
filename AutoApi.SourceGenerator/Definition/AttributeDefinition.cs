using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class AttributeDefinition
    {
        public AttributeDefinition(string attributeName)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(attributeName));
            }

            AttributeName = attributeName;
        }
        
        public string AttributeName { get; }

        public List<ArgumentDefinition> Arguments { get; } = new();
    }
}