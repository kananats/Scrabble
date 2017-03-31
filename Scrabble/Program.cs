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

            int numberOfIteration = 1000;
            for (int i = 0; i < numberOfIteration; i++)
                game.Loop();

            Console.WriteLine("Program is finished");

            while (true)
                Console.ReadKey();
        }
    }
}
