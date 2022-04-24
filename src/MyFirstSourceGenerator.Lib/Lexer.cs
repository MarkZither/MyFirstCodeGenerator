using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Tokens = System.Collections.Generic.IEnumerable<MyFirstSourceGenerator.Lib.Token>;

namespace MyFirstSourceGenerator.Lib
{
    public static class Lexer
    {
        public static void PrintTokens(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                System.Console.WriteLine($"{token.Line}, {token.Column}, {token.Type}, {token.Value}");
            }
        }

        static (TokenType, string)[] tokenStrings = {
            (TokenType.EOL,         @"(\r\n|\r|\n)"),
            (TokenType.Spaces,      @"\s+"),
            (TokenType.Number,      @"[+-]?((\d+\.?\d*)|(\.\d+))"),
            (TokenType.Identifier,  @"[_a-zA-Z][`'""_a-zA-Z0-9]*"),
            (TokenType.Operation,   @"[\+\-/\*]"),
            (TokenType.OpenParens,  @"[([{]"),
            (TokenType.CloseParens, @"[)\]}]"),
            (TokenType.Equal,       @"="),
            (TokenType.Comma,       @","),
            (TokenType.Sum,         @"∑")
        };

        static IEnumerable<(TokenType, Regex)> tokenExpressions =
            tokenStrings.Select(
                t => (t.Item1, new Regex($"^{t.Item2}", RegexOptions.Compiled | RegexOptions.Singleline)));

        public static Tokens Tokenize(string source)
        {
            var currentLine = 1;
            var currentColumn = 1;

            while (source.Length > 0)
            {

                var matchLength = 0;
                var tokenType = TokenType.None;
                var value = "";

                foreach (var (type, rule) in tokenExpressions)
                {
                    var match = rule.Match(source);
                    if (match.Success)
                    {
                        matchLength = match.Length;
                        tokenType = type;
                        value = match.Value;
                        break;
                    }
                }

                if (matchLength == 0)
                {
                    throw new Exception($"Unrecognized symbol '{source[currentLine - 1]}' at index {currentLine - 1} (line {currentLine}, column {currentColumn}).");
                }
                else
                {
                    if (tokenType != TokenType.Spaces)
                        yield return new Token
                        {
                            Type = tokenType,
                            Value = value,
                            Line = currentLine,
                            Column = currentColumn
                        };

                    currentColumn += matchLength;
                    if (tokenType == TokenType.EOL)
                    {
                        currentLine += 1;
                        currentColumn = 0;
                    }

                    source = source.Substring(matchLength);
                }
            }

            yield return new Token
            {
                Type = TokenType.EOF,
                Line = currentLine,
                Column = currentColumn
            };
        }
    }
}