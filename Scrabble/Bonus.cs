using System;

namespace Scrabble
{
    class Bonus : IComparable<Bonus>
    {
        public static readonly Bonus DoubleLetter = new Bonus(Type.Letter, 2);
        public static readonly Bonus TripleLetter = new Bonus(Type.Letter, 3);
        public static readonly Bonus DoubleWord = new Bonus(Type.Word, 2);
        public static readonly Bonus TripleWord = new Bonus(Type.Word, 3);
        public static readonly Bonus None = null;

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

        private Bonus(Type type, int multiplier = 1)
        {
            this.type = type;
            this.multiplier = multiplier;
        }

        public int CompareTo(Bonus other)
        {
            return type.CompareTo(other.type) == 0 ? (multiplier.CompareTo(other.multiplier)) : type.CompareTo(other.type);
        }
    }
}
