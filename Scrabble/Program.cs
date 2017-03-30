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

            for (int i = 0; i < 3; i++)
                game.Cycle();

            Console.ReadKey();
        }
    }
}
