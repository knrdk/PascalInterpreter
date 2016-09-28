using System;
using System.Collections.Generic;

namespace PascalInterpreter
{
    public class Parser
    {
        private State CurrentState = State.Start;
        private Token Number;
        private Token Operator;

        public void Parse(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                switch (CurrentState)
                {
                    case State.Start:
                        StartState(token);
                        break;
                    case State.NumberReceived:
                        NumberReceivedState(token);
                        break;
                    case State.OperatorReceived:
                        OperatorReceivedState(token);
                        break;
                    case State.End:
                        return;
                }
            }
        }

        public object GetResult()
        {
            if (CurrentState == State.End)
            {
                return int.Parse(Number.Value);
            }
            throw new Exception("Cannot get result before parsing");
        }

        private void StartState(Token token)
        {
            switch (token.Type)
            {
                case TokenType.EndOfFile:
                    CurrentState = State.End;
                    break;
                case TokenType.Integer:
                    Number = token;
                    CurrentState = State.NumberReceived;
                    break;
                default:
                    throw new Exception();
            }
        }

        private void NumberReceivedState(Token token)
        {
            if (token.Type.IsOperator())
            {
                Operator = token;
                CurrentState = State.OperatorReceived;
                return;
            }
            if (token.Type == TokenType.EndOfFile)
            {
                CurrentState = State.End;
                return;
            }
            throw new Exception();
        }

        private void OperatorReceivedState(Token token)
        {
            if (token.Type == TokenType.Integer)
            {
                var aggregator = GetAggregateFunction();
                Number = aggregator(Number, token);
                CurrentState = State.NumberReceived;
            }
        }

        private Func<Token, Token, Token> GetAggregateFunction()
        {
            switch (Operator.Type)
            {
                case TokenType.Minus:
                    return (x, y) =>
                    {
                        var left = int.Parse(x.Value);
                        var right = int.Parse(y.Value);
                        var newValue = left - right;
                        return new Token(TokenType.Integer, newValue.ToString());
                    };
                case TokenType.Plus:
                    return (x, y) =>
                    {
                        var left = int.Parse(x.Value);
                        var right = int.Parse(y.Value);
                        var newValue = left + right;
                        return new Token(TokenType.Integer, newValue.ToString());
                    };
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
