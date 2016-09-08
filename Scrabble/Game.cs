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

        public void Initialize()
        {
            bag.Initialize();

            board.Initialize();

            playerQueue.Clear();

            Player player1 = new Player("A");
            player1.Initialize(100, 7);

            Player player2 = new Player("B");
            player2.Initialize(100, 7);

            playerQueue.Enqueue(player1);
            playerQueue.Enqueue(player2);

            Debug.Log(1, "Game Initialized");
        }

        public void Step()
        {
            Debug.Step();

            Player player = playerQueue.Dequeue();
            playerQueue.Enqueue(player);

            player.Step();
        }

        public void Finalize()
        {
            Debug.Log("Game Finalized");

            foreach (Player player in playerQueue)
                Debug.Log(1, "Player " + player.name + " gets " + player.point + " score");
        }
    }
}
