using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static int y_length = 0;
        static int x_length = 0;
        static int[][] grid;
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            grid = File.ReadAllLines(filename).Select(line => line.Select(c => c - '0').ToArray()).ToArray();
            y_length = grid.Length;
            x_length = grid[0].Length;

            int flashes = 0;

            var number_of_flashed = 0;

            int step;

            for (step = 0; number_of_flashed < x_length * y_length; step++) {
                number_of_flashed = 0;

                printGrid();

                for (int y = 0; y < y_length; y++) {
                    for (int x = 0; x < y_length; x++) {
                        grid[y][x]++;
                    }
                }

                var flashed = new HashSet<(int, int)>();
                var to_flash = new HashSet<(int, int)>();

                while (true) {

                    for (int y = 0; y < y_length; y++) {
                        for (int x = 0; x < y_length; x++) {
                            if (grid[y][x] > 9 && !flashed.Contains((y, x))) {
                                flashes++;
                                number_of_flashed++;
                                to_flash.Add((y, x));
                            }
                        }
                    }

                    if (to_flash.Count == 0) break;

                    foreach (var flash in to_flash) {
                        foreach (var adj in GetAdj(flash.Item1, flash.Item2)) {
                            grid[adj.Item1][adj.Item2]++;
                        }
                        flashed.Add(flash);
                    }

                    to_flash.Clear();
                }

                for (int y = 0; y < y_length; y++) {
                    for (int x = 0; x < y_length; x++) {
                        if (grid[y][x] > 9) {
                            grid[y][x] = 0;
                        }
                    }
                }
            }

            Console.WriteLine("Step: " + step);
            // 1669 är för stort
        }
        static IEnumerable<(int, int)> GetAdj(int y, int x) {
            if (y > 0) yield return (y - 1, x);
            if (y > 0 && x < x_length - 1) yield return (y - 1, x + 1);
            if (x < x_length - 1) yield return (y, x + 1);
            if (y < y_length - 1 && x < x_length - 1) yield return (y + 1, x + 1);
            if (y < y_length - 1) yield return (y + 1, x);
            if (y < y_length - 1 && x > 0) yield return (y + 1, x - 1);
            if (x > 0) yield return (y, x - 1);
            if (x > 0 && y > 0) yield return (y - 1, x - 1);
        }

        static void printGrid() {
            for (int y = 0; y < y_length; y++) {
                for (int x = 0; x < y_length; x++) {
                    Console.Write(grid[y][x]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}