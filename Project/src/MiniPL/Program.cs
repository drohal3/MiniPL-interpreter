using System;
using Project.MiniPL.Phase;
using Project.MiniPL.Phase.Parser;

namespace Project.MiniPL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var aText = "var X : int;";
            var aScanner = new Scanner(aText);

            var aParser = new Parser(aScanner);
            aParser.Parse();
        }
    }
}