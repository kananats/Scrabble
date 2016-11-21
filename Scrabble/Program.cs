using Common;
using System;

namespace Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Reset();

            //hack space



            //end of hack space

            game.Step();
            Debug.Log("Press any key to exit");
            Console.ReadKey();
        }
    }
}
