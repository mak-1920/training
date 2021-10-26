using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;

namespace Game
{
    public static class Table
    {
        public static void Print(string[] moves)
        {
            var row = new string[moves.Length + 1];
            row[0] = "PC \\ User";
            for (int i = 0; i < moves.Length; i++)
                row[i + 1] = moves[i];
            var table = new ConsoleTable(row);
            for (int i = 0; i < moves.Length; i++)
            {
                row = new string[moves.Length + 1];
                row[0] = moves[i];
                for (int j = 0; j < moves.Length; j++)
                    row[j + 1] = Game.IsWin(i, j, moves.Length).ToString();
                table.AddRow(row);
            }
            Console.WriteLine(table.ToStringAlternative());
        }
    }
}
