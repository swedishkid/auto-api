using System.Collections.Generic;
using System.Collections.Immutable;
using AutoApi.SourceGenerator.Definition;
using Microsoft.CodeAnalysis;
using Xunit;

namespace AutoApi.SourceGenerator.Tests
{
    public class MySourceGeneratorTests
    {
        public class FakeDefinition : IDefinition
        {
            public List<ClassDefinition> Classes { get; } = new ();

            public List<InterfaceDefinition> Interfaces { get; } = new();
        }
        
        [Fact]
        public void TestExecute()
        {
            var log = new List<string>();
            
            void AddSource(string fileName, string _)
            {
                log.Add(fileName + " was added.");
            }
            
            MySourceGenerator generator = new();
            var fakeDefinition = new FakeDefinition();
            var additionalFiles = ImmutableArray<AdditionalText>.Empty;
            generator.Execute(fakeDefinition, additionalFiles, AddSource);
        }
    }
}