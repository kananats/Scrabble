using Common;
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

            int numberOfIteration = 200;

            for (double d = 0.1; d <= 1; d += 0.1)
            {
                game.dictionary.Reset(d);
                for (double p = 0.1; p <= 1; p += 0.1)
                {
                    game.assessment.seesawModel.Reset();
                    game.assessment.boardGameModel.Reset();

                    for (int i = 0; i < numberOfIteration; i++)
                    {
                        game.SetLevel(p);
                        game.SingleGame();
                    }

                    string assessment = game.assessment.ToString();
                    Debug.Log(string.Format("D = {0}, P = {1}, {2}", d, p, game.assessment.ToString()), LogLevel.Default);
                }
            }

            Console.WriteLine("Program is finished");

            while (true)
                Console.ReadKey();
        }
    }
}
