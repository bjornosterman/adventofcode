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
            var values = lines[0].Split(",").Select(int.Parse).ToList();
            var min = values.Min();
            var max = values.Max();

            var optimal = 0;
            var fuel = 999999999;

            for (var i = min; i <= max; i++) {
                var test_fuel = values.Select(x => Enumerable.Range(1, Math.Abs(x - i)).Sum()).Sum();
                if (test_fuel < fuel) {
                    fuel = test_fuel;
                    optimal = i;
                }
            }

            Console.WriteLine("Minimum Fuel = " + fuel);

        }
    }
}
