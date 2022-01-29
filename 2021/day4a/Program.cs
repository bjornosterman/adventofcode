using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day4a {
    class Program {
        static void Main() {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename).ToList();
            var file = File.OpenRead(filename);
            var reader = new StreamReader(file);
            var balls = reader.ReadLine().Split(',').Select(int.Parse).ToList();

            var grids = new List<int[,]>();

            while (reader.ReadLine() == "") {
                var grid = new int[5, 5];
                for (int y = 0; y < 5; y++) {
                    var row = reader.ReadLine().Split(' ').Where(x => x.Trim().Any()).Select(int.Parse).ToArray();
                    for (int x = 0; x < 5; x++) {
                        grid[x, y] = row[x];
                    }
                    grids.Add(grid);
                }
            }

            foreach (var ball in balls) {
                foreach (var grid in grids) {
                    for (int y = 0; y < 5; y++) {
                        for (int x = 0; x < 5; x++) {
                            if (grid[x, y] == ball) {
                                grid[x, y] = 0;
                            }
                        }
                    }
                }

                foreach (var grid in grids) {
                    for (int y = 0; y < 5; y++) {
                        var v = 0;
                        for (int x = 0; x < 5; x++) {
                            v += grid[x, y];
                        }
                        if (v == 0) {
                            // Bingo!
                            var sum = GetSum(grid);
                            Console.WriteLine($"{sum} * {ball} = {sum * ball}");
                            return;
                        }
                    }
                }

                foreach (var grid in grids) {
                    for (int y = 0; y < 5; y++) {
                        var v = 0;
                        for (int x = 0; x < 5; x++) {
                            v += grid[y, x];
                        }
                        if (v == 0) {
                            // Bingo!
                            var sum = GetSum(grid);
                            Console.WriteLine($"{sum} * {ball} = {sum * ball}");
                            return;
                        }
                    }
                }
            }
        }

        private static int GetSum(int[,] grid) {
            var sum = 0;
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    sum += grid[y, x];
                }
            }
            return sum;
        }
    }
}