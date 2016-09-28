using System;
using System.Collections.Generic;
using System.Linq;

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
            var tokens = _Lexer.ReadTokens();
            var parser = new Parser();
            parser.Parse(tokens);
            return parser.GetResult();
        }
    }
}
