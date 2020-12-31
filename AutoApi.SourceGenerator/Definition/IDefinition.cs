using System.Collections.Generic;

namespace AutoApi.SourceGenerator.Definition
{
    public interface IDefinition
    {
        List<ClassDefinition> Classes { get; }

        List<InterfaceDefinition> Interfaces { get; }
    }
}