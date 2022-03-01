using System;
using System.Collections.Generic;
using System.Linq;
using Project.Phase.Exception;

namespace Project.Phase
{
    /*
     * Role: The role of the scanner is to group characters into recognizable units - tokens
     * Tasks:
     * - remove white spaces
     * - remove comments
     *   - single line comments: // <something> \n
     *   - multi line comments: /* <something> *\/
     * - identify token category
    */
    public class Scanner
    {
        /// <summary>
        ///   digit characters
        /// </summary>
        const string digits = "0123456789";
        
        /// <summary>
        ///   whitespaces
        /// </summary>
        const string whitespaces = " \t\n\r\0";
        
        /// <summary>
        ///   operator characters
        /// </summary>
        const string operators = "+-/*!&<";
        
        /// <summary>
        ///   separator characters
        /// </summary>
        const string separators = ")(";
        
        /// <summary>
        ///   special characters
        /// </summary>
        const string special = ";:";
        
        /// <summary>
        ///   upper case letter characters
        /// </summary>
        const string upperCaseLetters = "QWERTYUIOPLKJHGFDSAZXCVBNM";
        
        /// <summary>
        ///   lower case letter characters
        /// </summary>
        const string lowercaseLetters = "qwertyuioplkjhgfdsazxcvbnm";
        
        /// <summary>
        ///   position of the next unread character
        /// </summary>
        private int _position;
        
        /// <summary>
        ///   scanned text
        /// </summary>
        private readonly string _text;
        
        /// <summary>
        ///   constructor
        /// </summary>
        /// <param name="text">text to scan</param>
        public Scanner(string text)
        {
            _text = text;
            _position = 0;
            
            Console.WriteLine(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasNextChar()
        {
            // TODO: handle errors
            return _position < _text.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool HasNextToken()
        {
            var origPosition = _position;
            // TODO: handle errors
            var nextToken = GetNextToken();
            
            _position = origPosition;
            return nextToken != null;
        }
        
        /// <summary>
        ///   Returns the next token.
        /// </summary>
        /// <returns>Next token</returns>
        public Token GetNextToken()
        {             // TODO: handle errors
            /*
             * kind of modified deterministic finite automaton
             * split into multiple methods - pool to avoid "spaghetti code"
             */
            
            CleanCommentsAndWhites();

            var actions = new List<(TokenType, Func<string>)>
            {
                (TokenType.Operator, CutOperator), 
                (TokenType.Separator, CutSeparator),
                (TokenType.Assign, cutAssign), // must be run before Special (: vs :=)
                (TokenType.Special, CutSpecial),
                (TokenType.String, CutString),
                (TokenType.Number, CutNumber),
                (TokenType.Identifier, CutIdentifier)
            };

            foreach (var action in actions)
            {
                var tokenStr = action.Item2.Invoke();

                if (tokenStr.Length > 0)
                { // handling keywords
                    if (action.Item1 == TokenType.Identifier && IsKeyword(tokenStr))
                    {
                        return new Token(TokenType.Keyword, tokenStr);
                    }

                    return new Token(action.Item1, tokenStr);
                }
            }

            return null;
        }

        /// <summary>
        ///   Returns the next character in the code and adjusts the position. 
        /// </summary>
        /// <param name="peek"></param>
        /// <returns>Next character</returns>
        /// <exception cref="NoCharException"></exception>
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

        /*
         *  Returns operator if the next character matches one of the operators. If no match, returns empty string.
         */
        private string CutOperator()
        {
            return operators.Contains(GetNextChar(true)) ? GetNextChar().ToString() : "";
        }

        /*
         *  Returns separator if the next character matches one of the operators. If no match, returns empty string.
         */
        private string CutSeparator()
        {
            return separators.Contains(GetNextChar(true)) ? GetNextChar().ToString() : "";
        }

        /*
         *  Returns assign chars if the next two match match :=. If no match, returns empty string.
         */
        private string cutAssign()
        {
            var startPos = _position;
            var token = "";
            if (GetNextChar(true) == ':')
            {
                token += GetNextChar();

                if (GetNextChar(true) == '=')
                {
                    token += GetNextChar();
                }
                else
                {
                    _position = startPos;
                    
                    return "";
                }
            }

            return token;
        }

        /*
         *  Returns special char if the next character matches one of the operators. If no match, returns empty string.
         */
        private string CutSpecial()
        {
            return special.Contains(GetNextChar(true)) ? GetNextChar().ToString() : "";
        }

        /*
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

        /*
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

        /*
         *  Returns identifier if the next sequence of characters is identifier. If no match, returns empty string;
         *  WARNING! This method does not handle keywords. Use IsKeyword method to check if the returned identifier is not
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
        
        /*
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

        /*
         *  Removes all comments and whitespaces before the next possible token.
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
                        nextChar = GetNextChar(); // skip the character
                    }
                }
                
                CleanWhitespaces();
            }
        }
    }
}