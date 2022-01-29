using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static int[][] grid;
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);

            var scores = lines.Select(GetPoint).Where(x => x > 0).OrderBy(x => x).ToList();

            var middle_score = scores[(int)(scores.Count / 2)];

            Console.WriteLine("Middlescore: " + middle_score);
            // 854427058 är för lågt
        }
        static long GetPoint(string line) {
            var stack = new List<char>();
            foreach (var c in line) {
                if ("({<[".Contains(c)) {
                    stack.Add(c);
                }
                else if (!stack.Any()) {
                    Console.WriteLine("no stack!");
                }
                else {
                    var expected = "({<["[")}>]".IndexOf(c)];
                    if (stack.Last() != expected) {
                        return -1;
                    }
                    stack.RemoveAt(stack.Count() - 1);
                }
            }
            long points = 0;
            stack.Reverse();
            foreach (var c in stack) {
                points *= 5;
                points += "_([{<".IndexOf(c);
            }
            return points;
        }
    }
}