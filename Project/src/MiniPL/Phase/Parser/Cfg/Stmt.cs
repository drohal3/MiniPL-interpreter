using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    /*
     * <stmt> ::= “var” <var_ident> “:” <type> [ “:=” <expr> ]
                 | <var_ident> “:=” <expr>
                 | “for” <var_ident> “in” <expr> “..” <expr> “do” <stmts> “end” “for”
                 | “read” <var_ident>
                 | “print” <expr>
                 | “assert” “(” <expr> “)”
     */
     
    public class Stmt : NonTerminal
    {
        public Stmt()
        {
            
        }


        protected override void ProduceNext()
        {
            if (PredictionSet == null)
            {
                PredictionSet = new ArrayList(){ // prediction set
                    new ArrayList()
                    { 
                        new Terminal(TokenType.Keyword, "var"),
                        new Terminal(TokenType.Identifier), 
                        new Terminal(TokenType.Special, ":") 
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

            PredictionSetPositionY++; //TODO: what if hit the max?
            
        }
    }
}