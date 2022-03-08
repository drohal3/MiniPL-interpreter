using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    /*
     * <stmts> ::= <stmt> “;” ( <stmt> “;” )*
     */
    
    public class Stmts : NonTerminal
    {
        protected override void ProduceNext()
        {
            if (PredictionSet == null)
            {
                PredictionSet = new ArrayList(){ // prediction set
                    new ArrayList()
                    { 
                        new Terminal(TokenType.Keyword, "var"),
                        new Terminal(TokenType.Identifier), 
                        new Terminal(TokenType.Special, ";") 
                        /*TODO <type>*/
                        // TODO: [ “:=” <expr> ]
                    }, // “var” <var_ident> “:” TODO: <type> [ “:=” <expr> ] // ideas: new statement type Optional or new prediction option
                    new ArrayList()
                    {
                        new Terminal(TokenType.Identifier),
                        new Terminal(TokenType.Assign, ":="),
                        new Expr()
                    }, // <var_ident> “:=” <expr>
                    new ArrayList() {}, // TODO: “for” <var_ident> “in” <expr> “..” <expr> “do” <stmts> “end” “for”
                    new ArrayList() {}, // TODO: “read” <var_ident>
                    new ArrayList() {}, // TODO: “print” <expr>
                    new ArrayList() {}  // TODO: “assert” “(” <expr> “)”
                };
            }

            PredictionSetPositionY++;
        }
        // TODO:
    }
}