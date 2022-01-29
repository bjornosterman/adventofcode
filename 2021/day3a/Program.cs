using System;
using System.IO;
using System.Linq;

namespace day2b
{
    class Program
    {
        static void Main()
        {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";
            var lines = File.ReadAllLines(filename);
            var value = 0;
            var value2 = 0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                var one_diff = 0;
                foreach (var line in lines)
                {
                    one_diff += line[i] == '1' ? 1 : -1;
                }
                value = value * 2 + (one_diff > 0 ? 1 : 0);
                value2 = value2 * 2 + (one_diff > 0 ? 0 : 1);
            }
            Console.WriteLine($"{value} * {value2} = {value * value2}");
        }
    }
}
