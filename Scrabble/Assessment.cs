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

        public BoardGameModel boardGameModelScore5
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore10
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore15
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore20
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore25
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore30
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore35
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore40
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore45
        {
            get;
            private set;
        }

        public BoardGameModel boardGameModelScore50
        {
            get;
            private set;
        }

        public Assessment()
        {
            boardGameModel = new BoardGameModel();
            boardGameModelScore5 = new BoardGameModel();
            boardGameModelScore10 = new BoardGameModel();
            boardGameModelScore15 = new BoardGameModel();
            boardGameModelScore20 = new BoardGameModel();
            boardGameModelScore25 = new BoardGameModel();
            boardGameModelScore30 = new BoardGameModel();
            boardGameModelScore35 = new BoardGameModel();
            boardGameModelScore40 = new BoardGameModel();
            boardGameModelScore45 = new BoardGameModel();
            boardGameModelScore50 = new BoardGameModel();

            seesawModel = new SeesawModel();
        }

        public override string ToString()
        {
            return string.Format("S = {0:#.###}, L = {1:#.###}, R = {2:#.###}"
                , seesawModel.averageTop
                , seesawModel.averageBottom
                , Math.Sqrt(seesawModel.averageTop) / seesawModel.averageBottom);
            /*
            return string.Format("N = {0}, L = {1}, S = {2:#.###}, B = {3:#.###}, B5 = {4:#.###}, B10 = {5:#.###}, B15 = {6:#.###}, B20 = {7:#.###}, B25 = {8:#.###}, B30 = {9:#.###}, B35 = {10:#.###}, B40 = {11:#.###}, B45 = {12:#.###}, B50 = {13:#.###}"
                , seesawModel.numberOfData
                , seesawModel.averageBottom
                , seesawModel.averageTop
                , boardGameModel.averageTop
                , boardGameModelScore5.averageTop
                , boardGameModelScore10.averageTop
                , boardGameModelScore15.averageTop
                , boardGameModelScore20.averageTop
                , boardGameModelScore25.averageTop
                , boardGameModelScore30.averageTop
                , boardGameModelScore35.averageTop
                , boardGameModelScore40.averageTop
                , boardGameModelScore45.averageTop
                , boardGameModelScore50.averageTop);
                */
        }
    }
}
