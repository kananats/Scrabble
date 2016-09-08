using Common;
using System;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.Initialize();
            game.Step();

            Debug.Log("Press any key to exit");
            Console.ReadKey();
        }
    }
}
