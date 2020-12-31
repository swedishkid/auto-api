using System;
using System.Collections.Immutable;
using AutoApi.SourceGenerator.CodeGeneration;
using AutoApi.SourceGenerator.Definition;
using Microsoft.CodeAnalysis;

namespace AutoApi.SourceGenerator
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a factory that can create our custom syntax receiver
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }

        /// <summary>
        /// Called to perform source generation. 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxReceiver = (MySyntaxReceiver) context.SyntaxReceiver;
            Execute(syntaxReceiver, context.AdditionalFiles, context.AddSource);
        }

        public void Execute(IDefinition receiver, ImmutableArray<AdditionalText> additionalFiles, Action<string, string> addSource)
        {
            var manager = new CodeGenerationManager(receiver);
            var codeFiles = manager.GenerateCode();
            
            foreach (var codeFile in codeFiles)
            {
                addSource(codeFile.HintName, codeFile.SourceCode);
            }
        }
    }
}