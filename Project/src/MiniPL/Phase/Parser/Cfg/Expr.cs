using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    
    /*
     * <expr> ::= <opnd> <op> <opnd>
                | [ <unary_opnd> ] <opnd>
     */
    public class Expr : NonTerminal
    {
        public Expr()
        {
            PredictionSet = new ArrayList()
            {
                // TODO:
            };
        }
        // TODO:
    }
}