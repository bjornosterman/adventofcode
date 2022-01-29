using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        new static readonly Dictionary<string, int> costs = new();
        static new Dictionary<string, int> toCosts = new();

        const int LEN = 7 + 4 * 4;

        static void Main(string[] args) {
            var is_test = false;

            var intial = is_test ? ".......BCBDDCBADBACADCA" : ".......BCCBDCBADBACDDAA";

            var min = Walk(0, intial);
            Console.WriteLine("Min: " + min);

        }

        static int globalMinCost = int.MaxValue;

        static List<string> stack = new();
        static List<int> costStack = new();

        static int Walk(int cost, string layout) {
            try {
                stack.Add(layout);
                costStack.Add(cost);

                if (
                layout[7] == 'A' && layout[11] == 'A' && layout[15] == 'A' && layout[19] == 'A'
                && layout[8] == 'B' && layout[12] == 'B' && layout[16] == 'B' && layout[20] == 'B'
                && layout[9] == 'C' && layout[13] == 'C' && layout[17] == 'C' && layout[21] == 'C'
                && layout[10] == 'D' && layout[14] == 'D' && layout[18] == 'D' && layout[22] == 'D'
                ) {
                    if (cost < globalMinCost) {
                        Console.WriteLine("New min found: " + cost);
                        foreach (var (a_step, a_cost) in stack.Zip(costStack, (a, b) => (a, b))) {
                            PrintLayout(a_step);
                            Console.WriteLine(a_cost);
                        }
                        globalMinCost = cost;
                    }
                    return cost;
                }

                if (toCosts.TryGetValue(layout, out int toCost)) {
                    if (toCost <= cost) return int.MaxValue;
                }
                toCosts[layout] = cost;

                var minCost = int.MaxValue;

                var new_chars = new char[LEN];
                layout.CopyTo(0, new_chars, 0, LEN);

                for (int i = 0; i < LEN; i++) {
                    var c = layout[i];
                    if (c == '.') continue;
                    new_chars[i] = '.';
                    foreach (var (dest, steps) in GetDests(layout, i)) {
                        var step_cost = steps * (c switch { 'A' => 1, 'B' => 10, 'C' => 100, 'D' => 1000 });
                        if (step_cost > minCost) continue;
                        new_chars[dest] = c;
                        var new_layout = new string(new_chars);
                        var walk_cost = Walk(cost + step_cost, new_layout);
                        if (walk_cost < minCost) minCost = walk_cost;
                        new_chars[dest] = '.';
                    }
                    new_chars[i] = c;
                }

                costs[layout] = minCost;
                return minCost;
            }
            finally {
                stack.RemoveAt(stack.Count - 1);
                costStack.RemoveAt(costStack.Count - 1);
            }
        }

        static (int, int) GetDrop(string layout, int from) {
            char c = layout[from];
            bool e(int x) => layout[x] == '.';
            bool s(int x) => layout[x] == c;

            var d0 = c - 'A' + 7;
            var d1 = d0 + 4;
            var d2 = d1 + 4;
            var d3 = d2 + 4;

            if (e(d3) && e(d2) && e(d1) && e(d0)) return (d3, 4);
            if (s(d3) && e(d2) && e(d1) && e(d0)) return (d2, 3);
            if (s(d3) && s(d2) && e(d1) && e(d0)) return (d1, 2);
            if (s(d3) && s(d2) && s(d1) && e(d0)) return (d0, 1);
            return (-1, -1);
        }

        static int GetLengthToInlet(string layout, int from, char c) {
            // char c = layout[from];
            bool e(int x) => layout[x] == '.';
            bool s(int x) => layout[x] == c;

            var d0 = c - 'A' + 7;
            var d1 = d0 + 4;
            var d2 = d1 + 4;
            var d3 = d2 + 4;

            switch (from) {
                case 0:
                    if (!e(1)) return -1;
                    return c switch {
                        'A' => 2,
                        'B' => e(2) ? 4 : -1,
                        'C' => e(2) && e(3) ? 6 : -1,
                        'D' => e(2) && e(3) && e(4) ? 8 : -1
                    };
                case 1:
                    return c switch {
                        'A' => 1,
                        'B' => e(2) ? 3 : -1,
                        'C' => e(2) && e(3) ? 5 : -1,
                        'D' => e(2) && e(3) && e(4) ? 7 : -1
                    };
                case 2:
                    return c switch {
                        'A' => 1,
                        'B' => 1,
                        'C' => e(3) ? 3 : -1,
                        'D' => e(3) && e(4) ? 5 : -1
                    };
                case 3:
                    return c switch {
                        'A' => e(2) ? 3 : -1,
                        'B' => 1,
                        'C' => 1,
                        'D' => e(4) ? 3 : -1
                    };
                case 4:
                    return c switch {
                        'A' => e(3) && e(2) ? 5 : -1,
                        'B' => e(3) ? 3 : -1,
                        'C' => 1,
                        'D' => 1
                    };
                case 5:
                    return c switch {
                        'A' => e(4) && e(3) && e(2) ? 7 : -1,
                        'B' => e(4) && e(3) ? 5 : -1,
                        'C' => e(4) ? 3 : -1,
                        'D' => 1
                    };
                case 6:
                    if (!e(5)) return -1;
                    return c switch {
                        'A' => e(4) && e(3) && e(2) ? 8 : -1,
                        'B' => e(4) && e(3) ? 6 : -1,
                        'C' => e(4) ? 4 : -1,
                        'D' => 2
                    };
            }
            throw new Exception("Not possible");
        }

        static int GetUndrop(string layout, int from) {
            char c = layout[from];
            bool e(int x) => layout[x] == '.';
            bool s(int x) => layout[x] == c;

            var d0 = c - 'A' + 7;
            var d1 = d0 + 4;
            var d2 = d1 + 4;
            var d3 = d2 + 4;

            if (from == d3) return -1;
            if (from == d2 && s(d3)) return -1;
            if (from == d1 && s(d2) && s(d3)) return -1;
            if (from == d0 && s(d2) && s(d2) && s(d3)) return -1;

            if (from < 11) return 1;
            if (!e(from - 4)) return -1;
            return ((from - 7) / 4) + 1;
        }

        static IEnumerable<(int, int)> GetDests(string layout, int from) {
            char c = layout[from];
            bool e(int x) => layout[x] == '.';
            bool s(int x) => layout[x] == c;

            var drop = 0;
            var dropDest = 0;

            if (from < 7) {
                (dropDest, drop) = GetDrop(layout, from);
                if (drop == -1) yield break;
                var length_to_inlet = GetLengthToInlet(layout, from, layout[from]);
                if (length_to_inlet == -1) yield break;
                // var dropDest = 7 + ((drop - 1) * 4);
                yield return (dropDest, length_to_inlet + drop);
                yield break;
            }

            var undrop = GetUndrop(layout, from);
            if (undrop == -1) yield break;
            var drop_letter = (char)('A' + ((from - 7) % 4));
            for (int i = 0; i < 7; i++) {
                if (e(i)) {
                    var length_to_inlet = GetLengthToInlet(layout, i, drop_letter);
                    if (length_to_inlet != -1) yield return (i, undrop + length_to_inlet);
                }
            }
        }
        static void PrintLayout(string layout) {
            // #############
            // #...........#
            // ###A#B#C#D###
            //   #A#B#C#D#
            //   #A#B#C#D#
            //   #A#B#C#D#
            //   #########

            Console.WriteLine("\\ #############");
            Console.WriteLine($"/ #{layout[0]}{layout[1]}.{layout[2]}.{layout[3]}.{layout[4]}.{layout[5]}{layout[6]}#");
            Console.WriteLine($"\\ ###{layout[7]}#{layout[8]}#{layout[9]}#{layout[10]}###");
            Console.WriteLine($"| ###{layout[11]}#{layout[12]}#{layout[13]}#{layout[14]}###");
            Console.WriteLine($"/ ###{layout[15]}#{layout[16]}#{layout[17]}#{layout[18]}###");
            Console.WriteLine($"- ###{layout[19]}#{layout[20]}#{layout[21]}#{layout[22]}###");
            Console.WriteLine("\\  #########");
            Console.WriteLine();
        }
    }
}
