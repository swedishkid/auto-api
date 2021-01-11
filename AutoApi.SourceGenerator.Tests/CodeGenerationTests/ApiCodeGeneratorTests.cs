using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using AutoApi.SourceGenerator.CodeGeneration;
using AutoApi.SourceGenerator.CodeGeneration.Api;
using AutoApi.SourceGenerator.Definition;
using Xunit;

namespace AutoApi.SourceGenerator.Tests.CodeGenerationTests
{
    [UseReporter(typeof(DiffReporter))]
    public class ApiCodeGeneratorTests
    {
        [Fact]
        public void MatchesHttpAttributeNames()
        {
            var attributeNames = ApiCodeGenerator.GetAttributeNames();
            Approvals.VerifyAll(attributeNames, "Name");
        }
        
        [Fact]
        public void CanMatchRouteAttributeOnClass()
        {
            var definition = new ClassDefinition("TestClass");
            
            definition.Attributes.Add(new AttributeDefinition("Route"));

            var classes = new List<ClassDefinition>()
            {
                definition
            };
            
            var candidateClasses = ApiCodeGenerator.GetCandidateClasses(classes);
            Assert.Single(candidateClasses);
        }
        
        [Theory]
        [InlineData("HttpGet")]
        [InlineData("HttpDelete")]
        [InlineData("HttpPut")]
        [InlineData("HttpPatch")]
        [InlineData("HttpPost")]        
        [InlineData("HttpGetAttribute")]
        [InlineData("HttpDeleteAttribute")]
        [InlineData("HttpPutAttribute")]
        [InlineData("HttpPatchAttribute")]
        [InlineData("HttpPostAttribute")]
        public void CanMatchAttributeOnMethod(string attributeName)
        {
            var classDefinition = new ClassDefinition("TestClass");

            var methodDefinition = new MethodDefinition();
            methodDefinition.Attributes.Add(new AttributeDefinition(attributeName));
            
            classDefinition.Methods.Add(methodDefinition);
            
            var classes = new List<ClassDefinition>()
            {
                classDefinition
            };
            
            var candidateClasses = ApiCodeGenerator.GetCandidateClasses(classes);
            Assert.Single(candidateClasses);
        }
    }
}