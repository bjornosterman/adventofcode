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

            grid = File.ReadAllLines(filename).Select(line => line.Select(c => c - '0').ToArray()).ToArray();

            var lakes = new List<int>();

            for (var y = 0; y < grid.Length; y++) {
                for (var x = 0; x < grid[0].Length; x++) {
                    lakes.Add(sumLake(y, x));
                }
            }

            var sum = lakes.OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b);

            Console.WriteLine("Lake-sum: " + sum);
        }

        static int sumLake(int y, int x) {
            var y_length = grid.Length;
            var x_length = grid[0].Length;
            if (grid[y][x] == 9) return 0;
            grid[y][x] = 9;
            int sum = 1;
            if (y > 0) sum += sumLake(y - 1, x);
            if (y < y_length - 1) sum += sumLake(y + 1, x);
            if (x > 0) sum += sumLake(y, x - 1);
            if (x < x_length - 1) sum += sumLake(y, x + 1);
            return sum;
        }

    }
}