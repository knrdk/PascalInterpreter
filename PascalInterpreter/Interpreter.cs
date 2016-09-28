using System;
using System.Collections.Generic;

namespace PascalInterpreter
{
    public class Interpreter
    {
        private readonly Lexer _Lexer;

        public Interpreter(string program)
        {
            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            _Lexer = new Lexer(program);
        }

        public object Expr()
        {
            Func<Token, Token, int> Operator;

            var left = _Lexer.GetNextToken();
            var op = _Lexer.GetNextToken();
            if (op.Type == TokenType.Plus)
            {
                Operator = (x, y) => int.Parse(x.Value) + int.Parse(y.Value);
            }
            else if (op.Type == TokenType.Minus)
            {
                Operator = (x, y) => int.Parse(x.Value) - int.Parse(y.Value);
            }
            else
            {
                throw new InvalidOperationException("Invalid Token");
            }

            var right = _Lexer.GetNextToken();
            
            return Operator(left, right);
        }
    }
}
