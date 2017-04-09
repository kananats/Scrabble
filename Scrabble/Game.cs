using Common;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    class Game
    {
        public static Game instance;

        private Assessment assessment;

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

            assessment = new Assessment();
        }

        public void Reset()
        {
            bag.Reset();
            board.Reset();

            playerQueue.Clear();

            Player player1 = new Player("A");
            player1.Reset(10);

            Player player2 = new Player("B");
            player2.Reset(10);

            playerQueue.Enqueue(player1);
            playerQueue.Enqueue(player2);

            leader = null;
        }

        public void SingleGame()
        {
            Reset();

            int round = 0;

            int seesaw = 0;

            int moveCount = 0;
            int moveCountScore5 = 0;
            int moveCountScore10 = 0;
            int moveCountScore15 = 0;
            int moveCountScore20 = 0;
            int moveCountScore25 = 0;
            int moveCountScore30 = 0;
            int moveCountScore35 = 0;
            int moveCountScore40 = 0;
            int moveCountScore45 = 0;
            int moveCountScore50 = 0;

            int passStreak = 0;

            while (true)
            {
                round++;

                Player player = playerQueue.Dequeue();
                playerQueue.Enqueue(player);

                int point = player.point;
                player.Step();

                moveCount = moveCount + player.moveList.Count;
                moveCountScore5 = moveCountScore5 + player.moveList.Count(x => x.point >= 5);
                moveCountScore10 = moveCountScore10 + player.moveList.Count(x => x.point >= 10);
                moveCountScore15 = moveCountScore15 + player.moveList.Count(x => x.point >= 15);
                moveCountScore20 = moveCountScore20 + player.moveList.Count(x => x.point >= 20);
                moveCountScore25 = moveCountScore25 + player.moveList.Count(x => x.point >= 25);
                moveCountScore30 = moveCountScore30 + player.moveList.Count(x => x.point >= 30);
                moveCountScore35 = moveCountScore35 + player.moveList.Count(x => x.point >= 35);
                moveCountScore40 = moveCountScore40 + player.moveList.Count(x => x.point >= 40);
                moveCountScore45 = moveCountScore45 + player.moveList.Count(x => x.point >= 45);
                moveCountScore50 = moveCountScore50 + player.moveList.Count(x => x.point >= 50);

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
                    assessment.boardGameModel.Add(moveCount * 1.0 / round, round);
                    assessment.boardGameModelScore5.Add(moveCountScore5 * 1.0 / round, round);
                    assessment.boardGameModelScore10.Add(moveCountScore10 * 1.0 / round, round);
                    assessment.boardGameModelScore15.Add(moveCountScore15 * 1.0 / round, round);
                    assessment.boardGameModelScore20.Add(moveCountScore20 * 1.0 / round, round);
                    assessment.boardGameModelScore25.Add(moveCountScore25 * 1.0 / round, round);
                    assessment.boardGameModelScore30.Add(moveCountScore30 * 1.0 / round, round);
                    assessment.boardGameModelScore35.Add(moveCountScore35 * 1.0 / round, round);
                    assessment.boardGameModelScore40.Add(moveCountScore40 * 1.0 / round, round);
                    assessment.boardGameModelScore45.Add(moveCountScore45 * 1.0 / round, round);
                    assessment.boardGameModelScore50.Add(moveCountScore50 * 1.0 / round, round);

                    Debug.Log(assessment.ToString() + "\n", LogLevel.Default);
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
