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
            var sum = 0;
            foreach (var line in lines) {
                var r = line.Split(" | ")[0].Split(' ').Select(x => x.OrderBy(y => y).ToArray()).ToArray();
                var d1 = r.Single(x => x.Length == 2);
                var d3 = r.Single(x => x.Length == 5 && x.Contains(d1[0]) && x.Contains(d1[1]));
                var d4 = r.Single(x => x.Length == 4);
                var d6 = r.Single(x => x.Length == 6 && (!x.Contains(d1[0]) || !x.Contains(d1[1])));
                var d7 = r.Single(x => x.Length == 3);
                var d8 = r.Single(x => x.Length == 7);
                var d5 = r.Single(x => x.Length == 5 && x.Except(d6).Any() == false);
                var d2 = r.Single(x => x.Length == 5 && x != d3 && x != d5);
                var d9 = r.Single(x => x.Length == 6 && x.Except(d5).Count() == 1 && x != d6);
                var d0 = r.Single(x => x.Length == 6 && x.Except(d5).Count() == 2);
                var d_to_n = new Dictionary<string, int> {
                    { new string(d1),1 },
                    { new string( d2),2 } ,
                    { new string( d3),3 } ,
                    { new string( d4),4 } ,
                    { new string( d5),5 } ,
                    { new string( d6),6 } ,
                    { new string( d7),7 } ,
                    { new string( d8),8 } ,
                    { new string( d9),9 } ,
                    { new string( d0),0 } };

                var digits = "";

                foreach (var digit in line.Split(" | ")[1].Split(' ').Select(x => new string(x.OrderBy(y => y).ToArray()))) {
                    digits += d_to_n[digit];
                }

                Console.WriteLine(digits);
                sum += int.Parse(digits);
            }
            Console.WriteLine("Sum: " + sum);
        }
    }
}