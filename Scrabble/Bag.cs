using System.Collections.Generic;

namespace Scrabble
{
    class Bag
    {
        private List<string> alphabetList;

        public string nextAlphabet
        {
            get
            {
                if (alphabetList.Count == 0)
                    return null;

                return alphabetList.RemoveAndGet(alphabetList.Count - 1);
            }
        }

        public Bag()
        {
            alphabetList = new List<string>();
        }

        public void Reset()
        {
            alphabetList.Clear();

            string alphabets = Constant.bagAlphabets;

            foreach (char alphabet in alphabets)
                alphabetList.Add(alphabet.ToString());

            alphabetList.Shuffle();

            string s = "";
            for (int i = 0; i < alphabetList.Count; i++)
                s += alphabetList[i];
        }
    }
}
