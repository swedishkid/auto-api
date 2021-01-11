using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public class ConstructorDefinition
    {
        public List<AttributeDefinition> Attributes { get; } = new ();
        
        public string Name { get; set; }

        public List<ParameterDefinition> Parameters { get; } = new();
    }
}