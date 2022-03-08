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
            
        }

        protected override void ProduceNext()
        {
            // TODO: if hit the max position?
            
            if (PredictionSet == null)
            {
                PredictionSet = new ArrayList()
                {
                    new ArrayList(){new Stmts()}
                };
            }

            PredictionSetPositionY++;
        }
    }
}