using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static bool isLow(int[][] grid, int y, int x) {
            var v = grid[y][x];
            var y_length = grid.Length;
            var x_length = grid[0].Length;

            if (y > 0 && v >= grid[y - 1][x]) return false;
            if (y < y_length - 1 && v >= grid[y + 1][x]) return false;
            if (x > 0 && v >= grid[y][x - 1]) return false;
            if (x < x_length - 1 && v >= grid[y][x + 1]) return false;
            return true;
        }
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var grid = File.ReadAllLines(filename).Select(line => line.Select(c => c - '0').ToArray()).ToArray();

            var risk = 0;

            for (var y = 0; y < grid.Length; y++) {
                for (var x = 0; x < grid[0].Length; x++) {
                    if (isLow(grid, y, x)) {
                        risk += grid[y][x] + 1;
                    }
                }
            }

            Console.WriteLine("Risk: " + risk);
        }
    }
}