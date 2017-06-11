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

        public void Reset()
        {
            numberOfData = 0;
            accumulatedTop = 0;
            accumulatedBottom = 0;
        }

        public abstract override string ToString();
    }

    public class SeesawModel : GameRefinement
    {
        public override string ToString()
        {
            return string.Format("N: {0}\tS: {1} \tL: {2:#.###}\tR: {3:#.###}", numberOfData, averageTop, averageBottom, value);
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
            return string.Format("S = {0:#.###}, L = {1:#.###}, B = {2:#.###}, D = {3:#.###}"
                , seesawModel.averageTop
                , seesawModel.averageBottom
                , boardGameModel.averageTop
                , boardGameModel.averageBottom);
        }
    }
}
