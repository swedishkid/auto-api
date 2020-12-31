using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class FieldDefinition
    {
        public string FieldName { get; }
        public string FieldType { get; }
        
        public List<AttributeDefinition> Attributes { get; } = new();

        public FieldDefinition(string fieldName, string fieldType)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldName));
            }

            if (string.IsNullOrWhiteSpace(fieldType))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fieldType));
            }

            FieldName = fieldName;
            FieldType = fieldType;
        }
    }
}