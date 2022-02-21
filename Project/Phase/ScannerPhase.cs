using System;
using System.Collections.Generic;
using System.Linq;
using Project.Phase.Exception;

namespace Project.Phase
{
    /*
     * Role: The role of scanner is to group characters into recognizable units - tokens
     * Tasks:
     * - remove white spaces
     * - remove comments
     * - identify token category
    */
    public class Scanner
    {
        
        const string digits = "0123456789";
        const string whitespaces = " \t\n\r\0";
        const string operators = "+-/*!&<";
        const string separators = ")(";
        const string upperCaseLetters = "QWERTYUIOPLKJHGFDSAZXCVBNM";
        const string lowercaseLetters = "qwertyuioplkjhgfdsazxcvbnm";
        
        private int _position;
        private readonly string _text;
        public Scanner(string text)
        {
            _text = text;
            _position = 0;
            
            Console.WriteLine(text);
        }

        public bool HasNextChar()
        {
            return _position < _text.Length;
        }

        /*
         * kind of deterministic finite automaton
         */
        public Token GetNextToken()
        {
            CleanCommentsAndWhites();

            var actions = new List<(TokenType, Func<string>)>
            {
                (TokenType.Operator, CutOperator), 
                (TokenType.Separator, CutSeparator),
                (TokenType.String, CutString),
                (TokenType.Number, CutNumber),
                (TokenType.Identifier, CutIdentifier)
            };

            foreach (var action in actions)
            {
                var tokenStr = action.Item2.Invoke();

                if (tokenStr.Length > 0)
                {
                    if (action.Item1 == TokenType.Identifier && IsKeyword(tokenStr))
                    {
                        return new Token(TokenType.Keyword, tokenStr);
                    }

                    return new Token(action.Item1, tokenStr);
                }
            }

            return null;
        }

        /**
         * Returns next character.
         */
        private char GetNextChar(bool peek = false)
        {
            if (!HasNextChar())
            {
                throw new NoCharException();
            }

            // finding the next token
            var next = _text[_position];
            if (!peek) {_position++;}

            return next;
        }

        /**
         *  Returns operator if the next character matches one of the operators. If no match, returns empty string.
         */
        private string CutOperator()
        {
            return operators.Contains(GetNextChar(true)) ? GetNextChar().ToString() : "";
        }

        /**
         *  Returns separator if the next character matches one of the operators. If no match, returns empty string.
         */
        private string CutSeparator()
        {
            return separators.Contains(GetNextChar(true)) ? GetNextChar().ToString() : "";
        }

        /**
         *  Returns literal if the next sequence of characters is string. If no match, returns empty string.
         */
        private string CutString()
        {
            if (GetNextChar(true) != '\"')
            {
                return "";
            }

            var literal = GetNextChar().ToString();

            while (GetNextChar(true) != '\"')
            {
                literal += GetNextChar();
            }
            // TODO: unclosed string Error

            return literal + GetNextChar();
        }

        /**
         *  Returns number if the next sequence of characters are digits. If no match, returns empty string.
         */
        private string CutNumber()
        {
            var literal = "";
            while (digits.Contains(GetNextChar(true)))
            {
                literal += GetNextChar();
            }

            return literal;
        }

        /**
         *  Returns true if the parsed string matches one of the reserved keywords. Returns false otherwise.
         */
        private bool IsKeyword(string txt)
        {
            var keywords = new string[]
            {
                "var", "for", "end", "in", "do", "read", "print", "int", "string", "bool", "assert"
            };

            return Array.IndexOf(keywords, txt) > -1;
        }

        /**
         *  Returns identifier if the next sequence of characters is identifier. If no match, returns empty string;
         *  WARNING! This method does not handle keywords. Use IsKeyword to check if the returned identifier is not
         *  a keyword
         */
        private string CutIdentifier()
        {
            var allowedChars = lowercaseLetters + upperCaseLetters + '_';
            var token = "";
            
            while (allowedChars.Contains(GetNextChar(true)))
            {
                if (token.Length == 0)
                {
                    allowedChars += digits;
                }

                token += GetNextChar();
            }
            
            return token;
        }

        /**
         *  Removes all comments and whitespaces before the next token.
         */
        private void CleanCommentsAndWhites()
        {
            var nextChar = '\0';
            
            void CleanWhitespaces()
            {
                while (HasNextChar())
                {
                    nextChar = GetNextChar(true);
                    if (whitespaces.Contains(nextChar.ToString()))
                    {
                        GetNextChar();
                        continue;
                    }

                    break;
                }

                if (nextChar == '\0')
                {
                    nextChar = GetNextChar();
                }
            }

            CleanWhitespaces();

            while (nextChar == '/' && (GetNextChar(true) == '*' || GetNextChar(true) == '/'))
            { // there can be multiple comments in the code
                var secondChar = GetNextChar();
                if (secondChar == '*')
                { // multi-line comment
                    nextChar = GetNextChar();
                    while (!(nextChar == '*' && GetNextChar(true) == '/')) // TODO: end of no more chars error
                    {
                        nextChar = GetNextChar();
                        // skip
                    }
                
                    this.GetNextChar(); // skipping '/'
                }
            
                if (secondChar == '/')
                { // single-line comment
                    while (GetNextChar(true) != '\n') // TODO: what if no next?
                    {
                        nextChar = GetNextChar();
                        // skip
                    }
                }
                
                CleanWhitespaces();
            }
        }
    }
}