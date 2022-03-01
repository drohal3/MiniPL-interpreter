namespace Project.MiniPL
{
    /// <summary>
    ///   Token types
    /// </summary>
    public enum TokenType
    {
        String,
        Number,
        Operator,
        Separator,
        Special,
        Assign,
        Identifier,
        Keyword
    }
    
    
    public class Token
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="value"></param>
        /// <param name="position"></param>
        /// <param name="line"></param>
        public Token(TokenType tokenType, string value, int position = -1, int line = -1)
        {
            TokenType = tokenType;
            Value = value;
            Position = position;
            Line = line;
            // Console.WriteLine(text);
        }

        /// <summary>
        /// </summary>
        /// <returns>Token type</returns>
        public TokenType TokenType { get; }

        /// <summary>
        /// </summary>
        /// <returns>Sequence of characters belonging to the token</returns>
        public string Value { get; }
        
        /// <summary>
        /// </summary>
        /// <returns>
        ///   Position of the token in the source code.
        ///   (Number of characters between the beginning of the line and the first character belonging to the token)
        /// </returns>
        public int Position { get; }

        /// <summary>
        /// </summary>
        /// <returns>The line number of the token position in the code</returns>
        public int Line { get; }
    }
}