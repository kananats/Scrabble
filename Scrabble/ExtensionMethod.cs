﻿using System;
using Common;
using System.Collections.Generic;

namespace Scrabble
{
    static class ExtensionMethod
    {
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

        public static int ToIntIndex(this string s)
        {
            s = s.ToLower();
            switch (s)
            {
                case "a":
                    return 0;
                case "b":
                    return 1;
                case "c":
                    return 2;
                case "d":
                    return 3;
                case "e":
                    return 4;
                case "f":
                    return 5;
                case "g":
                    return 6;
                case "h":
                    return 7;
                case "i":
                    return 8;
                case "j":
                    return 9;
                case "k":
                    return 10;
                case "l":
                    return 11;
                case "m":
                    return 12;
                case "n":
                    return 13;
                case "o":
                    return 14;
                case "p":
                    return 15;
                case "q":
                    return 16;
                case "r":
                    return 17;
                case "s":
                    return 18;
                case "t":
                    return 19;
                case "u":
                    return 20;
                case "v":
                    return 21;
                case "w":
                    return 22;
                case "x":
                    return 23;
                case "y":
                    return 24;
                case "z":
                    return 25;
                default:
                    throw new Exception("Invalid input string");
            }
        }
    }
}
