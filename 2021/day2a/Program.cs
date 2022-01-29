using System;
using System.IO;
using System.Linq;

namespace day2b {
    class Program {
        static void Main() {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";
            int depth = 0;
            int pos = 0;
            int aim = 0;
            foreach (var line in File.ReadAllLines(filename)) {
                var value = int.Parse(line.Split(' ')[1]);
                switch (line.Split(' ')[0]) {
                    case "forward":
                        pos += value;
                        depth += (aim * value);
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }

            }
            Console.WriteLine($"{pos} + {depth} = {pos * depth}");
        }
    }
}
