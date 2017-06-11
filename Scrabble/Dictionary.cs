using System.Collections.Generic;
using System.IO;
using Common;

namespace Scrabble
{
    class NextNode
    {
        private Node[] nodes;

        public NextNode()
        {
            nodes = new Node[26];
        }

        public Node this[string s]
        {
            get
            {
                return nodes[s.ToIndex()];
            }

            set
            {
                nodes[s.ToIndex()] = value;
            }
        }
    }

    class Node
    {
        public bool valid;

        public NextNode next
        {
            get;
            private set;
        }

        public Node()
        {
            next = new NextNode();

            valid = false;
        }
    }

    class Dictionary
    {
        private List<string> words;

        private IEnumerable<string> lines;

        public Dictionary()
        {
            lines = File.ReadLines(@"..\..\Resources\dictionary.txt");
        }

        public void Reset(double level)
        {
            words = new List<string>();

            foreach (string line in lines)
            {
                if (Randomizer.Double(0, 1) > level)
                    continue;

                words.Add(line);
            }
        }

        public Node CreateDictionary(double level)
        {
            level = level > 1 ? 1 : (level < 0 ? 0 : level);

            Node root = new Node()
            {
                valid = true
            };

            foreach (string word in words)
            {
                if (Randomizer.Double(0, 1) > level)
                    continue;

                Node node = root;

                for (int i = 0; i < word.Length; i++)
                {
                    string alphabet = word[i].ToString().ToLower();
                    if (node.next[alphabet] == null)
                        node.next[alphabet] = new Node();

                    if (i == word.Length - 1)
                        node.next[alphabet].valid = true;

                    else
                        node = node.next[alphabet];
                }
            }

            return root;
        }
    }
}
