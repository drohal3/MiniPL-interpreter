using System;

namespace Project
{
    public enum TokenType
    {
        String,
        Number,
        Operator,
        Separator,
        Identifier,
        Keyword
    }
    
    
    public class Token
    {
        public Token(TokenType tokenType, string value, int position = -1, int line = -1)
        {
            TokenType = tokenType;
            Value = value;
            Position = position;
            Line = line;
            // Console.WriteLine(text);
        }

        public TokenType TokenType { get; }

        public string Value { get; }

        public int Position { get; }

        public int Line { get; }
    }
}