using Common;
using System.Collections.Generic;

namespace Scrabble
{
    class Player
    {
        public string name
        {
            get;
            private set;
        }

        public int point
        {
            get;
            private set;
        }

        private int level;

        private List<Tile> tileList;
        private int size;

        public Player(string name)
        {
            this.name = name;

            tileList = new List<Tile>();
        }

        public void Initialize(int level, int size)
        {
            tileList.Clear();

            this.level = level < 1 ? 1 : (level > 100 ? 100 : level);

            point = 0;

            this.size = size;

            Draw();
        }

        public void Step()
        {
            Play();
            Draw();
            CheckEndGame();
        }

        private void Play()
        {
            if (Game.instance.board.activeSlots.Count == 0)
            {
                
                Permute(tileList);
                //TODO compute the best and need to start from center
            }
        }

        private static void Log(List<Tile> tileList)
        {
            Log(tileList, tileList.Count);
        }

        private static void Log(List<Tile> tileList, int index)
        {
            string s = "";
            for (int i = 0; i < index; i++)
                s = s + (i == 0 ? "[ " : "") + tileList[i].alphabet + (i == index - 1 ? " ]" : ", ");

            Debug.Log(s);
        }

        public static void Permute(List<Tile> tileList)
        {
            Permute(tileList, 0, Game.instance.dictionary.root);
        }

        private static void Permute(List<Tile> tileList, int index, Node node)
        {
            if (index >= 2 && node.valid)
                Log(tileList, index);

            if (index == tileList.Count)
                return;

            for (int i = index; i < tileList.Count; i++)
            {
                Swap(tileList, index, i);

                Node next = node.next[tileList[index].alphabet];
                if (next != null)
                    Permute(tileList, index + 1, next);

                Swap(tileList, index, i);
            }
        }

        private static void Swap(List<Tile> tileList, int i, int j)
        {
            Tile temp = tileList[i];
            tileList[i] = tileList[j];
            tileList[j] = temp;
        }

        private void Draw()
        {
            string s = name + " draws ";

            while (tileList.Count < size)
            {
                Tile tile = Game.instance.bag.GetTile();

                if (tile == null)
                    break;

                s = s + tile.alphabet + (tileList.Count == size - 1 ? "" : ", ");

                tileList.Add(tile);
            }

            Debug.Log(s);
        }

        private void CheckEndGame()
        {
            if (tileList.Count == 0)
                Game.instance.Finalize();

            else
                Game.instance.Step();
        }
    }
}
