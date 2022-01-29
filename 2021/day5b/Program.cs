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
            var rects = new List<(int, int, int, int)>();
            foreach (var line in lines) {
                var split = line.Split(" -> ");
                var split2 = split[0].Split(',');
                var x1 = int.Parse(split2[0]);
                var y1 = int.Parse(split2[1]);
                split2 = split[1].Split(',');
                var x2 = int.Parse(split2[0]);
                var y2 = int.Parse(split2[1]);
                rects.Add((x1, y1, x2, y2));
            }

            var max_x = rects.Max(x => Math.Max(x.Item1, x.Item3)) + 1;
            var max_y = rects.Max(x => Math.Max(x.Item2, x.Item4)) + 1;
            var grid = new int[max_x, max_y];

            foreach (var rect in rects) {
                var x1 = rect.Item1;
                var y1 = rect.Item2;
                var x2 = rect.Item3;
                var y2 = rect.Item4;
                var step_x = x1 < x2 ? 1 : x1 == x2 ? 0 : -1;
                var step_y = y1 < y2 ? 1 : y1 == y2 ? 0 : -1;
                var steps = Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
                for (var a = 0; a <= steps; a++) {
                    grid[x1, y1]++;
                    x1 += step_x;
                    y1 += step_y;
                }
            }

            var dangers = 0;
            for (var x = 0; x < max_x; x++)
                for (var y = 0; y < max_y; y++)
                    if (grid[x, y] > 1) dangers++;

            for (var x = 0; x < max_x; x++) {
                Console.Write("|");
                for (var y = 0; y < max_y; y++) {
                    Console.Write(grid[x, y] == 0 ? "." : grid[x, y]);
                }
                Console.WriteLine();
            }


            Console.WriteLine("Dangers: " + dangers);

        }
    }
}
