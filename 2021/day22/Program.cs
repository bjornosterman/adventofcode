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

            var spaces = new List<Space>();

            foreach (var line in lines) {
                var split1 = line.Split(' ');
                var split2 = split1[1].Split(',');
                var split_x = split2[0][2..].Split("..").Select(int.Parse).ToArray();
                var split_y = split2[1][2..].Split("..").Select(int.Parse).ToArray();
                var split_z = split2[2][2..].Split("..").Select(int.Parse).ToArray();

                spaces.Add(new Space() {
                    On = split1[0] == "on",
                    FromX = Math.Min(split_x[0], split_x[1]),
                    ToX = Math.Max(split_x[0], split_x[1]),
                    FromY = Math.Min(split_y[0], split_y[1]),
                    ToY = Math.Max(split_y[0], split_y[1]),
                    FromZ = Math.Min(split_z[0], split_z[1]),
                    ToZ = Math.Max(split_z[0], split_z[1])

                });
            }

            spaces.Reverse();

            var min = -50;
            var max = 50;

            var ons = 0;

            for (int x = min; x <= max; x++)
                for (int y = min; y <= max; y++)
                    for (int z = min; z <= max; z++) {
                        var match_space = spaces.FirstOrDefault(s => s.IsIn(x, y, z));
                        if (match_space != null && match_space.On) ons++;
                    }

            Console.WriteLine("Lit ones:" + ons);
        }

        public class Space {
            public bool On;
            public int FromX;
            public int ToX;
            public int FromY;
            public int ToY;
            public int FromZ;
            public int ToZ;

            public bool IsIn(int x, int y, int z) {
                return x >= FromX && x <= ToX && y >= FromY && y <= ToY && z >= FromZ && z <= ToZ;
            }

        }
    }
}