using System.Text.RegularExpressions;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    public class Terminal : Node // TODO: correct name? "Leaf"? 
    {
        private readonly TokenType? _tokenType; // null == any value
        private readonly string _value; // null == any value
        private Token _token;

        public Terminal(TokenType? tokenType, string value = null)
        {
            _tokenType = tokenType;
            _value = value;
        }

        public TokenType? TokenType => _tokenType;
        
        public string Value => _value;

        public bool Match(TokenType tokenType, string value)
        {
            return (_tokenType == null || tokenType == _tokenType) && (_value == null || value.Equals(_value));
        }
        
        public Token Token 
        {
            get => _token;
            set => _token = value;
        }

        public override bool Add(Token token)
        {
            if (!Match(token.TokenType, token.Value)) return false;
            _token = token;
                
            return true;
        }
    }
}