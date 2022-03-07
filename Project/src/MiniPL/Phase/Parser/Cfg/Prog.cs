using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    /*
     * <prog> ::= <stmts>
     */
    public class Prog : NonTerminal
    {
        public Prog()
        {
            PredictionSet = new ArrayList()
            {
                new ArrayList(){new Stmts()}
            };
        }
    }
}