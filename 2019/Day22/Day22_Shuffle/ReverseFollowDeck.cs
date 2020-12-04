using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day22_Shuffle
{
    public class ReverseFollowDeck
    {
        private long _pos;
        public long Pos => _pos;
        private string _input;
        private long _size;

        public ReverseFollowDeck(long size, long endPosition, string input)
        {
            _size = size;
            _pos = endPosition;
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
            TakeEveryNth(42);
            Cut(-937);
            TakeEveryNth(19);
            Cut(3675);
            TakeEveryNth(33);
            Cut(-1879);
            TakeEveryNth(6);
            Cut(9479);
            TakeEveryNth(74);
            Cut(-7535);
            TakeEveryNth(6);
            Cut(-9601);
            TakeEveryNth(68);
            Cut(-9014);
            Reverse();
            TakeEveryNth(13);
            Cut(4768);
            TakeEveryNth(62);
            Cut(3532);
            TakeEveryNth(30);
            Cut(9776);
            TakeEveryNth(66);
            Cut(-7137);
            TakeEveryNth(60);
            Cut(690);
            TakeEveryNth(66);
            Cut(7372);
            TakeEveryNth(4);
            Cut(-704);
            TakeEveryNth(50);
            Cut(-65);
            TakeEveryNth(46);
            Cut(432);
            TakeEveryNth(14);
            Cut(-9008);
            TakeEveryNth(26);
            Cut(-1145);
            TakeEveryNth(14);
            Cut(1031);
            Reverse();
            TakeEveryNth(43);
            Cut(-6989);
            TakeEveryNth(12);
            Cut(797);
            TakeEveryNth(13);
            Cut(9344);
            TakeEveryNth(7);
            Cut(9686);
            TakeEveryNth(60);
            Cut(3372);
            TakeEveryNth(70);
            Cut(9036);
            TakeEveryNth(33);
            Cut(9563);
            TakeEveryNth(45);
            Cut(2719);
            TakeEveryNth(51);
            Cut(1977);
            TakeEveryNth(60);
            Reverse();
            Cut(6714);
            TakeEveryNth(42);
            Cut(8446);
            TakeEveryNth(24);
            Cut(-3523);
            TakeEveryNth(34);
            Cut(-5590);
            Reverse();
            TakeEveryNth(7);
            Reverse();
            TakeEveryNth(34);
            Cut(8232);
            TakeEveryNth(65);
            Reverse();
            TakeEveryNth(29);
            Cut(7128);
            TakeEveryNth(23);
            Reverse();
            TakeEveryNth(62);
            Cut(-4543);
            TakeEveryNth(47);
            Cut(8675);
            Reverse();
            Cut(-930);
            TakeEveryNth(69);
            Cut(-6185);
            TakeEveryNth(24);
            Reverse();
            Cut(-4951);
            TakeEveryNth(42);
            Cut(-8408);
            TakeEveryNth(8);
            Cut(-7730);
            Reverse();
            TakeEveryNth(8);
            Cut(-873);
            TakeEveryNth(32);
            Cut(-1639);
            TakeEveryNth(21);
            Reverse();
        }

        public void Run2()
        {
            foreach (var line in GetLines(_input).Reverse())
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

        public static long OriginLong(long pos, long step, long size)
        {
            long small = pos / step;
            long rest = pos % step;
            long slip = step - size % step;
            var inner = rest == 0 ? 0 : OriginLong(rest, slip, step);
            var ret = small + (rest == 0 ? 0 : 1) + (size * inner / step);
            //if (ret * step % size != pos)
            //{
            //    throw new Exception("Assert");
            //}
            //Console.WriteLine($"Origin({pos}, {step}, {size}) => {ret} : if correct = " + (ret * step % size));
            return ret;
        }

        public static BigInteger Origin(BigInteger pos, BigInteger step, BigInteger size)
        {
            BigInteger small = pos / step;
            BigInteger rest = pos % step;
            BigInteger slip = step - size % step;
            var inner = rest == 0 ? 0 : Origin(rest, slip, step);
            var ret = small + (rest == 0 ? 0 : 1) + (size * inner / step);
            //if (ret * step % size != pos)
            //{
            //    throw new Exception("Assert");
            //}
            //Console.WriteLine($"Origin({pos}, {step}, {size}) => {ret} : if correct = " + (ret * step % size));
            return ret;
        }


        private void TakeEveryNth(int step)
        {
            _pos = OriginLong(_pos, step, _size);
            // 4126
            // 4126 %/ 7 = 589:3
            // step-slip 2
            // 

            //  size /% step =
            // 10007 /%    7 = 1429:4
            //     7 /%    4 =    1:3
            //     4 /%    3 =    1:2
            //     3

            //  4126 /%    7 =  589:1
            //
            //

            // step:23
            // size 10007
            // 2019 * 23 % 10007 = 6409
            //  6409 /%   23 = 278 : 15 
            //    15 /%   21 =   0 : 15
            //    15 /%   19 =   0 : 15
            //    15 /%   17 =   0 : 15
            //    15 /%   15 =   1 : 0

            //long Origin(long pos, long step, long size)
            //{
            //    long loops = pos / step;
            //    long rest = pos % step;
            //    long slip = step - rest;
            //    return loops + size % step * Origin(rest, slip, step);
            //}



            // 10007 /%   23 = 435 :  2
            //    23 /%   21 =   1 :  2
            //    21 /%   19 =   1 :  2
            //    19 /%   17 =   1 :  2
            //    17 /%   15 =   1 :  2
            //    15 /%   13 =   1 :  2
            //    13 /%   11 =   1 :  2


            // pos % step = current_slip
            // step_lip = size%step
            //var x2019 = 2019 * 7 % 10007;
            //var y2019 = vet_ej * 7 % 10007;
            //var slip = _size % step;
            //var slip2 =

            // 10 % 3 : 0 7 4 1 8 5 2 9 6 3 
            // 3-1=2
            // number
            // 10007
            // 7
            // 10007/7=1429
            // 10003

            // i => i*step%size
            //_pos = _pos % step * step + _pos / step;
        }

        private void Reverse()
        {
            _pos = _size - _pos - 1;
        }

        private void Cut(int input)
        {
            _pos = (_pos + _size + input) % _size;
        }
    }


}
