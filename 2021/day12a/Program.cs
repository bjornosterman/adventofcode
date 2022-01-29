using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        static new Dictionary<string, List<string>> paths;
        static int number_of_paths = 0;

        static void AddPath(string from, string to) {
            if (paths.ContainsKey(from) == false) paths[from] = new List<string>();
            paths[from].Add(to);
        }
        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);
            paths = new Dictionary<string, List<string>>();
            foreach (var line in lines) {
                var split = line.Split('-');
                AddPath(split[0], split[1]);
                AddPath(split[1], split[0]);
            }

            Traverse("start", new List<string>());
            Console.WriteLine("Number of paths: " + number_of_paths);
        }

        static void Traverse(string location, List<string> visited) {
            var new_visited = visited.ToList();
            new_visited.Add(location);
            if (location == "end") {
                number_of_paths++;
                Console.WriteLine(string.Join(",", new_visited));
            }
            else {
                foreach (var adj in paths[location]) {
                    if (new_visited.Contains(adj) == false || adj[0] < 'a') {
                        Traverse(adj, new_visited);
                    }
                }
            }
        }
    }
}