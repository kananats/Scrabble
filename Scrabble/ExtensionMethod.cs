using System;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble
{
    static class ExtensionMethod
    {
        public static void Log(this object o, int level = 0)
        {
            Debug.Log(o.ToString(), level);
        }

        public static void Log<T>(this IEnumerable<T> collection, int level = 0)
        {
            foreach (T item in collection)
                item.Log(level);
        }

        public static bool IsUpper(this string s)
        {
            if (s.Any(char.IsLower))
                return false;

            return true;
        }

        public static bool IsLower(this string s)
        {
            if (s.Any(char.IsUpper))
                return false;

            return true;
        }

        public static Direction Perpendicular(this Direction direction)
        {
            return direction == Direction.Horizontal ? Direction.Vertical : Direction.Horizontal;
        }

        public static void AddIfNotExist<T>(this List<T> list, T item)
        {
            if (list.Exists(x => x.Equals(item)))
                return;

            list.Add(item);
        }

        public static string Eliminate(this string s, string alphabet)
        {
            int index = s.IndexOf(alphabet);
            if (index < 0)
                return s;

            string first = s.Substring(0, index);
            string last = s.Substring(index + 1);

            return first + last;
        }

        public static string Swap(this string s, int i, int j)
        {
            if (i == j)
                return s;

            if (i > j)
                return Swap(s, j, i);

            return s.Substring(0, i) + s[j] + s.Substring(i + 1, j - i - 1) + s[i] + s.Substring(j + 1);
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Randomizer.Int(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T RemoveAndGet<T>(this IList<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static int ToIndex(this string s)
        {
            switch (s)
            {
                case "a":
                case "A":
                    return 0;

                case "b":
                case "B":
                    return 1;

                case "c":
                case "C":
                    return 2;

                case "d":
                case "D":
                    return 3;

                case "e":
                case "E":
                    return 4;

                case "f":
                case "F":
                    return 5;

                case "g":
                case "G":
                    return 6;

                case "h":
                case "H":
                    return 7;

                case "i":
                case "I":
                    return 8;

                case "j":
                case "J":
                    return 9;

                case "k":
                case "K":
                    return 10;

                case "l":
                case "L":
                    return 11;

                case "m":
                case "M":
                    return 12;

                case "n":
                case "N":
                    return 13;

                case "o":
                case "O":
                    return 14;

                case "p":
                case "P":
                    return 15;

                case "q":
                case "Q":
                    return 16;

                case "r":
                case "R":
                    return 17;

                case "s":
                case "S":
                    return 18;

                case "t":
                case "T":
                    return 19;

                case "u":
                case "U":
                    return 20;

                case "v":
                case "V":
                    return 21;

                case "w":
                case "W":
                    return 22;

                case "x":
                case "X":
                    return 23;

                case "y":
                case "Y":
                    return 24;

                case "z":
                case "Z":
                    return 25;

                default:
                    throw new Exception("Invalid input string");
            }
        }
    }
}
