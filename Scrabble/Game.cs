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

        private Player leader;

        public Game()
        {
            instance = this;

            dictionary = new Dictionary();
            bag = new Bag();
            board = new Board();
            playerQueue = new Queue<Player>();

            Reset();
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

            leader = null;
        }

        public void Cycle()
        {
            Reset();

            while (true)
            {
                Player player = playerQueue.Dequeue();
                playerQueue.Enqueue(player);

                player.Step();

                if (player.alphabets.Length == 0)
                {
                    List<Player> playerList = new List<Player>(playerQueue);
                    playerList.ForEach(x => x.Reduce());

                    if (player.point > leader.point)
                        leader = player;

                    Debug.Log(leader.name + "[Win]" + "\t" + leader.point, LogLevel.Result);
                    Debug.Step(LogLevel.Result);

                    Reset();
                    break;
                }

                if (player.point > leader.point)
                    leader = player;
            }

            Finish();
        }

        public void Finish()
        {
            
        }
    }
}
