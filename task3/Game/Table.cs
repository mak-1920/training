using System;
using System.Collections.Generic;
using System.Text;
using ASCII_Raw;

namespace Game
{
    public static class Table
    {
        public static void Print(string[] moves)
        {
            var rows = new List<string[]>();
            rows.Add(new[] { "largest", "", "smallest" });
            for (int i = 0; i < moves.Length; i++)
            {
                rows.Add(new[] { moves[i], " == ", moves[i] });
                for (int j = 1; j <= moves.Length / 2; j++)
                    rows.Add(new[] { moves[(i + j) % moves.Length], " -> ", moves[i] });
            }
            Console.WriteLine(new ASCIITable(rows).GetAsString());
        }
    }
}
