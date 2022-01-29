using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static void Add(Dictionary<string, long> dict, string key, long value) {
            if (dict.ContainsKey(key) == false) dict[key] = 0;
            dict[key] += value;
        }

        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);

            var transforms = new Dictionary<string, string>();

            foreach (var line in lines.Skip(2)) {
                var split = line.Split(" -> ");
                transforms.Add(split[0], split[1]);
            }

            var theLine = lines[0];

            var pairs = new Dictionary<string, long>();
            for (int i = 0; i < theLine.Count() - 1; i++) {
                var pair = theLine.Substring(i, 2);
                Add(pairs, pair, 1);
            }

            for (int s = 0; s < 40; s++) {
                var pairs_new = new Dictionary<string, long>();
                foreach (var kv in pairs) {
                    if (transforms.ContainsKey(kv.Key)) {
                        Add(pairs_new, kv.Key[0] + transforms[kv.Key], kv.Value);
                        Add(pairs_new, transforms[kv.Key] + kv.Key[1], kv.Value);
                    }
                    else {
                        Add(pairs_new, kv.Key, kv.Value);
                    }
                }

                Console.Write("Step " + (s + 1) + ": " + (pairs_new.Sum(x => x.Value) + 1));
                Console.WriteLine();
                pairs = pairs_new;
            }

            var letters = pairs.ToLookup(x => x.Key[0], x => x.Value).ToDictionary(x => x.Key, x => x.Sum());
            foreach (var kv in letters) {
                Console.WriteLine(kv.Key + ": " + kv.Value);
            }
        }
    }
}