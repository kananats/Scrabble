using Common;
using System;

namespace Scrabble
{
    static class Assessment
    {
        private static int numberOfData = 0;
        private static int numberOfSeesaw = 0;
        private static int numberOfRound = 0;

        public static void Insert(int seesaw, int round)
        {
            numberOfData++;
            numberOfSeesaw = numberOfSeesaw + seesaw;
            numberOfRound = numberOfRound + round;

            double averageSeesaw = numberOfSeesaw * 1d / numberOfData;
            double averageRound = numberOfRound * 1d / numberOfData;
            double averageRefinement = Math.Sqrt(averageSeesaw) / averageRound;
            Debug.Log(string.Format("{0}: {1}", numberOfData, averageRefinement), LogLevel.Assessment);
        }
    }
}
