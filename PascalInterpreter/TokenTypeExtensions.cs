using System.Linq;

namespace PascalInterpreter
{
    public static class TokenTypeExtensions
    {
        public static bool IsOperator(this TokenType x)
        {
            var operators = new[] { TokenType.Minus, TokenType.Plus };
            return operators.Contains(x);
        }
    }
}
