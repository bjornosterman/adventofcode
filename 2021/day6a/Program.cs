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
            var fishes = lines[0].Split(",").Select(int.Parse).ToList();

            var number_of_days = 80; // is_test ? 18 : 80;

            for (var d = 0; d < number_of_days; d++) {
                var number_of_fishes = fishes.Count;
                for (var i = 0; i < number_of_fishes; i++) {
                    if ((fishes[i]--) == 0) {
                        fishes[i] = 6;
                        fishes.Add(8);
                    }
                }
                Console.WriteLine("After Day " + (d + 1) + " there are " + fishes.Count);
            }

            Console.WriteLine("Number of fishes: " + fishes.Count);
        }
    }
}
