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

        public static void Log(string message, LogLevel level = LogLevel.Assessment)
        {
            if (!LOG_LEVEL.HasFlag(level))
                return;

            Console.WriteLine(message);
        }

        public static void Step(LogLevel level)
        {
            if (!LOG_LEVEL.HasFlag(level))
                return;

            Console.WriteLine("STEP");
            Console.ReadLine();
        }
    }
}
