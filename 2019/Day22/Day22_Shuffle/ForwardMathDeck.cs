using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace Day22_Shuffle
{
    public class ForwardMathDeck
    {
        private BigInteger _pos;
        public BigInteger Pos => (_pos * Multiplier + Shifter).Mod(Size);
        private BigInteger _posMultiplier = 1;
        public BigInteger Multiplier => _posMultiplier.Mod(Size);
        private BigInteger _posShifter = 0;
        public BigInteger Shifter => _posShifter.Mod(Size);

        public BigInteger Size { get; }

        public BigInteger GetOrigin(BigInteger pos)
        {
            return (pos - Shifter).Mod(Size).Origin(Multiplier, Size);
        }

        public BigInteger GetPos(BigInteger pos)
        {
            return (pos * Multiplier + Shifter).Mod(Size);
        }

        public override string ToString()
        {
            return $"Card {_pos} is now in position {Pos}";
        }
        public ForwardMathDeck(BigInteger size, BigInteger pos)
        {
            Size = size;
            _pos = pos;
        }

        private static IEnumerable<string> GetLines(string text)
        {
            string line;
            StringReader sr = new StringReader(text);
            while ((line = sr.ReadLine()) != null) yield return line;
        }

        internal void Multiply(int mul)
        {
            _posShifter = (_posShifter * _posMultiplier.PowerSeries(0, mul - 1)).Mod(Size);
            _posMultiplier = BigInteger.ModPow(_posMultiplier, mul, Size);
        }

        public void Apply(BigInteger multiplier, BigInteger shifter)
        {
            _posMultiplier = (_posMultiplier * multiplier).Mod(Size);
            _posShifter = (_posShifter * multiplier + shifter).Mod(Size);
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
        public void RunWithInput(string input)
        {
            foreach (var line in GetLines(input))
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
                else if (line.StartsWith("nth "))
                {
                    TakeEveryNth(int.Parse(line.Substring("nth ".Length)));
                }
                else if (line == "deal into new stack" || line == "rev")
                {
                    Reverse();
                }
                else
                {
                    throw new Exception("Unknown string");
                }

                //if (Pos < 0)
                //{
                //    throw new Exception("Too small");
                //}
            }
        }

        private void TakeEveryNth(BigInteger step)
        {
            _posMultiplier = (_posMultiplier * step).Mod(Size);
            _posShifter = (_posShifter * step).Mod(Size);
        }

        private void Reverse()
        {
            _posMultiplier = (-_posMultiplier).Mod(Size);
            _posShifter = (-_posShifter - 1).Mod(Size);
        }

        private void Cut(BigInteger input)
        {
            _posShifter = (_posShifter - input).Mod(Size);
        }
    }

}
