﻿using System;
using System.Collections.Generic;

namespace PascalInterpreter
{
    public class Interpreter
    {
        private readonly string Program;
        private int CurrentPosition;
        private Token CurrentToken;

        private static readonly Dictionary<string, Token> TokensMap = InitializeTokensMap();

        public Interpreter(string program)
        {
            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            Program = program;
            CurrentPosition = 0;
            CurrentToken = null;
        }

        public Token GetNextToken()
        {
            CurrentToken = null;

            CheckEndOfProgram();

            var tokenLength = 1;
            int? lastNumber = null;
            while (CurrentToken == null && (CurrentPosition + tokenLength <= Program.Length))
            {
                var tokenCandidate = Program.Substring(CurrentPosition, tokenLength);

                if (TryParseOperatorToken(tokenCandidate))
                {
                    break;
                }

                int currentNumber;
                var isNumber = int.TryParse(tokenCandidate, out currentNumber);
                if (isNumber)
                {
                    lastNumber = currentNumber;
                }
                else if (lastNumber.HasValue)
                {
                    var token = new Token(TokenType.Integer, lastNumber.Value.ToString());
                    CurrentPosition += (tokenLength - 1);
                    CurrentToken = token;
                    break;
                }

                tokenLength++;
            }
            TryParseLastTokenAsNumber(lastNumber);

            if (CurrentToken == null)
            {
                throw new Exception();
            }

            return CurrentToken;
        }

        private void CheckEndOfProgram()
        {
            if (CurrentPosition == Program.Length)
            {
                CurrentToken = new Token(TokenType.EndOfFile, string.Empty);
            }
        }

        private bool TryParseOperatorToken(string substring)
        {
            if (TokensMap.ContainsKey(substring))
            {
                var token = TokensMap[substring];
                CurrentPosition += substring.Length;
                CurrentToken = token;
                return true;
            }
            return false;
        }

        private void TryParseLastTokenAsNumber(int? lastNumber)
        {
            if (CurrentToken == null && lastNumber.HasValue)
            {
                var token = new Token(TokenType.Integer, lastNumber.Value.ToString());
                CurrentPosition = Program.Length;
                CurrentToken = token;
            }
        }

        private static Dictionary<string, Token> InitializeTokensMap()
        {
            return new Dictionary<string, Token>()
            {
                ["+"] = new Token(TokenType.Plus, "+"),
                ["-"] = new Token(TokenType.Minus, "-")
            };
        }
    }
}
