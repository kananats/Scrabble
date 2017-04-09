using Common;
using System;
using System.Collections.Generic;

namespace Scrabble
{
    enum Direction
    {
        Horizontal,
        Vertical
    }

    class Player
    {
        public static readonly int SIZE = 7;

        public string name
        {
            get;
            private set;
        }

        public int point
        {
            get;
            private set;
        }

        private int level;

        public string alphabets
        {
            get;
            private set;
        }

        public List<Anchor> anchorList
        {
            get;
            private set;
        }

        public List<Move> moveList
        {
            get;
            private set;
        }

        private Game game;
        private Bag bag;
        private Board board;
        private Slots slots;
        private List<Slot> slotList;
        private Node root;

        public Player(string name)
        {
            this.name = name;

            anchorList = new List<Anchor>();
            moveList = new List<Move>();

            game = Game.instance;

            bag = game.bag;
            board = game.board;

            slots = board.slots;
            slotList = board.slotList;
        }

        public void Reset(int level)
        {
            alphabets = "";

            this.level = level < 1 ? 1 : (level > 100 ? 100 : level);
            root = game.dictionary.CreateDictionary(level);

            point = 0;

            Draw();
        }

        public void Step()
        {
            Play();
            Draw();
        }

        public void Reduce()
        {
            int point = this.point;

            int diff = 0;
            foreach (char alphabet in alphabets)
                diff -= Constant.points[alphabet.ToString()];

            if (diff == 0)
                return;

            this.point = point + diff;
            Debug.Log(name + "\t[Reduce]\t" + alphabets + "\t" + diff + "\t" + point + " -> " + this.point, LogLevel.Reduce);
        }

        private void Draw()
        {
            string alphabets = this.alphabets;
            string diff = "";

            for (int i = 0; i < SIZE - alphabets.Length; i++)
            {
                string alphabet = bag.nextAlphabet;

                if (alphabet == null)
                    break;

                diff = diff + alphabet;
            }

            this.alphabets = alphabets + diff;

            Debug.Log(name + "\t[Draw]\t" + diff + "\t[" + alphabets + "] -> [" + this.alphabets + "]", LogLevel.Draw);
        }

        private void ResetPlay()
        {
            anchorList.Clear();
            moveList.Clear();
        }

        private void AddDefaultAnchor()
        {
            if (slotList.Count > 0)
                return;

            Slot slot = slots[slots.row / 2, slots.column / 2];

            if (slot.alphabet != null)
                return;

            foreach (char alphabet in alphabets)
            {
                string allAlphabets = alphabet.ToString() == Constant.blankAlphabet ? Constant.allAlphabets : alphabet.ToString();

                foreach (char allAlphabet in allAlphabets)
                    foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                        anchorList.AddIfNotExist(new Anchor(slot, allAlphabet.ToString(), "1", direction));
            }
        }

        // PAN -> (P)↓AN
        private void AddExistAnchor()
        {
            foreach (Slot slot in slotList)
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                    if (slot.previous[direction] == null || slot.previous[direction].alphabet == null)
                        anchorList.AddIfNotExist(new Anchor(slot, slot.alphabet, "0", direction));
        }

        // PAN -> S↓PAN
        private void AddHeadAnchor()
        {
            foreach (Slot slot in slotList)
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                    if (slot.previous[direction] != null && slot.previous[direction].alphabet == null && (slot.previous[direction].previous[direction] == null || slot.previous[direction].previous[direction].alphabet == null))
                        foreach (char alphabet in alphabets)
                        {
                            Node node;
                            Slot nextSlot;
                            string allAlphabets = alphabet.ToString() == Constant.blankAlphabet ? Constant.allAlphabets : alphabet.ToString();
                            foreach (char allAlphabet in allAlphabets)
                            {
                                node = root.next[allAlphabet.ToString()];
                                if (node == null)
                                    continue;

                                nextSlot = slot;
                                while (true)
                                {
                                    node = node.next[nextSlot.alphabet.ToString()];
                                    nextSlot = nextSlot.next[direction];

                                    if (node == null)
                                        break;

                                    if (nextSlot == null || nextSlot.alphabet == null)
                                    {
                                        if (node.valid && (slot.previous[direction].previous[direction.Perpendicular()] == null || slot.previous[direction].previous[direction.Perpendicular()].alphabet == null))
                                            anchorList.AddIfNotExist(new Anchor(slot.previous[direction], allAlphabet.ToString(), "1", direction.Perpendicular()));

                                        break;
                                    }
                                }
                            }
                        }
        }

