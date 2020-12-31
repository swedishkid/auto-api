using System.Collections.Generic;
using AutoApi.SourceGenerator.Definition;

namespace AutoApi.SourceGenerator.CodeGeneration
{
    public class CodeGenerationContext
    {
        public CodeGenerationContext(IDefinition definition)
        {
            Definition = definition;
        }
        
        public IDefinition Definition { get; }
        public List<CodeFile> Files { get; set; } = new();
    }
}