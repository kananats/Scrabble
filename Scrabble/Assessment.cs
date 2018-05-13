using System;

namespace Scrabble
{
    public abstract class GameRefinement
    {
        protected double accumulatedTop;
        protected double accumulatedBottom;

        public int numberOfData
        {
            get;
            protected set;
        }

        protected GameRefinement()
        {
            Reset();
        }

        public double averageTop
        {
            get
            {
                return accumulatedTop * 1.0 / numberOfData;
            }
        }

        public double averageBottom
        {
            get
            {
                return accumulatedBottom * 1.0 / numberOfData;
            }
        }

        public double value
        {
            get
            {
                return Math.Sqrt(averageTop) / averageBottom;
            }
        }

        public void Add(double top, double bottom)
        {
            numberOfData++;
            accumulatedTop = accumulatedTop + top;
            accumulatedBottom = accumulatedBottom + bottom;
        }

        public virtual void Reset()
        {
            numberOfData = 0;
            accumulatedTop = 0;
            accumulatedBottom = 0;
        }

        public abstract override string ToString();
    }

    public class SeesawModel : GameRefinement
    {
        private double accumulatedMinscore;
        private double accumulatedMaxscore;

        public double averageMinscore
        {
            get
            {
                return accumulatedMinscore * 1.0 / numberOfData;
            }
        }

        public double averageMaxscore
        {
            get
            {
                return accumulatedMaxscore * 1.0 / numberOfData;
            }
        }

        public void Add(double top, double bottom, double minscore, double maxscore)
        {
            numberOfData++;

            accumulatedTop = accumulatedTop + top;
            accumulatedBottom = accumulatedBottom + bottom;

            accumulatedMinscore = accumulatedMinscore + minscore;
            accumulatedMaxscore = accumulatedMaxscore + maxscore;
        }

        public override void Reset()
        {
            numberOfData = 0;

            accumulatedTop = 0;
            accumulatedBottom = 0;

            accumulatedMinscore = 0;
            accumulatedMaxscore = 0;
        }

        public override string ToString()
        {
            return string.Format("N: {0}\tS: {1} \tL: {2:#.###}\tR: {3:#.###}\tP1: {#.###}\tP2: {#.###}", numberOfData, averageTop, averageBottom, value, averageMinscore, averageMaxscore);
        }
    }

    public class BoardGameModel : GameRefinement
    {
        public override string ToString()
        {
            return string.Format("N: {0}\tB: {1} \tD: {2:#.###}\tR: {3:#.###}", numberOfData, averageTop, averageBottom, value);
        }
    }

    public class Assessment
    {
        public SeesawModel seesawModel
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModel
        {
            get;
            private set;
        }

        public Assessment()
        {
            boardGameModel = new BoardGameModel();

            seesawModel = new SeesawModel();
        }

        public override string ToString()
        {
            return string.Format("S = {0:#.###}, L = {1:#.###}, B = {2:#.###}, D = {3:#.###}, R = {4:#.###}, MIN = {5:#.###}, MAX = {6:#.###}"
                , seesawModel.averageTop
                , seesawModel.averageBottom
                , boardGameModel.averageTop
                , boardGameModel.averageBottom
                , Math.Sqrt(seesawModel.averageTop) / seesawModel.averageBottom
                , seesawModel.averageMinscore
                , seesawModel.averageMaxscore);
        }
    }
}
