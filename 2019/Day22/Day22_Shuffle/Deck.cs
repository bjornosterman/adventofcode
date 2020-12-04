using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22_Shuffle
{
    public class Deck
    {
        private readonly int _size;
        int[] _cards;
        public int[] Cards => _cards;
        int _index = 0;
        public Deck(int size)
        {
            _cards = Enumerable.Range(0, size).ToArray();
            _size = size;
        }

        internal void Cut(int index)
        {
            var cards2 = new int[_size];
            for (int i = 0; i < _size; i++)
            {
                cards2[(i + _size - index) % _size] = _cards[i];
            }
            _cards = cards2;
        }

        internal void Reverse()
        {
            _cards = _cards.Reverse().ToArray();
        }

        internal void TakeEveryNth(int step)
        {
            var cards2 = new int[_size];
            for (int i = 0, j = 0; i < _size; i++, j += step)
            {
                cards2[j % _size] = _cards[i];
            }
            _cards = cards2;
        }
        public override string ToString()
        {
            return string.Join(" ", _cards);
        }

        private static IEnumerable<string> GetLines(string text)
        {
            string line;
            StringReader sr = new StringReader(text);
            while ((line = sr.ReadLine()) != null) yield return line;
        }

        internal void Run()
        {
            foreach (var line in GetLines(Input.Puzzle))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("cut "))
                {
                    Cut(int.Parse(line.Substring(4)));
                }
                else if (line.StartsWith("deal with increment "))
                {
                    TakeEveryNth(int.Parse(line.Substring("deal with increment ".Length)));
                }
                else if (line == "deal into new stack")
                {
                    Reverse();
                }
                else
                {
                    throw new Exception("Unknown string");
                }

            }
        }
    }


}
