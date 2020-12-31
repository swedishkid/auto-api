using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;
using static Xunit.Assert;

namespace AutoApi.SourceGenerator.Tests
{
    public class MySyntaxReceiverTests
    {
        [Fact]
        public void DetectClasses()
        {
            var source = "class CustomClassName {}";
            var receiver = ReceiveMember(source);
            
            var classCount = receiver.Classes.Count;
            Equal(1, classCount);

            var firstClass = receiver.Classes.First();
            Equal("CustomClassName", firstClass.ClassName);
        }

        [Fact]
        public void DetectInterfaces()
        {
            var source = "interface CustomInterfaceName {}";

            var receiver = ReceiveMember(source);
            Single(receiver.Interfaces);

            var firstInterface = receiver.Interfaces.First();
            Equal("CustomInterfaceName", firstInterface.InterfaceName);
        }

        [Fact]
        public void DetectArgumentsOnInterfaces()
        {
            var source = @"
[Awesome(Cool = ""This is it"")]
public interface CustomInterfaceName
{
}
";

            var receiver = ReceiveMember(source);
            
            Single(receiver.Interfaces);

            var firstInterface = receiver.Interfaces.First();
            NotNull(firstInterface);

            Single(firstInterface.Attributes);

            var firstAttribute = firstInterface.Attributes.First();
            NotNull(firstAttribute);

            Equal("Awesome", firstAttribute.AttributeName);

            Single(firstAttribute.Arguments);

            var firstArgument = firstAttribute.Arguments.First();

            NotNull(firstArgument);

            Equal("Cool", firstArgument.ArgumentName);
            Equal("\"This is it\"", firstArgument.ArgumentExpression);
        }


        [Fact]
        public void DetectArgumentsOnClasses()
        {
            var source = @"
[Awesome(Cool = ""This is it"")]
public class CustomClassName
{
}
";

            var receiver = ReceiveMember(source);
            Single(receiver.Classes);

            var firstInterface = receiver.Classes.First();
            NotNull(firstInterface);

            Single(firstInterface.Attributes);

            var firstAttribute = firstInterface.Attributes.First();
            NotNull(firstAttribute);

            Equal("Awesome", firstAttribute.AttributeName);

            Single(firstAttribute.Arguments);

            var firstArgument = firstAttribute.Arguments.First();

            NotNull(firstArgument);

            Equal("Cool", firstArgument.ArgumentName);
            Equal("\"This is it\"", firstArgument.ArgumentExpression);
        }

        [Fact]
        public void DetectPropertiesOnClass()
        {
            var source = @"
public class CustomClassName
{
    public string CustomPropertyName { get; set; }
}
";

            var receiver = ReceiveMember(source);

            Single(receiver.Classes);
            var firstClass = receiver.Classes.First();
            NotNull(firstClass);
            Equal("CustomClassName", firstClass.ClassName);
            NotNull(firstClass.Properties);
            Single(firstClass.Properties);

            var firstProperty = firstClass.Properties.First();
            NotNull(firstProperty);
            Equal("CustomPropertyName", firstProperty.PropertyName);
            Equal("string", firstProperty.PropertyType);
        }

        [Fact]
        public void DetectClassesWithoutAttributes()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
public class CustomClassName
{
}
");
            var syntaxNode = (CompilationUnitSyntax) syntaxTree.GetRoot();
            var receiver = new MySyntaxReceiver();
            receiver.OnVisitSyntaxNode(syntaxNode.Members.First());
        }

        [Fact]
        public void DetectInterfaceWithoutAttributes()
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(@"
public interface CustomInterfaceName
{
}
");
            var syntaxNode = (CompilationUnitSyntax) syntaxTree.GetRoot();
            var receiver = new MySyntaxReceiver();
            receiver.OnVisitSyntaxNode(syntaxNode.Members.First());
        }

        [Fact]
        public void DetectClassWithPropertiesWithNoAttribute()
        {
            var source = @"
public class CustomClass
{
    public string CustomProperty { get; set; }
}
";
            var receiver = ReceiveMember(source);
            Single(receiver.Classes);
        }

        [Fact]
        public void CanDetectAttributeWithUnnamedParameter()
        {
            var source = @"
[Fedt(""WithOneUnnamedArgument"")]
public class CustomClass
{
}
";
            var receiver = ReceiveMember(source);
            Single(receiver.Classes);
            var attribute = receiver.Classes.First().Attributes.First();
            Single(attribute.Arguments);
            var argument = attribute.Arguments.First();
            Equal("\"WithOneUnnamedArgument\"", argument.ArgumentExpression);
        }


        [Fact]
        public void DetectAttributeWithNoParameter()
        {
            var source = @"
[Fedt]
public class CustomClass
{
}
";
            var receiver = ReceiveMember(source);
            Single(receiver.Classes);
            Single(receiver.Classes.First().Attributes);
            var attribute = receiver.Classes.First().Attributes.First();
            NotNull(attribute);
            Equal("Fedt", attribute.AttributeName);
            NotNull(attribute.Arguments);
            Empty(attribute.Arguments);
        }

        [Fact]
        public void DetectFieldsOnClasses()
        {
            var source = @"
public class CustomClassName 
{
    private string fieldName;
}
";
            var receiver = ReceiveMember(source);
            NotNull(receiver.Classes);
            Single(receiver.Classes);
            var firstClass = receiver.Classes.First();
            NotNull(firstClass.Fields);
            Single(firstClass.Fields);
            var firstField = firstClass.Fields.First();
            Equal("fieldName", firstField.FieldName);
            Equal("string", firstField.FieldType);
        }

        private MySyntaxReceiver ReceiveMember(string source)
        {
            var node = MemberFromSource(source);
            var receiver = new MySyntaxReceiver();
            receiver.OnVisitSyntaxNode(node);
            return receiver;
        }
        
        private static SyntaxNode MemberFromSource(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            var syntaxNode = (CompilationUnitSyntax) syntaxTree.GetRoot();
            return syntaxNode.Members.First();
        }
    }
}
