using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    public abstract class NonTerminal : Node
    {
        protected ArrayList PredictionSet = null; //TODO: alter, some dynamic way to produce it

        protected ArrayList ChildrenNodes = null; // generate based on PredictionSet
        
        // // TODO: prediction set
        protected int PredictionSetPositionY = -1;
        protected int PredictionSetPositionX = -1;

        protected virtual void ProduceNext()
        {
            
        }

        public override bool Add(Token token)
        {
            if (PredictionSet == null)
            {
                ProduceNext(); // initializing prediction set and children nodes
            }

            
            var currentPredictionSet = (ArrayList)PredictionSet[PredictionSetPositionY];
            var currentPredictionSetNode = (Node) currentPredictionSet[PredictionSetPositionX];
            
            
            
            // foreach (var prediction in currentPredictionSet)
            // {
            //     var predictionN = (Node) prediction;
            //
            //     if (predictionN.Add(token))
            //     {
            //         return true;
            //     }
            // }
            
            // TODO:

            return false;
        }
        // protected void AddStatement(AbstractStatement statement) // TODO: needed?
        // {
        //     PredictionSet.Add(statement);
        // }
    }
}