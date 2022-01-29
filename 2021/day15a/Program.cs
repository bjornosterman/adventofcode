using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var is_b = true;
            var filename = is_test ? "sample.txt" : "input.txt";
            var dup = is_b ? 5 : 1;

            var lines = File.ReadAllLines(filename);
            var y_len = lines.Length;
            var x_len = lines[0].Length;

            var init = new int[y_len, x_len];
            var grid = new int[y_len * dup, x_len * dup];
            var grid2 = new int[y_len * dup, x_len * dup];

            for (int y = 0; y < y_len; y++) {
                for (int x = 0; x < x_len; x++) {
                    init[y, x] = lines[y][x] - '0';
                }
            }

            for (int dy = 0; dy < dup; dy++)
                for (int dx = 0; dx < dup; dx++)
                    for (int y = 0; y < y_len; y++)
                        for (int x = 0; x < x_len; x++) {
                            grid[dy * y_len + y, dx * x_len + x] = (((init[y, x] + dy + dx) - 1) % 9) + 1;
                            grid2[dy * y_len + y, dx * x_len + x] = int.MaxValue;
                        }

            grid[0, 0] = 0;
            grid2[0, 0] = 0;

            y_len *= dup;
            x_len *= dup;

            for (int y = 0; y < y_len; y++) {
                for (int x = 0; x < x_len; x++) {
                    Console.Write(grid[y, x] + "");
                }
                Console.WriteLine();
            }

            bool dirty = false;

            do {

                dirty = false;

                for (int y = 0; y < y_len; y++) {
                    for (int x = 0; x < x_len; x++) {
                        if (y == 0 && x == 0) continue;
                        var above = y == 0 ? int.MaxValue : grid2[y - 1, x];
                        var left = x == 0 ? int.MaxValue : grid2[y, x - 1];
                        var below = y == (y_len - 1) ? int.MaxValue : grid2[y + 1, x];
                        var right = x == (x_len - 1) ? int.MaxValue : grid2[y, x + 1];
                        var min_value = new[] { above, left, below, right }.Min();
                        if (min_value == int.MaxValue) min_value = 0;
                        var new_value = grid[y, x] + min_value;
                        if (new_value < grid2[y, x]) {
                            grid2[y, x] = new_value;
                            dirty = true;
                        }
                    }
                }

                // for (int y = 0; y < y_len; y++) {
                //     for (int x = 0; x < x_len; x++) {
                //         Console.Write(grid2[y, x] + " ");
                //     }
                //     Console.WriteLine();
                // }

                var risk = grid2[y_len - 1, x_len - 1] - grid[0, 0];
                Console.WriteLine("Lowest risk = " + risk);

            } while (dirty);

            // var risk = grid[y_len - 1, x_len - 1] - grid[0, 0];
            // Console.WriteLine("Lowest risk = " + risk);
            // 752 is too high

        }
    }
}