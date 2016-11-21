using Common;
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

        private string alphabets;

        public Player(string name)
        {
            this.name = name;

            anchorList = new List<Anchor>();
            moveList = new List<Move>();
        }

        public void Reset(int level)
        {
            alphabets = "";

            this.level = level < 1 ? 1 : (level > 100 ? 100 : level);

            point = 0;

            Draw();
        }

        public void Step()
        {
            Play();
            Draw();
            CheckEndGame();
        }

        private void Draw()
        {
            string s = name + " draws ";

            while (alphabets.Length < SIZE)
            {
                string alphabet = Game.instance.bag.GetAlphabet();

                if (alphabet == null)
                    break;

                s = s + alphabet + (alphabets.Length == SIZE - 1 ? "" : ", ");

                alphabets = alphabets + alphabet;
            }

            Debug.Log(s);
        }

        private void CheckEndGame()
        {
            if (alphabets.Length == 0)
                Game.instance.Finish();

            else
                Game.instance.Step();
        }

        List<Anchor> anchorList;
        List<Move> moveList;

        private void ResetPlay()
        {
            anchorList.Clear();
            moveList.Clear();
        }

        private void AddDefaultAnchor()
        {
            Board board = Game.instance.board;

            if (board.slotList.Count > 0)
                return;

            Slots slots = board.slots;
            Slot slot = slots[slots.row / 2, slots.column / 2];

            if (slot.alphabet != null)
                return;

            foreach (char alphabet in alphabets)
            {
                anchorList.AddIfNotExist(new Anchor(slot, alphabet.ToString(), "1", Direction.Horizontal));
                anchorList.AddIfNotExist(new Anchor(slot, alphabet.ToString(), "1", Direction.Vertical));
            }
        }

        // PAN -> (P)↓AN
        private void AddExistAnchor()
        {
            Slots slots = Game.instance.board.slots;
            List<Slot> slotList = Game.instance.board.slotList;

            foreach (Slot slot in slotList)
            {
                // Horizontal
                if (slot.left == null || slot.left.alphabet == null)
                    anchorList.AddIfNotExist(new Anchor(slot, slot.alphabet, "0", Direction.Horizontal));

                // Vertical
                if (slot.top == null || slot.top.alphabet == null)
                    anchorList.AddIfNotExist(new Anchor(slot, slot.alphabet, "0", Direction.Vertical));
            }
        }

        // P N -> PA↓N
        private void AddCenterAnchor()
        {
            Slots slots = Game.instance.board.slots;
            List<Slot> slotList = Game.instance.board.slotList;

            foreach (Slot slot in slotList)
            {
                // Horizontal -> Vertical
                if (slot.left != null && slot.left.alphabet == null && (slot.left.left != null && slot.left.left.alphabet != null))
                {
                    Node startNode = Game.instance.dictionary.root;
                    Slot startSlot = slot.left.left;

                    while (startSlot.left != null && startSlot.left.alphabet != null)
                        startSlot = startSlot.left;

                    while (startSlot != slot.left)
                    {
                        startNode = startNode.next[startSlot.alphabet];
                        startSlot = startSlot.right;
                    }

                    for (int i = 0; i < alphabets.Length; i++)
                    {
                        Node node = startNode.next[alphabets[i].ToString()];
                        Slot nextSlot = startSlot;

                        if (node == null)
                            continue;

                        nextSlot = slot;
                        while (true)
                        {
                            node = node.next[nextSlot.alphabet.ToString()];
                            nextSlot = nextSlot.right;

                            if (node == null)
                                break;

                            if (nextSlot == null || nextSlot.alphabet == null)
                            {
                                if (node.valid && (slot.left.top == null || slot.left.top.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(slot.left, alphabets[i].ToString(), "1", Direction.Vertical));

                                break;
                            }
                        }
                    }
                }

                // Vertical -> Horizontal
                if (slot.top != null && slot.top.alphabet == null && (slot.top.top != null && slot.top.top.alphabet != null))
                {
                    Node startNode = Game.instance.dictionary.root;
                    Slot startSlot = slot.top.top;

                    while (startSlot.top != null && startSlot.top.alphabet != null)
                        startSlot = startSlot.top;

                    while (startSlot != slot.top)
                    {
                        startNode = startNode.next[startSlot.alphabet];
                        startSlot = startSlot.bottom;
                    }

                    for (int i = 0; i < alphabets.Length; i++)
                    {
                        Node node = startNode.next[alphabets[i].ToString()];
                        Slot nextSlot = startSlot;

                        if (node == null)
                            continue;

                        nextSlot = slot;
                        while (true)
                        {
                            node = node.next[nextSlot.alphabet.ToString()];
                            nextSlot = nextSlot.bottom;

                            if (node == null)
                                break;

                            if (nextSlot == null || nextSlot.alphabet == null)
                            {
                                if (node.valid && (slot.top.left == null || slot.top.left.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(slot.top, alphabets[i].ToString(), "1", Direction.Horizontal));

                                break;
                            }
                        }
                    }
                }
            }
        }

        // PAN -> S↓PAN
        private void AddHeadAnchor()
        {
            Slots slots = Game.instance.board.slots;
            List<Slot> slotList = Game.instance.board.slotList;

            foreach (Slot slot in slotList)
            {
                Node node;

                // Horizontal -> Vertical
                if (slot.left != null && slot.left.alphabet == null && (slot.left.left == null || slot.left.left.alphabet == null))
                {
                    Node root = Game.instance.dictionary.root;
                    Slot nextSlot;

                    for (int i = 0; i < alphabets.Length; i++)
                    {
                        node = root.next[alphabets[i].ToString()];
                        if (node == null)
                            continue;

                        nextSlot = slot;
                        while (true)
                        {
                            node = node.next[nextSlot.alphabet.ToString()];
                            nextSlot = nextSlot.right;

                            if (node == null)
                                break;

                            if (nextSlot == null || nextSlot.alphabet == null)
                            {
                                if (node.valid && (slot.left.top == null || slot.left.top.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(slot.left, alphabets[i].ToString(), "1", Direction.Vertical));

                                break;
                            }
                        }
                    }
                }

                // Vertical -> Horizontal
                if (slot.top != null && slot.top.alphabet == null && (slot.top.top == null || slot.top.top.alphabet == null))
                {
                    Node root = Game.instance.dictionary.root;
                    Slot nextSlot;

                    for (int i = 0; i < alphabets.Length; i++)
                    {
                        node = root.next[alphabets[i].ToString()];
                        if (node == null)
                            continue;

                        nextSlot = slot;
                        while (true)
                        {
                            node = node.next[nextSlot.alphabet.ToString()];
                            nextSlot = nextSlot.bottom;

                            if (node == null)
                                break;

                            if (nextSlot == null || nextSlot.alphabet == null)
                            {
                                if (node.valid && (slot.top.left == null || slot.top.left.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(slot.top, alphabets[i].ToString(), "1", Direction.Horizontal));

                                break;
                            }
                        }
                    }
                }
            }
        }

        // PAN -> PANS↓
        private void AddTailAnchor()
        {
            Slots slots = Game.instance.board.slots;
            List<Slot> slotList = Game.instance.board.slotList;

            foreach (Slot slot in slotList)
            {
                Slot nextSlot;
                Node node;

                // Horizontal -> Vertical
                if (slot.left == null || slot.left.alphabet == null)
                {
                    nextSlot = slot;
                    node = Game.instance.dictionary.root;
                    while (true)
                    {
                        node = node.next[nextSlot.alphabet];
                        nextSlot = nextSlot.right;

                        if (nextSlot == null)
                            break;

                        if (nextSlot.alphabet == null)
                        {
                            for (int i = 0; i < alphabets.Length; i++)
                                if (node.next[alphabets[i].ToString()] != null && node.next[alphabets[i].ToString()].valid && (nextSlot.top == null || nextSlot.top.alphabet == null) && (nextSlot.right == null || nextSlot.right.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(nextSlot, alphabets[i].ToString(), "1", Direction.Vertical));

                            break;
                        }
                    }
                }

                // Vertical -> Horizontal
                if (slot.top == null || slot.top.alphabet == null)
                {
                    nextSlot = slot;
                    node = Game.instance.dictionary.root;
                    while (true)
                    {
                        node = node.next[nextSlot.alphabet];
                        nextSlot = nextSlot.bottom;

                        if (nextSlot == null)
                            break;

                        if (nextSlot.alphabet == null)
                        {
                            for (int i = 0; i < alphabets.Length; i++)
                                if (node.next[alphabets[i].ToString()] != null && node.next[alphabets[i].ToString()].valid && (nextSlot.left == null || nextSlot.left.alphabet == null) && (nextSlot.bottom == null || nextSlot.bottom.alphabet == null))
                                    anchorList.AddIfNotExist(new Anchor(nextSlot, alphabets[i].ToString(), "1", Direction.Horizontal));

                            break;
                        }
                    }
                }
            }
        }

        private void AddAnchor()
        {
            AddExistAnchor();
            AddHeadAnchor();
            AddCenterAnchor();
            AddTailAnchor();
            AddDefaultAnchor();
        }

        private void PermuteForEachAnchor()
        {
            foreach (Anchor anchor in anchorList)
            {
                string alphabets = this.alphabets.Eliminate(anchor.alphabet);
                Slots slots = Game.instance.board.slots;

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
            PermuteForEachStartPoint(startSlot, startSlot, anchor, "", "", alphabets, Game.instance.dictionary.root);
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
                    Node nextNode = node.next[nextAlphabets[i].ToString()];
                    if (nextNode != null && ValidPerpendicularly(nextAlphabets[i].ToString(), slot, anchor.direction))
                        PermuteForEachStartPoint(nextSlot, startSlot, anchor, alphabets + nextAlphabets[i], newAlphabets + "1", nextAlphabets.Swap(0, i).Substring(1), nextNode);
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

            Node node = Game.instance.dictionary.root;

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

        private void Place()
        {
            if (moveList.Count == 0)
            {
                Debug.Log(name + "\t" + "Passed");
                return;
            }

            Move move = moveList[moveList.Count - 1];
            Place(move.slot, move.direction, move.alphabets, move.newAlphabets);

            alphabets = move.nextAlphabets;
            point = point + move.point;

            Debug.Log(name + "\t" + move.slot.ToString() + "\t" + move.formattedAlphabets + "\t" + move.point);
        }

        private void Place(Slot slot, Direction direction, string alphabets, string newAlphabets)
        {
            if (alphabets == "")
                return;

            Slot nextSlot = slot.next[direction];

            if (newAlphabets[0].ToString() == "1")
                Game.instance.board.Place(slot, alphabets[0].ToString());

            Place(slot.next[direction], direction, alphabets.Substring(1), newAlphabets.Substring(1));
        }

        private void Play()
        {
            ResetPlay();
            AddAnchor();
            Debug.Log("===ANCHOR===" + "(" + anchorList.Count + ")");
            //foreach (Anchor anchor in anchorList)
            //    Debug.Log(anchor.ToString());
            Debug.Log();

            PermuteForEachAnchor();
            Evaluate();

            Debug.Log("===MOVE===" + "(" + moveList.Count + ")");
            //foreach (Move move in moveList)
            //    Debug.Log(move.ToString());
            Debug.Log();

            Debug.Log("===PLACE===");
            Place();
            Debug.Log();

            PrintBoard();
        }

        private void PrintBoard()
        {
            Debug.Log("===BOARD===");
            Debug.Log(Game.instance.board.ToString());
            Debug.Log();
        }
    }
}
