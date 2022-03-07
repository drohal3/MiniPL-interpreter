using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    public abstract class NonTerminal : Node
    {
        protected ArrayList PredictionSet = null;
        // // TODO: prediction set
        protected int PredictionSetPosition = -1;
        
        public virtual void ProduceNext()
        {
            
            // TODO: what if no next? 
            // TODO:
        }
        
        // protected void AddStatement(AbstractStatement statement) // TODO: needed?
        // {
        //     PredictionSet.Add(statement);
        // }
    }
}