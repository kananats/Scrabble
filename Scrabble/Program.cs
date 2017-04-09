using System;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Board board = game.board;
            Slots slots = board.slots;

            int numberOfIteration = 100;
            for (int i = 0; i < numberOfIteration; i++)
                game.SingleGame();

            Console.WriteLine("Program is finished");

            while (true)
                Console.ReadKey();
        }
    }
}
