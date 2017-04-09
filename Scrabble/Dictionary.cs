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
        private IEnumerable<string> lines;

        public Dictionary()
        {
            lines = File.ReadLines(@"..\..\Resources\dictionary.txt");
        }

        public Node CreateDictionary(int level)
        {
            level = level < 1 ? 1 : (level > 100 ? 100 : level);

            Node root = new Node();
            root.valid = true;

            foreach (string line in lines)
            {
                if (Randomizer.Int(1, 100) > level)
                    continue;

                Node node = root;

                for (int i = 0; i < line.Length; i++)
                {
                    string alphabet = line[i].ToString().ToLower();
                    if (node.next[alphabet] == null)
                        node.next[alphabet] = new Node();

                    if (i == line.Length - 1)
                        node.next[alphabet].valid = true;

                    else
                        node = node.next[alphabet];
                }
            }

            return root;
        }
    }
}
