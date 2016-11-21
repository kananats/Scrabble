using Common;
using System.Collections.Generic;

namespace Scrabble
{
    class Bag
    {
        public Dictionary<string, int> points;

        private List<string> alphabetList;

        public Bag()
        {
            alphabetList = new List<string>();

            points = new Dictionary<string, int>();
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

        public void Reset()
        {
            alphabetList.Clear();

            string alphabets = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";
            //alphabets = "WUOITADIEIEITEYZTNMKROJEOGEARASHAIOENNIIUGRYLRNEADFCBVOEDAVAPHEEOGUNOFCDSSARTIOWEMXRIULTLABETNQSPL";

            foreach (char alphabet in alphabets)
                alphabetList.Add(alphabet.ToString());

            alphabetList.Shuffle();
            string s = "";
            for (int i = 0; i < alphabetList.Count; i++)
            {
                s += alphabetList[i];
            }
            Debug.Log(s);
        }

        public string GetAlphabet()
        {
            if (alphabetList.Count == 0)
                return null;

            return alphabetList.RemoveAndGet(alphabetList.Count - 1);
        }
    }
}
