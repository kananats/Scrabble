
namespace Scrabble
{
    class Anchor
    {
        public Slot slot;
        public Direction direction;

        public string alphabet;
        // "0": old alphabet
        // "1": new alphabet
        public string newAlphabet;

        public Anchor(Slot slot, string alphabet, string newAlphabet, Direction direction)
        {
            this.slot = slot;
            this.alphabet = alphabet;
            this.newAlphabet = newAlphabet;
            this.direction = direction;
        }

        public override string ToString()
        {
            return slot.row + ", " + slot.column + "\t" + direction + "\t" + (newAlphabet == "0" ? "(" + alphabet + ")" : alphabet);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Anchor anchor = (Anchor)obj;
            return slot == anchor.slot && direction == anchor.direction && alphabet == anchor.alphabet && newAlphabet == anchor.newAlphabet;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + slot.GetHashCode();
            hash = (hash * 7) + direction.GetHashCode();
            hash = (hash * 7) + alphabet.GetHashCode();
            hash = (hash * 7) + newAlphabet.GetHashCode();
            return hash;
        }
    }
}
