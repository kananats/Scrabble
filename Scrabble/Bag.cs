using Common;
using System.Collections.Generic;

namespace Scrabble
{
    class Tile
    {
        public static readonly Tile A = new Tile("a", 1);
        public static readonly Tile B = new Tile("b", 3);
        public static readonly Tile C = new Tile("c", 3);
        public static readonly Tile D = new Tile("d", 2);
        public static readonly Tile E = new Tile("e", 1);
        public static readonly Tile F = new Tile("f", 4);
        public static readonly Tile G = new Tile("g", 2);
        public static readonly Tile H = new Tile("h", 4);
        public static readonly Tile I = new Tile("i", 1);
        public static readonly Tile J = new Tile("j", 8);
        public static readonly Tile K = new Tile("k", 5);
        public static readonly Tile L = new Tile("l", 1);
        public static readonly Tile M = new Tile("m", 3);
        public static readonly Tile N = new Tile("n", 1);
        public static readonly Tile O = new Tile("o", 1);
        public static readonly Tile P = new Tile("p", 3);
        public static readonly Tile Q = new Tile("q", 10);
        public static readonly Tile R = new Tile("r", 1);
        public static readonly Tile S = new Tile("s", 1);
        public static readonly Tile T = new Tile("t", 1);
        public static readonly Tile U = new Tile("u", 1);
        public static readonly Tile V = new Tile("v", 4);
        public static readonly Tile W = new Tile("w", 4);
        public static readonly Tile X = new Tile("x", 8);
        public static readonly Tile Y = new Tile("y", 4);
        public static readonly Tile Z = new Tile("z", 10);

        public string alphabet
        {
            get;
            private set;
        }

        public int point
        {
            get;
            private set;
        }

        private Tile(string alphabet, int point = 0)
        {
            this.alphabet = alphabet;
            this.point = point;
        }
    }

    class Bag
    {
        private List<Tile> tileList;

        public Bag()
        {
            tileList = new List<Tile>();
        }

        public void Initialize()
        {
            for (int i = 0; i < 9; i++)
                tileList.Add(Tile.A);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.B);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.C);

            for (int i = 0; i < 4; i++)
                tileList.Add(Tile.D);

            for (int i = 0; i < 12; i++)
                tileList.Add(Tile.E);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.F);

            for (int i = 0; i < 3; i++)
                tileList.Add(Tile.G);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.H);

            for (int i = 0; i < 9; i++)
                tileList.Add(Tile.I);

            for (int i = 0; i < 1; i++)
                tileList.Add(Tile.J);

            for (int i = 0; i < 1; i++)
                tileList.Add(Tile.K);

            for (int i = 0; i < 4; i++)
                tileList.Add(Tile.L);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.M);

            for (int i = 0; i < 6; i++)
                tileList.Add(Tile.N);

            for (int i = 0; i < 8; i++)
                tileList.Add(Tile.O);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.P);

            for (int i = 0; i < 1; i++)
                tileList.Add(Tile.Q);

            for (int i = 0; i < 6; i++)
                tileList.Add(Tile.R);

            for (int i = 0; i < 4; i++)
                tileList.Add(Tile.S);

            for (int i = 0; i < 6; i++)
                tileList.Add(Tile.T);

            for (int i = 0; i < 4; i++)
                tileList.Add(Tile.U);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.V);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.W);

            for (int i = 0; i < 1; i++)
                tileList.Add(Tile.X);

            for (int i = 0; i < 2; i++)
                tileList.Add(Tile.Y);

            for (int i = 0; i < 1; i++)
                tileList.Add(Tile.Z);

            tileList.Shuffle();

            Debug.Log(2, "Bag Initialized");
        }

        public Tile GetTile()
        {
            if (tileList.Count == 0)
                return null;

            return tileList.RemoveAndGet(tileList.Count - 1);
        }
    }
}
