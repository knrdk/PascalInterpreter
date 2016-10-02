using System;
using System.Collections.Generic;
using Model;
using AbstractSyntaxTree;

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
            return result.Evaluate();
        }

        private AbstractNode Expr()
        {
            var result = Term();
            while (_Enumerator.Current.Type.IsAdditionOrSubtraction())
            {
                var token = _Enumerator.Current;
                Eat(token.Type);
                result = new BinaryOperatorNode(result, Term(), token);
            }
            return result;
        }

        private AbstractNode Term()
        {
            var result = Factor();
            while (_Enumerator.Current.Type.IsMultiplicationOrDivision())
            {
                var token = _Enumerator.Current;
                Eat(token.Type);
                result = new BinaryOperatorNode(result, Factor(), token);
            }
            return result;
        }

        private AbstractNode Factor()
        {
            var token = _Enumerator.Current;
            if(token.Type == TokenType.BracketLeft)
            {
                Eat(TokenType.BracketLeft);
                var output = Expr();
                Eat(TokenType.BracketRight);
                return output;
            }
            else
            {
                Eat(TokenType.Integer);
                return new NumericNode(token);
            }
        }

        private void Eat(TokenType tokenType)
        {
            if(_Enumerator.Current.Type == tokenType)
            {
                _Enumerator.MoveNext();
            }
        }
    }
}
