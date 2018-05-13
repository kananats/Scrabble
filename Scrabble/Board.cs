using Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace Scrabble
{
    class Bonus : IComparable<Bonus>
    {
        public enum Type
        {
            Letter,
            Word
        }

        public int multiplier
        {
            get;
            private set;
        }

        public Type type
        {
            get;
            private set;
        }

        public Bonus(Type type, int multiplier = 1)
        {
            this.type = type;
            this.multiplier = multiplier;
        }

        public int CompareTo(Bonus other)
        {
            return type.CompareTo(other.type) == 0 ? (multiplier.CompareTo(other.multiplier)) : type.CompareTo(other.type);
        }

        public int GetMultiplier(Type type)
        {
            if (this.type == type)
                return multiplier;

            return 1;
        }
    }

    class PreviousSlot
    {
        public Slot this[Direction direction]
        {
            get
            {
                return direction == Direction.Horizontal ? left : top;
            }
        }

        public Slot left;
        public Slot top;

        public PreviousSlot()
        {
            left = top = null;
        }
    }

    class NextSlot
    {
        public Slot this[Direction direction]
        {
            get
            {
                return direction == Direction.Horizontal ? right : bottom;
            }
        }

        public Slot right;
        public Slot bottom;

        public NextSlot()
        {
            right = bottom = null;
        }
    }

    class Slot
    {
        public string alphabet
        {
            get;
            set;
        }

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

        public Bonus bonus
        {
            get;
            private set;
        }

        public PreviousSlot previous;
        public NextSlot next;

        public Slot top
        {
            get
            {
                return previous.top;
            }

            set
            {
                previous.top = value;
            }
        }
        public Slot left
        {
            get
            {
                return previous.left;
            }

            set
            {
                previous.left = value;
            }
        }
        public Slot bottom
        {
            get
            {
                return next.bottom;
            }
            set
            {
                next.bottom = value;
            }
        }
        public Slot right
        {
            get
            {
                return next.right;
            }
            set
            {
                next.right = value;
            }
        }

        public Slot(int row, int column, Bonus bonus)
        {
            this.row = row;
            this.column = column;
            this.bonus = bonus;

            previous = new PreviousSlot();
            next = new NextSlot();
        }

        public void Reset()
        {
            alphabet = null;
        }

        public override string ToString()
        {
            return "(" + row + ", " + column + ")";
        }
    }

    class Slots
    {
        public Slot[,] slots;

        public virtual Slot this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.row || column < 0 || column >= this.column)
                    return null;

                return slots[row, column];
            }

            set
            {
                slots[row, column] = value;
            }
        }

        public Slots(int row, int column)
        {
            slots = new Slot[row, column];
        }

        public int row
        {
            get
            {
                return slots.GetLength(0);
            }
        }

        public int column
        {
            get
            {
                return slots.GetLength(1);
            }
        }
    }

    class Board
    {
        public List<Slot> slotList
        {
            get;
            private set;
        }

        public Slots slots
        {
            get;
            private set;
        }

        public int row
        {
            get
            {
                return slots.row;
            }
        }

        public int column
        {
            get
            {
                return slots.column;
            }
        }

        public Board()
        {
            IEnumerable<string> lines = File.ReadLines(@"..\..\Resources\board15.txt");

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

            slots = new Slots(row, column);

            int i = 0;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(null);
                for (int j = 0; j < tokens.Length; j++)
                {
                    Bonus.Type type = tokens[j].Length == 2 && tokens[j][1] == 'W' ? Bonus.Type.Word : Bonus.Type.Letter;
                    int multiplier = tokens[j].Length == 2 ? int.Parse(tokens[j][0].ToString()) : 1;

                    Bonus bonus = new Bonus(type, multiplier);

                    slots[i, j] = new Slot(i, j, bonus);
                }

                i++;
            }

            for (i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (i > 0) slots[i, j].top = slots[i - 1, j];
                    if (j > 0) slots[i, j].left = slots[i, j - 1];
                    if (i < row - 1) slots[i, j].bottom = slots[i + 1, j];
                    if (j < column - 1) slots[i, j].right = slots[i, j + 1];
                }
            }

            slotList = new List<Slot>();
        }

        public void Reset()
        {
            for (int i = 0; i < slots.row; i++)
                for (int j = 0; j < slots.column; j++)
                    slots[i, j].Reset();

            slotList.Clear();
        }

        public void Place(Slot slot, string alphabet)
        {
            slot.alphabet = alphabet;

            slotList.Add(slot);
        }

        public override string ToString()
        {
            string s = "[Board]\n  ";

            for (int j = 0; j < slots.column; j++)
                s = s + j + (j < 10 ? " " : "");

            s = s + "\n";

            for (int i = 0; i < slots.row; i++)
            {
                s = s + i + (i < 10 ? " " : "");
                for (int j = 0; j < slots.column; j++)
                    s = s + (slots[i, j].alphabet == null ? "-" : slots[i, j].alphabet) + " ";

                s = s + "\n";
            }

            return s;
        }
    }
}
