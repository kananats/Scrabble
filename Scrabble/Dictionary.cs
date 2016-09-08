using System.IO;

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
                return nodes[s.ToIntIndex()];
            }

            set
            {
                nodes[s.ToIntIndex()] = value;
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
        public Node root
        {
            get;
            private set;
        }

        public Dictionary()
        {
            root = new Node();

            foreach (string line in File.ReadLines(@"..\..\Resources\dictionary.txt"))
            {
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
        }

        public bool Search(string word)
        {
            Node node = root;

            for (int i = 0; i < word.Length; i++)
            {
                string alphabet = word[i].ToString();

                if (node.next[alphabet] == null)
                    return false;

                else if (i == word.Length - 1)
                    return node.next[alphabet].valid;

                else
                    node = node.next[alphabet];
            }
            return false;
        }
    }
}
