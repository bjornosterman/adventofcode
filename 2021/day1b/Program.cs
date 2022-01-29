using System;
using System.IO;
using System.Linq;

namespace day1b {
    class Program {
        static void Main() {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";
            var numbers = File.ReadAllLines(filename).Select(x => int.Parse(x)).ToList();
            var count = 0;
            for (int i = 3; i < numbers.Count; i++) {
                if (numbers[i - 3] < numbers[i]) count++;
            }
            Console.WriteLine("Count: " + count);
        }
    }
}
