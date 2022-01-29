using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename).ToList();
            var line_digits = lines.Select(x => x.Split(" | ")[1].Split(' ').ToArray()).ToArray();

            var easy_digits = line_digits.Select(x => x.Count(y => y.Length == 2 || y.Length == 3 || y.Length == 4 || y.Length == 7)).ToList();

            Console.WriteLine("Easy digits = " + easy_digits.Sum());

        }
    }
}
