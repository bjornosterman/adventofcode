using System.Numerics;

namespace Day22_Shuffle
{
    public static class Input
    {
        public static string TestAll =
            @"
cut 6
deal with increment 7
deal into new stack
";
        public static string Test1 =
            @"
deal with increment 7
deal into new stack
deal into new stack
";
        public static string Test2 =
            @"
deal with increment 7
deal with increment 9
cut -2";
        public static string TestCut =
            @"cut 3";

        public static string Test4 =
            @"
deal into new stack
cut -2
deal with increment 7
cut 8
cut -4
deal with increment 7
cut 3
deal with increment 9
deal with increment 3
cut -1";
        public static string Puzzle = @"
deal into new stack
deal with increment 21
cut -1639
deal with increment 32
cut -873
deal with increment 8
deal into new stack
cut -7730
deal with increment 8
cut -8408
deal with increment 42
cut -4951
deal into new stack
deal with increment 24
cut -6185
deal with increment 69
cut -930
deal into new stack
cut 8675
deal with increment 47
cut -4543
deal with increment 62
deal into new stack
deal with increment 23
cut 7128
deal with increment 29
deal into new stack
deal with increment 65
cut 8232
deal with increment 34
deal into new stack
deal with increment 7
deal into new stack
cut -5590
deal with increment 34
cut -3523
deal with increment 24
cut 8446
deal with increment 42
cut 6714
deal into new stack
deal with increment 60
cut 1977
deal with increment 51
cut 2719
deal with increment 45
cut 9563
deal with increment 33
cut 9036
deal with increment 70
cut 3372
deal with increment 60
cut 9686
deal with increment 7
cut 9344
deal with increment 13
cut 797
deal with increment 12
cut -6989
deal with increment 43
deal into new stack
cut 1031
deal with increment 14
cut -1145
deal with increment 26
cut -9008
deal with increment 14
cut 432
deal with increment 46
cut -65
deal with increment 50
cut -704
deal with increment 4
cut 7372
deal with increment 66
cut 690
deal with increment 60
cut -7137
deal with increment 66
cut 9776
deal with increment 30
cut 3532
deal with increment 62
cut 4768
deal with increment 13
deal into new stack
cut -9014
deal with increment 68
cut -9601
deal with increment 6
cut -7535
deal with increment 74
cut 9479
deal with increment 6
cut -1879
deal with increment 33
cut 3675
deal with increment 19
cut -937
deal with increment 42";

        public static string Tmp = @"
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
";
    }

    public static class MathExtensions
    {
        public static BigInteger Mod(this BigInteger value, BigInteger mod)
        {
            var ret = value % mod;
            return ret < 0 ? ret + mod : ret;
        }

        public static BigInteger Pow(this BigInteger value, int pow)
        {
            return BigInteger.Pow(value, pow);
        }

        public static BigInteger Origin(this BigInteger pos, BigInteger step, BigInteger size)
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

        public static BigInteger PowerSeries(this BigInteger bas, int start, int end)
        {
            var a = BigInteger.Pow(bas, start);
            var b = BigInteger.Pow(bas, end + 1);
            return (a - b) / (1 - bas);
        }
    }
}
