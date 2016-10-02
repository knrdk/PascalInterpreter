namespace Model
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type)
        {
            Type = type;
        }

        public Token(TokenType type, string value) : this(type)
        {
            Value = value;
        }

        public override string ToString() => $"Token({Type}: {Value})";
    }
}
