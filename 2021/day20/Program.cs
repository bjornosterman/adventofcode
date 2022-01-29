using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            // var is_b = true;
            var filename = is_test ? "sample.txt" : "input.txt";
            var lines = File.ReadAllLines(filename);

            var iea = lines[0].Select(x => x == '#' ? 1 : 0).ToArray();
            var image_lines = lines.Skip(2).ToArray();

            var times = 50;

            var margin = times + 5;

            var width = image_lines[0].Length + margin * 2;
            var height = image_lines.Length + margin * 2;

            var image = new int[height, width];

            for (int y = 0; y < image_lines.Length; y++)
                for (int x = 0; x < image_lines[y].Length; x++)
                    image[margin + y, margin + x] = image_lines[y][x] == '#' ? 1 : 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++)
                    Console.Write(image[y, x] == 1 ? '#' : '.');
                Console.WriteLine();
            }
            Console.WriteLine();


            for (int i = 0; i < times; i++) {

                var new_image = new int[height, width];

                for (int y = 1; y < height - 1; y++)
                    for (int x = 1; x < width - 1; x++) {
                        var number =
                        (image[y - 1, x - 1] << 8) +
                        (image[y - 1, x] << 7) +
                        (image[y - 1, x + 1] << 6) +
                        (image[y, x - 1] << 5) +
                        (image[y, x] << 4) +
                        (image[y, x + 1] << 3) +
                        (image[y + 1, x - 1] << 2) +
                        (image[y + 1, x] << 1) +
                        (image[y + 1, x + 1] << 0);
                        new_image[y, x] = iea[number];
                    }

                for (int y = 0; y < height; y++) {
                    new_image[y, 0] = new_image[1, 1];
                    new_image[y, width - 1] = new_image[1, 1];
                }

                for (int x = 0; x < width; x++) {
                    new_image[0, x] = new_image[1, 1];
                    new_image[height - 1, x] = new_image[1, 1];
                }

                image = new_image;

                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++)
                        Console.Write(image[y, x] == 1 ? '#' : '.');
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            int lits = 0;
            // for (int y = margin; y < height - margin; y++) {
            //     for (int x = margin; x < width - margin; x++)
            //         lits += image[y, x];
            // }

            // for (int y = 0; y < height; y++) {
            //     for (int x = 0; x < width; x++)
            //         lits += image[y, x];
            // }

            for (int y = 4; y < height - 4; y++) {
                for (int x = 4; x < width - 4; x++)
                    lits += image[y, x];
            }

            // 5291 är för högt... sen fick jag 5527..5275
            // Vid andra: 17338 är för högt.. 17599 är ännu högre, 17490 är också högt
            Console.WriteLine(lits);
        }
    }
}