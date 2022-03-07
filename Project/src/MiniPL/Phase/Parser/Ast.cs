using Project.MiniPL.Phase.Parser.Cfg;

namespace Project.MiniPL.Phase.Parser
{
    public class Ast
    {
        private readonly Prog _astRoot;
        public Ast()
        {
            _astRoot = new Prog();
        }

        public void AddToken(Token token)
        {
            _astRoot.Add(token);
        }
    }
}