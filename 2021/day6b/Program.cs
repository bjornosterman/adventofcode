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
            var fish_values = lines[0].Split(",").Select(int.Parse).ToList();
            var fishes = new long[9];
            foreach (var v in fish_values) {
                fishes[v]++;
            }

            var number_of_days = 256; // is_test ? 18 : 80;

            for (var d = 0; d < number_of_days; d++) {
                var new_fishes = new long[9];
                Array.Copy(fishes, 1, new_fishes, 0, 8);
                new_fishes[8] = fishes[0];
                new_fishes[6] += fishes[0];
                Array.Copy(new_fishes, fishes, 9);
                Console.WriteLine("After Day " + (d + 1) + " there are " + fishes.Sum());
            }

            Console.WriteLine("Number of fishes: " + fishes.Length);
        }
    }
}
