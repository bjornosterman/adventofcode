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

            var height = lines.Length;
            var width = lines[0].Length;

            void Copy(char[,] from, char[,] to) {
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        to[x, y] = from[x, y];
                    }
                }
            }

            void Clear(char[,] elGrid) {
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        elGrid[x, y] = '.';
                    }
                }
            }

            var grid = new char[width, height];
            var grid2 = new char[width, height];

            void Print(char[,] elGrid) {
                if (is_test == false) return;
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        Console.ForegroundColor = elGrid[x, y] switch {
                            '>' => ConsoleColor.Cyan,
                            'v' => ConsoleColor.Green,
                            '.' => ConsoleColor.Gray,
                        };
                        Console.Write(elGrid[x, y]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }



            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    grid[x, y] = lines[y][x];
                }
            }

            int step = 0;

            while (true) {
                Console.WriteLine("STEP: " + (step++));
                Print(grid);
                var is_dirty = false;
                Clear(grid2);
                // EAST
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        var spot = grid[x, y];
                        if (spot == '>') {
                            var next = grid[(x + 1) % width, y];
                            if (next == '.') {
                                grid2[(x + 1) % width, y] = '>';
                                is_dirty = true;
                            }
                            else {
                                grid2[x, y] = spot;
                            }
                        }
                        if (spot == 'v') {
                            grid2[x, y] = spot;
                        }
                    }
                }

                Copy(grid2, grid);

                Clear(grid2);

                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        var spot = grid[x, y];
                        if (spot == 'v') {
                            var next = grid[x, (y + 1) % height];
                            if (next == '.') {
                                grid2[x, (y + 1) % height] = 'v';
                                is_dirty = true;
                            }
                            else {
                                grid2[x, y] = spot;
                            }
                        }
                        if (spot == '>') {
                            grid2[x, y] = spot;
                        }
                    }
                }

                Copy(grid2, grid);

                if (is_dirty == false) break;
            }
            Console.WriteLine("STEP: " + (step++));
            Print(grid);
        }
    }
}
