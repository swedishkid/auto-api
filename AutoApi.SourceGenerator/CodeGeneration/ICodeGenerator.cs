namespace AutoApi.SourceGenerator.CodeGeneration
{
    public interface ICodeGenerator
    {
        public void GenerateCode(CodeGenerationContext context);
    }
}