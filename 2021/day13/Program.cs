using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static new List<(int, int)> dots = new List<(int, int)>();
        static int max_y = 1000;
        static int max_x = 1000;
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);

            foreach (var line in lines) {
                if (line == "") break;
                var split = line.Split(',');
                dots.Add((int.Parse(split[1]), int.Parse(split[0])));
            }

            if (is_test) {
                HorizontalFold(7);
                VerticalFold(5);
            }
            else {

                // fold along x=655
                VerticalFold(655);
                // fold along y=447
                HorizontalFold(447);
                // fold along x=327
                VerticalFold(327);
                // fold along y=223
                HorizontalFold(223);
                // fold along x=163
                VerticalFold(163);
                // fold along y=111
                HorizontalFold(111);
                // fold along x=81
                VerticalFold(81);
                // fold along y=55
                HorizontalFold(55);
                // fold along x=40
                VerticalFold(40);
                // fold along y=27
                HorizontalFold(27);
                // fold along y=13
                HorizontalFold(13);
                // fold along y=6
                HorizontalFold(6);
            }

            var grid = new int[max_y, max_x];
            foreach (var dot in dots) {
                grid[dot.Item1, dot.Item2] = 1;
            }

            var number_of_dots = 0;

            for (int y = 0; y < max_y; y++) {
                for (int x = 0; x < max_x; x++) {
                    number_of_dots += grid[y, x] == 0 ? 0 : 1;
                    Console.Write(grid[y, x] == 0 ? "." : "#");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Number of dots: " + number_of_dots);
        }

        static void HorizontalFold(int v) {
            max_y = v;
            for (int i = 0; i < dots.Count; i++) {
                var dot = dots[i];
                if (dot.Item1 > v) dot.Item1 = v - (dot.Item1 - v);
                dots[i] = dot;
            }
        }

        static void VerticalFold(int v) {
            max_x = v;
            for (int i = 0; i < dots.Count; i++) {
                var dot = dots[i];
                if (dot.Item2 > v) dot.Item2 = v - (dot.Item2 - v);
                dots[i] = dot;
            }
        }
    }
}