using System.Collections.Generic;

namespace Scrabble
{
    public class Points
    {
        private Dictionary<string, double> points;

        public double this[string alphabet]
        {
            get
            {
                double value;
                points.TryGetValue(alphabet, out value);

                return value;
            }
        }

        public Points()
        {
            points = new Dictionary<string, double>();

            points["A"] = 1;
            points["B"] = 3;
            points["C"] = 3;
            points["D"] = 2;
            points["E"] = 1;
            points["F"] = 4;
            points["G"] = 2;
            points["H"] = 4;
            points["I"] = 1;
            points["J"] = 8;
            points["K"] = 5;
            points["L"] = 1;
            points["M"] = 3;
            points["N"] = 1;
            points["O"] = 1;
            points["P"] = 3;
            points["Q"] = 10;
            points["R"] = 1;
            points["S"] = 1;
            points["T"] = 1;
            points["U"] = 1;
            points["V"] = 4;
            points["W"] = 4;
            points["X"] = 8;
            points["Y"] = 4;
            points["Z"] = 10;
        }
    }

    static class Constant
    {
        public static Points points
        {
            get;
            private set;
        }

        public static string blankAlphabet
        {
            get;
            private set;
        }

        public static string allAlphabets
        {
            get;
            private set;
        }

        public static string bagAlphabets
        {
            get;
            private set;
        }

        public static string oldAlphabet
        {
            get;
            private set;
        }

        public static string newAlphabet
        {
            get;
            private set;
        }

        public static int roundLimit
        {
            get;
            private set;
        }

        static Constant()
        {
            points = new Points();

            blankAlphabet = "_";
            allAlphabets = "abcdefghijklmnopqrstuvwxyz";
            bagAlphabets = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ__";
            oldAlphabet = "0";
            newAlphabet = "1";
            roundLimit = 100;
        }
    }
}
