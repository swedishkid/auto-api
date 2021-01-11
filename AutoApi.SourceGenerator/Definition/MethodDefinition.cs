using System.Collections;
using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class MethodDefinition
    {
        public List<AttributeDefinition> Attributes { get; } = new();
        public string MethodName { get; set; }
        public string ReturnTypeString { get; set; }
        public List<ParameterDefinition> Parameters { get; } = new();
    }
}