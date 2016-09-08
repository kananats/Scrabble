using System;

namespace Common
{
    static class Randomizer
    {
        private static Random random = new Random();

        public static int Int(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public static double Double(double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
