using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class PropertyDefinition
    {
        public PropertyDefinition(string propertyName, string propertyType)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyName));
            }

            if (string.IsNullOrWhiteSpace(propertyType))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(propertyType));
            }

            PropertyName = propertyName;
            PropertyType = propertyType;
        }
        
        public string PropertyName { get; }
        
        public string PropertyType { get; }

        public List<AttributeDefinition> Attributes { get; } = new();
    }
}