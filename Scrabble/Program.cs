using System;
using Common;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Board board = game.board;
            Slots slots = board.slots;

            int numberOfIteration = 30;

            for (int level = 10; level <= 100; level += 10)
            {
                game.assessment.seesawModel.Reset();

                for (int i = 0; i < numberOfIteration; i++)
                    game.SingleGame(level);

                string assessment = game.assessment.ToString();
                Debug.Log(string.Format("W = {0} {1}\n", level, game.assessment.ToString()), LogLevel.Default);
            }

            Console.WriteLine("Program is finished");

            while (true)
                Console.ReadKey();
        }
    }
}
