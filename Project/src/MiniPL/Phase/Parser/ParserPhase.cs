using System;

namespace Project.MiniPL.Phase.Parser
{
    public class Parser
    {
        private readonly Scanner _scanner;

        public Parser(Scanner scanner)
        {
            _scanner = scanner;
        }

        /**/
        public void Parse()
        {
            var ast = new Ast();
            
            while (_scanner.HasNextChar())
            {
                var token = _scanner.GetNextToken();
                if (token == null)
                {
                    break;
                }
                
                Console.WriteLine(token.ToString());
                
                ast.AddToken(token);
            }
        }
        
        // TODO: GetParseTree
    }
}