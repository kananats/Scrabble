using System;
using System.Collections.Generic;
using System.IO;

namespace Scrabble
{
    struct Position
    {
        public int row
        {
            get;
            private set;
        }

        public int column
        {
            get;
            private set;
        }

        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public static Position operator +(Position p1, Position p2)
        {
            return new Position(p1.row + p2.row, p1.column + p2.column);
        }
    }

    class Slot
    {
        public Tile tile
        {
            get;
            private set;
        }

        public Position position
        {
            get;
            private set;
        }

        public Bonus bonus
        {
            get;
            private set;
        }

        public Slot(Position position, Bonus bonus)
        {
            this.position = position;
            this.bonus = bonus;
        }

        public void Initialize()
        {
            tile = null;
        }

        public void SetTile(Tile tile)
        {
            this.tile = tile;
        }
    }

    class Board
    {
        public List<Slot> activeSlots
        {
            get;
            private set;
        }

        public Slot[,] slots
        {
            get;
            private set;
        }

        public Board()
        {
            IEnumerable<string> lines = File.ReadLines(@"..\..\Resources\board.txt");

            int row = 0;
            int column = 0;

            foreach (string line in lines)
            {
                string[] tokens = line.Split(null);

                if (column == 0)
                    column = tokens.Length;

                else if (column != tokens.Length)
                    throw new Exception("Invalid input string");

                row++;
            }

            slots = new Slot[row, column];

            int i = 0;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(null);
                for (int j = 0; j < tokens.Length; j++)
                {
                    Position position = new Position(i, j);

                    Bonus bonus;
                    int token = int.Parse(tokens[j]);
                    switch (token)
                    {
                        case 0:
                            bonus = Bonus.None;
                            break;

                        case 1:
                            bonus = Bonus.DoubleLetter;
                            break;

                        case 2:
                            bonus = Bonus.TripleLetter;
                            break;

                        case 3:
                            bonus = Bonus.DoubleWord;
                            break;

                        case 4:
                            bonus = Bonus.TripleWord;
                            break;

                        default:
                            throw new Exception("Invalid input string");
                    }

                    slots[i, j] = new Slot(position, bonus);
                }

                i++;
            }

            activeSlots = new List<Slot>();
        }

        public void Initialize()
        {
            for (int i = 0; i < slots.GetLength(0); i++)
                for (int j = 0; j < slots.GetLength(1); j++)
                    slots[i, j].Initialize();

            activeSlots.Clear();
        }

        public void Place(Tile tile, Position position)
        {
            Slot slot = slots[position.row, position.column];

            slot.SetTile(tile);
            activeSlots.Add(slot);
        }
    }
}
