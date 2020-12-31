using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class ClassDefinition
    {
        public ClassDefinition(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(className));
            }

            ClassName = className;
        }
        
        public string ClassName { get; }
        
        public List<AttributeDefinition> Attributes { get; } = new();
        
        public List<PropertyDefinition> Properties { get; } = new();
        
        public List<FieldDefinition> Fields { get; } = new();
    }
}