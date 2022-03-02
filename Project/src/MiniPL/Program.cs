using System;
using Project.MiniPL.Phase;

namespace Project.MiniPL
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var aText = " // \n : := -+ 984+ barts \"ahopj\" )) 56 ";
            var aScanner = new Scanner(aText);

            while (aScanner.HasNextChar())
            {
                var token = aScanner.GetNextToken();
                if (token == null)
                {
                    break;
                }
                
                Console.WriteLine(token.ToString());
            }
        }
    }
}