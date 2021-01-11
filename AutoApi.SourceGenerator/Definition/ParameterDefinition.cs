using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class ParameterDefinition
    {
        public ParameterDefinition(string parameterName, string parameterType)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterName));
            if (string.IsNullOrWhiteSpace(parameterType))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(parameterType));
            ParameterName = parameterName;
            ParameterType = parameterType;
        }

        public List<AttributeDefinition> Attributes { get; } = new();
        
        public string ParameterName { get; }
        
        public string ParameterType { get; }
    }
}