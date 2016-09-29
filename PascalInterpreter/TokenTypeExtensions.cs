namespace PascalInterpreter
{
    public static class TokenTypeExtensions
    {
        public static bool IsOperator(this TokenType x)
        {
            return x.IsAdditionOrSubtraction() || x.IsMultiplicationOrDivision();
        }

        public static bool IsAdditionOrSubtraction(this TokenType x)
        {
            return x == TokenType.Plus || x == TokenType.Minus;
        }

        public static bool IsMultiplicationOrDivision(this TokenType x)
        {
            return x == TokenType.Multiplication || x == TokenType.Division;
        }
    }
}
