using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoApi.SourceGenerator.Definition
{
    public class ClassDefinition
    {
        public ClassDefinition(string className, string namespaceName = null)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(className));
            }

            ClassName = className;
            NamespaceName = namespaceName;
        }

        public string NamespaceName { get; }

        public string ClassName { get; }
        
        public List<AttributeDefinition> Attributes { get; } = new();
        
        public List<PropertyDefinition> Properties { get; } = new();
        
        public List<FieldDefinition> Fields { get; } = new();
        
        public List<MethodDefinition> Methods { get; } = new();

        public List<ConstructorDefinition> Constructors { get; } = new();
    }
}