using System;
using System.Collections.Generic;

namespace PascalInterpreter
{
    public class Parser
    {
        IEnumerator<Token> _Enumerator;
        
        public Parser(IEnumerable<Token> tokens)
        {
            if (tokens == null)
            {
                throw new Exception("Tokens cannot be null");
            }
            _Enumerator = tokens.GetEnumerator();
        }

        public object Parse()
        {
            _Enumerator.MoveNext();
            var result = Expr();
            return result;
        }

        private int Expr()
        {
            var result = Term();
            while (_Enumerator.Current.Type.IsAdditionOrSubtraction())
            {
                var token = _Enumerator.Current;
                Eat(token.Type);
                var aggregateOperation = GetAggregateFunction(token);
                result = aggregateOperation(result, Term());
            }
            return result;
        }

        private int Term()
        {
            var result = Factor();
            while (_Enumerator.Current.Type.IsMultiplicationOrDivision())
            {
                var token = _Enumerator.Current;
                Eat(token.Type);
                var aggregateOperation = GetAggregateFunction(token);
                result = aggregateOperation(result, Term());
            }
            return result;
        }

        private int Factor()
        {
            var token = _Enumerator.Current;
            Eat(TokenType.Integer);
            return int.Parse(token.Value);
        }

        private void Eat(TokenType tokenType)
        {
            if(_Enumerator.Current.Type == tokenType)
            {
                _Enumerator.MoveNext();
            }
        }

        private Func<int, int, int> GetAggregateFunction(Token operatorToken)
        {
            switch (operatorToken.Type)
            {
                case TokenType.Minus:
                    return (x, y) => x - y;
                case TokenType.Plus:
                    return (x, y) => x + y;
                case TokenType.Multiplication:
                    return (x, y) => x * y;
                case TokenType.Division:
                    return (x, y) => x / y;
                default:
                    throw new Exception();
            }
        }

        private enum State
        {
            Unknown = 0,
            Start,
            NumberReceived,
            OperatorReceived,
            End
        }
    }
}
