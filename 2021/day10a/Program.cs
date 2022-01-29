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

            var points = 0;
            foreach (var line in lines) {
                points += GetPoint(line);
            }

            Console.WriteLine("Points: " + points);
        }
        static int GetPoint(string line) {
            var values = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
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
                        return values[c];
                    }
                    stack.RemoveAt(stack.Count() - 1);
                }
            }
            return 0;
        }
    }
}