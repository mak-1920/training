using System;
using System.Linq;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 3 || args.Length % 2 != 1)
            {
                Console.WriteLine("Count of args must odd and over or equal 3");
                return;
            }
            var isCorrect = true;
            foreach(var str in args.GroupBy(e => e)
                .Where(e => e.Count() > 1)
                .Select(e => e.Key))
            {
                isCorrect = false;
                Console.WriteLine($"The string \"{str}\" occurs more than once");
            }
            if (!isCorrect) return;
            var game = new Game(args);
        }
    }
}
