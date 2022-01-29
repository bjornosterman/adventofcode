using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);

            var transforms = new Dictionary<string, string>();

            foreach (var line in lines.Skip(2)) {
                var split = line.Split(" -> ");
                transforms.Add(split[0], split[1]);
            }

            var theLine = lines[0];

            for (int s = 0; s < 10; s++) {
                var nextLine = "";

                for (int i = 0; i < theLine.Count() - 1; i++) {
                    var pair = theLine.Substring(i, 2);
                    nextLine += pair[0];
                    if (transforms.ContainsKey(pair)) {
                        nextLine += transforms[pair];
                    }
                }
                theLine = nextLine + theLine[theLine.Length - 1];
                // Console.WriteLine("Step " + (s + 1) + ": " + theLine.Length);
                Console.Write("Step " + (s + 1) + ": ");
                foreach (var x in theLine.ToLookup(x => x).OrderBy(x => x.Key)) {
                    Console.Write($"{x.Key}: {x.Count():000} ");
                }
                Console.WriteLine();
            }

            var chars_dict = theLine.ToLookup(x => x);
            var max = chars_dict.Select(x => x.Count()).Max();
            var min = chars_dict.Select(x => x.Count()).Min();

            Console.WriteLine(max + " - " + min + " = " + (max - min));

        }
    }
}