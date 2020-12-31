using System;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class InterfaceDefinition
    {
        public InterfaceDefinition(string interfaceName)
        {
            if (string.IsNullOrWhiteSpace(interfaceName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(interfaceName));
            InterfaceName = interfaceName;
        }
        
        public string InterfaceName { get; }
        
        public List<AttributeDefinition> Attributes { get; } = new();

        public List<PropertyDefinition> Properties { get; } = new();
    }
}