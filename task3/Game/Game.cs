using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Game
{
    public class Game
    {
        public enum State { Win, Draw, Lose }

        private readonly string[] moves;
        private readonly int comMove;
        private readonly Security security;

        public Game(string[] moves)
        {
            this.moves = moves;
            security = new Security(moves.Length);
            Console.WriteLine($"HMAC (base64): {security.HMAC}");
            comMove = security.Move;
            string item;
            do
            {
                item = menu();
                if (item == "?")
                    Table.Print(moves);
            } 
            while (!isCorrectMenuItem(item));
            if (item == "0") return;
            else
            {
                var i = int.Parse(item) - 1;
                Console.WriteLine($"Your move: {moves[i]}");
                Console.WriteLine($"Computer move: {moves[comMove]}");
                switch (IsWin(comMove, i, moves.Length))
                {
                    case State.Draw: Console.WriteLine("Draw!"); break;
                    case State.Lose: Console.WriteLine("You lose!"); break;
                    case State.Win: Console.WriteLine("You win!"); break;
                }
                Console.WriteLine($"HMAC key (base64): {security.HMACKey}");
                //Console.WriteLine(Convert.ToBase64String(new HMACSHA384(Convert.FromBase64String(security.HMACKey)).ComputeHash(BitConverter.GetBytes(comMove))));
                security.Dispose();
            }
        }

        static public State IsWin(int comMove, int userMove, int movesCount)
        {
            if (comMove == userMove) return State.Draw;
            var smallest = Math.Min(userMove, comMove);
            var largest = Math.Max(userMove, comMove);
            if (smallest + movesCount / 2 < largest)
                return smallest == comMove ? State.Lose : State.Win;
            return smallest == comMove ? State.Win : State.Lose;
        }

        private string menu()
        {
            for (int i = 0; i < moves.Length; i++)
                Console.WriteLine($"{i + 1} - {moves[i]}");
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
            Console.Write("Enter your move: ");
            return Console.ReadLine();
        }

        private bool isCorrectMenuItem(string menuItem)
        {
            int num;
            if (!int.TryParse(menuItem, out num))
                return false;
            return num >= 0 && num <= moves.Length;
        }
    }
}
