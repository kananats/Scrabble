using System;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Reset();

            Board board = game.board;
            Slots slots = board.slots;

            game.Step();

            Console.ReadKey();
        }
    }
}
