using Common;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    class Game
    {
        public static Game instance;

        public Dictionary dictionary
        {
            get;
            private set;
        }

        public Bag bag
        {
            get;
            private set;
        }

        public Board board
        {
            get;
            private set;
        }

        private Queue<Player> playerQueue;

        public Game()
        {
            instance = this;

            dictionary = new Dictionary();
            bag = new Bag();
            board = new Board();
            playerQueue = new Queue<Player>();
        }

        public void Reset()
        {
            bag.Reset();
            board.Reset();

            playerQueue.Clear();

            Player player1 = new Player("A");
            player1.Reset(100);

            Player player2 = new Player("B");
            player2.Reset(100);

            playerQueue.Enqueue(player1);
            playerQueue.Enqueue(player2);
        }

        public void Step()
        {
            Player player = playerQueue.Dequeue();
            playerQueue.Enqueue(player);

            player.Step();
        }

        public void Finish()
        {
            List<Player> playerList = new List<Player>(playerQueue);
            playerList.ForEach(x => x.Reduce());

            int point = playerList.Max(x => x.point);

            string s = "";
            playerList.FindAll(x => x.point == point).ForEach(x =>
            {
                s = s + x.name + "\t";
            });

            Debug.Log(s + "[Win]" + "\t" + point, 0);

            Debug.Step(1);
            Debug.Log(0);
            Reset();
            Step();
        }
    }
}
