
using System;

namespace Scrabble
{
    class Move : IComparable<Move>
    {
        public Slot slot
        {
            get;
            private set;
        }

        public Direction direction
        {
            get;
            private set;
        }

        public string alphabets
        {
            get;
            private set;
        }

        public string newAlphabets
        {
            get;
            private set;
        }

        public string nextAlphabets
        {
            get;
            private set;
        }

        public double point
        {
            get;
            private set;
        }

        public double evaluate
        {
            get;
            private set;
        }

        public string formattedAlphabets
        {
            get;
            private set;
        }

        public Move(Slot slot, Direction direction, string alphabets, string newAlphabets, string nextAlphabets)
        {
            this.slot = slot;
            this.direction = direction;

            this.alphabets = alphabets;
            this.newAlphabets = newAlphabets;

            this.nextAlphabets = nextAlphabets;

            formattedAlphabets = "";
            for (int i = 0; i < alphabets.Length; i++)
                formattedAlphabets = formattedAlphabets + (newAlphabets[i] == '0' ? "(" + alphabets[i] + ")" : "" + alphabets[i]);
        }

        public void Evaluate()
        {
            point = GetMainPoint() + GetBingoPoint() + 50.0 / alphabets.Length;
            evaluate = point + GetLeavePoint() + GetBoardPoint();
        }

        private float GetBoardPoint()
        {
            return 0.0f;
        }

        private float GetLeavePoint()
        {
            //TODO
            return 0.0f;
        }

        private double GetBingoPoint()
        {
            return nextAlphabets == "" ? 50 : 0;
        }

        private double GetMainPoint()
        {
            return GetMainPoint(0, 0, 1, slot, alphabets, newAlphabets);
        }

        private double GetMainPoint(double primaryPoint, double secondaryPoint, double multiplier, Slot slot, string alphabets, string newAlphabets)
        {
            if (alphabets == "")
                return primaryPoint * multiplier + secondaryPoint;

            string alphabet = alphabets[0].ToString();

            if (newAlphabets[0].ToString() == "1")
                return GetMainPoint(primaryPoint + Constant.points[alphabet] * slot.bonus.GetMultiplier(Bonus.Type.Letter), secondaryPoint + GetPerpendicularPoint(alphabet, slot), multiplier * slot.bonus.GetMultiplier(Bonus.Type.Word), slot.next[direction], alphabets.Substring(1), newAlphabets.Substring(1));

            return GetMainPoint(primaryPoint + Constant.points[alphabet], secondaryPoint, multiplier, slot.next[direction], alphabets.Substring(1), newAlphabets.Substring(1));
        }

        private double GetPerpendicularPoint(string alphabet, Slot slot)
        {
            Slot previousSlot = slot.previous[direction.Perpendicular()];
            Slot nextSlot = slot.next[direction.Perpendicular()];

            if ((previousSlot == null || previousSlot.alphabet == null) && (nextSlot == null || nextSlot.alphabet == null))
                return 0;

            slot.alphabet = alphabet;
            Slot tempSlot = previousSlot = slot;

            double point = 0;

            while (true)
            {
                previousSlot = tempSlot.previous[direction.Perpendicular()];
                if (previousSlot == null || previousSlot.alphabet == null)
                    break;

                tempSlot = previousSlot;
            }

            while (true)
            {
                if (tempSlot == null || tempSlot.alphabet == null)
                {
                    slot.alphabet = null;
                    return point;
                }

                point = point + Constant.points[tempSlot.alphabet];
                tempSlot = tempSlot.next[direction.Perpendicular()];
            }
        }

        public int CompareTo(Move other)
        {
            return evaluate.CompareTo(other.evaluate);
        }

        public override string ToString()
        {
            return slot.row + ", " + slot.column + "\t" + direction + "\t" + formattedAlphabets + "\t" + point + "\t" + evaluate;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Move move = (Move)obj;
            return slot == move.slot && direction == move.direction && alphabets == move.alphabets && newAlphabets == move.newAlphabets;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + slot.GetHashCode();
            hash = (hash * 7) + direction.GetHashCode();
            hash = (hash * 7) + alphabets.GetHashCode();
            hash = (hash * 7) + newAlphabets.GetHashCode();
            hash = (hash * 7) + nextAlphabets.GetHashCode();
            return hash;
        }
    }
}
