using System;

namespace ElvesPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            var min = 172851;
            var max = 675869;
            var count = 0;
            for (int a = 0; a < 10; a++)
                for (int b = a; b < 10; b++)
                    for (int c = b; c < 10; c++)
                        for (int d = c; d < 10; d++)
                            for (int e = d;e < 10; e++)
                                for (int f = e; f < 10; f++)
                                {
                                    var value = a * 100000 + b * 10000 + c * 1000 + d * 100 + e * 10 + f;
                                    if (value >= min && value <= max)
                                    {
                                        if (
                                            (a == b && b != c)
                                            || (b == c && a != b && c != d)
                                            || (c == d && b != c && d != e)
                                            || (d == e && c != d && e != f)
                                            || (e == f && d != e)
                                            )
                                        {
                                            count++;
                                        }
                                    }
                                }
            Console.WriteLine("Count all = " + count);
        }
    }
}
