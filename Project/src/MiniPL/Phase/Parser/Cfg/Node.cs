using System;
using System.Collections;

namespace Project.MiniPL.Phase.Parser.Cfg
{
    public abstract class Node
    {
        // protected ArrayList PredictionSet; // WARNING: very bad idea!!!
        // // TODO: prediction set
        // protected int _predictionSet_position = -1;
        // protected Token _token;

        private readonly bool _optional;

        protected Node(bool optional = false)
        {
            _optional = optional;
        }

        public virtual bool Add(Token token)
        {
            
            // alternative: instead bool, return tokens that did not match rules - to rearrange the AST
            return false;
        }

        public bool Optional => _optional; //TODO: needed?
    }
}