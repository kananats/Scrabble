using Common;
using System.Collections.Generic;

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

            Player player1 = new Player("Player A");
            player1.Reset(100);

            Player player2 = new Player("Player B");
            player2.Reset(100);

            playerQueue.Enqueue(player1);
            playerQueue.Enqueue(player2);

            Debug.Log(1, "Game Reset");
        }

        public void Step()
        {
            Debug.Step();

            Player player = playerQueue.Dequeue();
            playerQueue.Enqueue(player);

            player.Step();
        }

        public void Finish()
        {
            Debug.Log("Game Finished");

            foreach (Player player in playerQueue)
                Debug.Log(1, "Player " + player.name + " gets " + player.point + " score");
        }
    }
}
