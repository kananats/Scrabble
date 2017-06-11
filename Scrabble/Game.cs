using Common;
using System.Collections.Generic;

namespace Scrabble
{
    class Game
    {
        public static Game instance;

        public Assessment assessment
        {
            get;
            private set;
        }

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

        private double level;
        private Player leader;

        public Game()
        {
            instance = this;

            dictionary = new Dictionary();
            bag = new Bag();
            board = new Board();
            playerQueue = new Queue<Player>();

            level = 1;

            assessment = new Assessment();
        }

        public void SetLevel(double level)
        {
            this.level = level > 1 ? 1 : (level < 0 ? 0 : level);
        }

        public void Reset()
        {
            bag.Reset();
            board.Reset();

            playerQueue.Clear();

            Player player1 = new Player("A");
            player1.Reset(level);

            Player player2 = new Player("B");
            player2.Reset(level);

            playerQueue.Enqueue(player1);
            playerQueue.Enqueue(player2);

            leader = null;
        }

        public void SingleGame()
        {
            Reset();

            int round = 0;
            int seesaw = 0;

            int passStreak = 0;

            int branch = 0;

            while (true)
            {
                round++;

                Player player = playerQueue.Dequeue();
                playerQueue.Enqueue(player);

                double point = player.point;
                player.Step();

                branch = branch + player.moveList.Count;

                passStreak = point == player.point ? passStreak + 1 : 0;

                if (player.alphabets.Length == 0 || passStreak == playerQueue.Count)
                {
                    List<Player> playerList = new List<Player>(playerQueue);
                    playerList.ForEach(x => x.Reduce());

                    if (player.point > leader.point)
                    {
                        leader = player;
                        seesaw++;
                    }

                    Debug.Log(leader.name + "[Win]" + "\t" + leader.point, LogLevel.Result);
                    Debug.Step(LogLevel.Result);

                    assessment.seesawModel.Add(seesaw, round);
                    assessment.boardGameModel.Add(branch * 1.0 / round, round);
                    break;
                }

                if (leader == null || player.point > leader.point)
                {
                    leader = player;
                    seesaw++;
                }
            }
        }
    }
}
