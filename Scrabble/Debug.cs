using System;

namespace Common
{
    [Flags]
    public enum LogLevel
    {
        Assessment = 1 << 0,
        Result = 1 << 1,
        Draw = 1 << 2,
        Move = 1 << 3,
        Reduce = 1 << 4,
        Board = 1 << 5,
        MoveCount = 1 << 6,
        MoveDetail = 1 << 7,
        AnchorCount = 1 << 8,
        AnchorDetail = 1 << 9,

        Default = Assessment,
        ShowResult = Default | Result,
        ShowMove = ShowResult | Draw | Move | Reduce,
        ShowMoveWithBoard = ShowMove | Board,
        ShowMoveDetail = ShowMoveWithBoard | MoveCount | MoveDetail,
        ShowAnchorDetail = ShowMoveDetail | AnchorCount | AnchorDetail,
        All = ShowAnchorDetail
    }

    static class Debug
    {
        private static LogLevel LOG_LEVEL = LogLevel.Default;

        /* 
         * 0: Result
         * 1: Move, Draw, Board, Reduce
         * 2: Anchor Count, Move Count
         * 3: Anchor List, Move List
         */
        public static void Log(string message, LogLevel level)
        {
            if (!LOG_LEVEL.HasFlag(level))
                return;

            Console.WriteLine(message);
        }

        /* 
         * 0: -
         * 1: Game
         * 2: Move
         * 3: Anchor
         */
        public static void Step(LogLevel level)
        {
            if (!LOG_LEVEL.HasFlag(level))
                return;

            Console.WriteLine("STEP");
            Console.ReadLine();
        }
    }
}
