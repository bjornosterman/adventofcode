using System;
using System.Collections.Generic;
using System.IO;

namespace Day22_Shuffle
{
    public class ForwardDeck
    {
        private long _pos;
        public long Pos => _pos;
        private string _input;
        private long _size;

        public ForwardDeck(long size, long pos, string input)
        {
            _size = size;
            _pos = pos;
            _input = input;
        }

        private static IEnumerable<string> GetLines(string text)
        {
            string line;
            StringReader sr = new StringReader(text);
            while ((line = sr.ReadLine()) != null) yield return line;
        }

        public void Run()
        {
            Reverse();
            TakeEveryNth(21);
            Cut(-1639);
            TakeEveryNth(32);
            Cut(-873);
            TakeEveryNth(8);
            Reverse();
            Cut(-7730);
            TakeEveryNth(8);
            Cut(-8408);
            TakeEveryNth(42);
            Cut(-4951);
            Reverse();
            TakeEveryNth(24);
            Cut(-6185);
            TakeEveryNth(69);
            Cut(-930);
            Reverse();
            Cut(8675);
            TakeEveryNth(47);
            Cut(-4543);
            TakeEveryNth(62);
            Reverse();
            TakeEveryNth(23);
            Cut(7128);
            TakeEveryNth(29);
            Reverse();
            TakeEveryNth(65);
            Cut(8232);
            TakeEveryNth(34);
            Reverse();
            TakeEveryNth(7);
            Reverse();
            Cut(-5590);
            TakeEveryNth(34);
            Cut(-3523);
            TakeEveryNth(24);
            Cut(8446);
            TakeEveryNth(42);
            Cut(6714);
            Reverse();
            TakeEveryNth(60);
            Cut(1977);
            TakeEveryNth(51);
            Cut(2719);
            TakeEveryNth(45);
            Cut(9563);
            TakeEveryNth(33);
            Cut(9036);
            TakeEveryNth(70);
            Cut(3372);
            TakeEveryNth(60);
            Cut(9686);
            TakeEveryNth(7);
            Cut(9344);
            TakeEveryNth(13);
            Cut(797);
            TakeEveryNth(12);
            Cut(-6989);
            TakeEveryNth(43);
            Reverse();
            Cut(1031);
            TakeEveryNth(14);
            Cut(-1145);
            TakeEveryNth(26);
            Cut(-9008);
            TakeEveryNth(14);
            Cut(432);
            TakeEveryNth(46);
            Cut(-65);
            TakeEveryNth(50);
            Cut(-704);
            TakeEveryNth(4);
            Cut(7372);
            TakeEveryNth(66);
            Cut(690);
            TakeEveryNth(60);
            Cut(-7137);
            TakeEveryNth(66);
            Cut(9776);
            TakeEveryNth(30);
            Cut(3532);
            TakeEveryNth(62);
            Cut(4768);
            TakeEveryNth(13);
            Reverse();
            Cut(-9014);
            TakeEveryNth(68);
            Cut(-9601);
            TakeEveryNth(6);
            Cut(-7535);
            TakeEveryNth(74);
            Cut(9479);
            TakeEveryNth(6);
            Cut(-1879);
            TakeEveryNth(33);
            Cut(3675);
            TakeEveryNth(19);
            Cut(-937);
            TakeEveryNth(42);
        }
        public void Run2()
        {
            foreach (var line in GetLines(_input))
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

                if (Pos < 0)
                {
                    throw new Exception("Too small");
                }
            }
        }

        private void TakeEveryNth(int step)
        {
            _pos = _pos * step % _size;
        }

        private void Reverse()
        {
            _pos = _size - _pos - 1;
        }

        private void Cut(int input)
        {
            _pos = (_pos + _size - input) % _size;
        }
    }

}
