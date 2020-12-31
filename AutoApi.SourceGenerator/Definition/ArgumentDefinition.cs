using System;

namespace AutoApi.SourceGenerator.Definition
{
    public class ArgumentDefinition
    {
        public string ArgumentName { get; }
        public string ArgumentExpression { get; }

        public ArgumentDefinition(string argumentName, string argumentExpression)
        {
            if (string.IsNullOrWhiteSpace(argumentName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(argumentName));
            }

            if (string.IsNullOrWhiteSpace(argumentExpression))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(argumentExpression));
            }

            ArgumentName = argumentName;
            ArgumentExpression = argumentExpression;
        }
        
        public ArgumentDefinition(string argumentExpression)
        {

            if (string.IsNullOrWhiteSpace(argumentExpression))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(argumentExpression));
            }

            ArgumentExpression = argumentExpression;
        }
    }
}