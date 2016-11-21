using System;

namespace Common
{
    static class Debug
    {
        private static int LOG_LEVEL = 10;
        private static bool STEP = true;

        public static void Log(string message = "")
        {
            Log(0, message);
        }

        public static void Log(int level, string message = "")
        {
            if (level > LOG_LEVEL)
                return;

            Console.WriteLine(message);
        }

        public static void Step(string message = null)
        {
            Step(null, message);
        }

        public static void Step(Action action, string message = null)
        {
            if (STEP)
            {
                if (message != null)
                    Log(message);

                Console.ReadLine();
            }

            if (action == null)
                return;

            action.Invoke();
        }
    }
}
