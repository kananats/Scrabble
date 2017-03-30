using System;

namespace Common
{
    static class Debug
    {
        private static int LOG_LEVEL = 1;
        private static int STEP_LEVEL = 2;

        /* 
         * 0: Result
         * 1: Move, Draw, Board, Reduce
         * 2: Anchor Count, Move Count
         * 3: Anchor List, Move List
         */
        public static void Log(string message, int level = 0)
        {
            if (level > LOG_LEVEL)
                return;

            Console.WriteLine(message);
        }

        public static void Log(int level = 0)
        {
            Log("", level);
        }

        /* 
         * 0: -
         * 1: Game
         * 2: Move
         * 3: Anchor
         */
        public static void Step(int level = 0)
        {
            if (level > STEP_LEVEL)
                return;

            Console.ReadLine();
        }
    }
}