        // PAN -> PANS↓
        private void AddTailAnchor()
        {
            foreach (Slot slot in slotList)
            {
                Slot nextSlot;
                Node node;

                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                    if (slot.previous[direction] == null || slot.previous[direction].alphabet == null)
                    {
                        nextSlot = slot;
                        node = root;
                        while (true)
                        {
                            node = node.next[nextSlot.alphabet];
                            if (node == null)
                                break;

                            nextSlot = nextSlot.next[direction];
                            if (nextSlot == null)
                                break;

                            if (nextSlot.alphabet == null)
                            {
                                foreach (char alphabet in alphabets)
                                {
                                    string allAlphabets = alphabet.ToString() == Constant.blankAlphabet ? Constant.allAlphabets : alphabet.ToString();

                                    foreach (char allAlphabet in allAlphabets)
                                        if (node.next[allAlphabet.ToString()] != null && node.next[allAlphabet.ToString()].valid && (nextSlot.previous[direction.Perpendicular()] == null || nextSlot.previous[direction.Perpendicular()].alphabet == null) && (nextSlot.next[direction] == null || nextSlot.next[direction].alphabet == null))
                                            anchorList.AddIfNotExist(new Anchor(nextSlot, allAlphabet.ToString(), "1", direction.Perpendicular()));
                                }

                                break;
                            }
                        }
                    }
            }
        }

        // P N -> PA↓N
        private void AddCenterAnchor()
        {
            foreach (Slot slot in slotList)
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                    if (slot.previous[direction] != null && slot.previous[direction].alphabet == null && (slot.previous[direction].previous[direction] != null && slot.previous[direction].previous[direction].alphabet != null))
                    {
                        Node startNode = root;
                        Slot startSlot = slot.previous[direction].previous[direction];

                        while (startSlot.previous[direction] != null && startSlot.previous[direction].alphabet != null)
                            startSlot = startSlot.previous[direction];

                        while (startSlot != slot.previous[direction] && startNode != null)
                        {
                            startNode = startNode.next[startSlot.alphabet];
                            startSlot = startSlot.next[direction];
                        }

                        if (startNode == null)
                            continue;

                        foreach (char alphabet in alphabets)
                        {
                            string allAlphabets = alphabet.ToString() == Constant.blankAlphabet ? Constant.allAlphabets : alphabet.ToString();

                            foreach (char allAlphabet in allAlphabets)
                            {
                                Node node = startNode.next[allAlphabet.ToString()];
                                Slot nextSlot = startSlot;

                                if (node == null)
                                    continue;

                                nextSlot = slot;
                                while (true)
                                {
                                    node = node.next[nextSlot.alphabet.ToString()];
                                    nextSlot = nextSlot.next[direction];

                                    if (node == null)
                                        break;

                                    if (nextSlot == null || nextSlot.alphabet == null)
                                    {
                                        if (node.valid && (slot.previous[direction].previous[direction.Perpendicular()] == null || slot.previous[direction].previous[direction.Perpendicular()].alphabet == null))
                                            anchorList.AddIfNotExist(new Anchor(slot.previous[direction], allAlphabet.ToString(), "1", direction.Perpendicular()));

                                        break;
                                    }
                                }
                            }
                        }
                    }
        }

        private void AddAnchor()
        {
            AddDefaultAnchor();
            AddExistAnchor();
            AddHeadAnchor();
            AddTailAnchor();
            AddCenterAnchor();
        }

        private void PermuteForEachAnchor()
        {
            foreach (Anchor anchor in anchorList)
            {
                string alphabets = this.alphabets.Eliminate(anchor.alphabet.IsLower() ? Constant.blankAlphabet : anchor.alphabet);

                if (anchor.newAlphabet == "1")
                    anchor.slot.alphabet = anchor.alphabet;

                int iteration = alphabets.Length;

                Slot slot = anchor.slot;

                while (slot != null && iteration > 0)
                {
                    Slot previousSlot = slot.previous[anchor.direction];

                    if (previousSlot == null || previousSlot.alphabet == null)
                        PermuteForEachStartPoint(slot, anchor, alphabets);

                    if (slot.alphabet == null)
                        iteration--;

                    slot = slot.previous[anchor.direction];
                }

                if (anchor.newAlphabet == "1")
                    anchor.slot.alphabet = null;
            }
        }

        private void PermuteForEachStartPoint(Slot startSlot, Anchor anchor, string alphabets)
        {
            PermuteForEachStartPoint(startSlot, startSlot, anchor, "", "", alphabets, root);
        }

