using System;
using System.IO;
using System.Linq;

namespace Advent_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = File.ReadAllLines("input.txt").Select(x => OneCalc(int.Parse(x))).Sum();
            Console.WriteLine("Sum: " + sum);
        }

        static int OneCalc(int input)
        {
            var gurka = (input / 3) - 2;
            return gurka > 0 ? gurka + OneCalc(gurka) : 0;
        }

    }
}