        private void PermuteForEachStartPoint(Slot slot, Slot startSlot, Anchor anchor, string alphabets, string newAlphabets, string nextAlphabets, Node node)
        {
            if ((slot == null || slot.alphabet == null && (anchor.direction == Direction.Horizontal ? slot.column - 1 >= anchor.slot.column : slot.row - 1 >= anchor.slot.row)) && node.valid && newAlphabets.IndexOf("1") >= 0)
                moveList.AddIfNotExist(new Move(startSlot, anchor.direction, alphabets, newAlphabets, nextAlphabets));

            if (slot == null)
                return;

            else if (slot.alphabet == null)
            {
                Slot nextSlot = slot.next[anchor.direction];

                for (int i = 0; i < nextAlphabets.Length; i++)
                {
                    string nextAlphabet = nextAlphabets[i].ToString();
                    Node nextNode;
                    string allAlphabets = nextAlphabet.ToString() == Constant.blankAlphabet ? Constant.allAlphabets : nextAlphabet.ToString();
                    foreach (char allAlphabet in allAlphabets)
                    {
                        nextNode = node.next[allAlphabet.ToString()];
                        if (nextNode != null && ValidPerpendicularly(allAlphabet.ToString(), slot, anchor.direction))
                            PermuteForEachStartPoint(nextSlot, startSlot, anchor, alphabets + allAlphabet.ToString(), newAlphabets + "1", nextAlphabets.Swap(0, i).Substring(1), nextNode);
                    }
                }
            }

            else
            {
                Slot nextSlot = slot.next[anchor.direction];
                Node nextNode = node.next[slot.alphabet];
                if (nextNode != null)
                    PermuteForEachStartPoint(slot.next[anchor.direction], startSlot, anchor, alphabets + slot.alphabet, newAlphabets + (slot == anchor.slot ? anchor.newAlphabet : "0"), nextAlphabets, nextNode);
            }
        }

        private bool ValidPerpendicularly(string alphabet, Slot slot, Direction direction)
        {
            Slot previousSlot = slot.previous[direction.Perpendicular()];
            Slot nextSlot = slot.next[direction.Perpendicular()];

            if ((previousSlot == null || previousSlot.alphabet == null) && (nextSlot == null || nextSlot.alphabet == null))
                return true;

            slot.alphabet = alphabet;

            Slot tempSlot = previousSlot = slot;

            while (true)
            {
                previousSlot = tempSlot.previous[direction.Perpendicular()];
                if (previousSlot == null || previousSlot.alphabet == null)
                    break;

                tempSlot = previousSlot;
            }

            Node node = root;

            while (true)
            {
                if (tempSlot == null || tempSlot.alphabet == null)
                {
                    slot.alphabet = null;
                    return node.valid;
                }

                if (node.next[tempSlot.alphabet.ToString()] == null)
                {
                    slot.alphabet = null;
                    return false;
                }

                node = node.next[tempSlot.alphabet.ToString()];
                tempSlot = tempSlot.next[direction.Perpendicular()];
            }
        }

        private void Evaluate()
        {
            foreach (Move move in moveList)
                move.Evaluate();

            moveList.Sort();
        }

        private void Pass()
        {
            string alphabets = this.alphabets;
            this.alphabets = alphabets;

            Debug.Log(name + "\t[Pass]\t[" + alphabets + "] -> [" + this.alphabets + "]\t+0\t" + point + " -> " + point, LogLevel.Move);
        }

        private void Place()
        {
            if (moveList.Count == 0)
            {
                Pass();
                return;
            }

            Move move = moveList[moveList.Count - 1];
            Place(move.slot, move.direction, move.alphabets, move.newAlphabets);

            alphabets = move.nextAlphabets;

            int point = this.point;
            int diff = move.point;
            this.point = point + move.point;

            Debug.Log(name + "\t[Move]\t" + move.slot.ToString() + "\t" + move.formattedAlphabets + "\t+" + diff + "\t" + point + " -> " + this.point, LogLevel.Move);
        }

        private void Place(Slot slot, Direction direction, string alphabets, string newAlphabets)
        {
            if (alphabets == "")
                return;

            Slot nextSlot = slot.next[direction];

            if (newAlphabets[0].ToString() == "1")
                board.Place(slot, alphabets[0].ToString());

            Place(slot.next[direction], direction, alphabets.Substring(1), newAlphabets.Substring(1));
        }

        private void Play()
        {
            ResetPlay();
            AddAnchor();

            Debug.Log(name + "\t[Anchor]\t" + anchorList.Count, LogLevel.AnchorDetail);
            anchorList.ForEach(x => Debug.Log(x.ToString(), LogLevel.AnchorDetail));
            Debug.Step(LogLevel.AnchorDetail);

            PermuteForEachAnchor();
            Evaluate();

            Debug.Log(name + "\t[Move]\t" + anchorList.Count, LogLevel.MoveDetail);
            moveList.ForEach(x => Debug.Log(x.ToString(), LogLevel.MoveDetail));

            Debug.Log("", LogLevel.MoveDetail);

            Place();
            Debug.Step(LogLevel.Move);

            Debug.Log(board.ToString(), LogLevel.Board);
            Debug.Step(LogLevel.MoveDetail);
        }

        public override string ToString()
        {
            return name + "\t" + point;
        }
    }
}
