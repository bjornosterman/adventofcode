using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {
        new static readonly Dictionary<string, int> costs = new();
        static new Dictionary<string, int> toCosts = new();

        static void Main(string[] args) {
            var is_test = true;

            var intial = is_test ? ".......BCBDADCA" : ".......BCCBDDAA";

            var min = Walk(0, intial, false, false, false, false);
            Console.WriteLine("Min: " + min);

        }

        static int globalMinCost = int.MaxValue;
        static HashSet<string> seenLayouts = new();

        static List<string> stack = new();
        static List<int> costStack = new();

        static int Walk(int cost, string layout, bool block11, bool block12, bool block13, bool block14) {
            if (
                (layout == "X......BCBDADCA" && cost == 0) ||
                (layout == "X.B....BC.DADCA" && cost < 40) ||
                (layout == "X.BC...B..DADCA" && cost < 240) ||
                (layout == "X.B....B.CDADCA" && cost < 440) ||
                (layout == "X.BD...B.CDA.CA" && cost < 3440) ||
                (layout == "X..D...B.CDABCA" && cost < 3470) ||
                (layout == "X.BD.....CDABCA" && cost < 3490) ||
                (layout == "...D....BCDABCA" && cost < 3510) ||
                (layout == "...DD...BC.ABCA" && cost < 5510) ||
                (layout == "...DDA..BC.ABC." && cost < 5513) ||
                (layout == "...D.A..BC.ABCD" && cost < 8513) ||
                (layout == ".....A..BCDABCD" && cost < 12513) ||
                (layout == ".......ABCDABCD" && cost < 12521)
               ) {
                var gurka = 1;
            }

            try {
                stack.Add(layout);
                costStack.Add(cost);

                if (layout.StartsWith(".......")
                && layout[7] == layout[11] && layout[11] == 'A'
                && layout[8] == layout[12] && layout[12] == 'B'
                && layout[9] == layout[13] && layout[13] == 'C'
                && layout[10] == layout[14] && layout[14] == 'D'
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

                // if (costs.ContainsKey(layout)) {
                //     return costs[layout];
                // }

                // if (seenLayouts.Add(layout) == false) {
                //     return int.MaxValue;
                // }

                var minCost = int.MaxValue;

                // PrintLayout(layout);

                var new_chars = new char[15];
                layout.CopyTo(0, new_chars, 0, 15);

                for (int i = 0; i < 15; i++) {
                    if (block11 && i == 11) continue;
                    if (block12 && i == 12) continue;
                    if (block13 && i == 13) continue;
                    if (block14 && i == 14) continue;
                    var c = layout[i];
                    if (c == '.') continue;
                    new_chars[i] = '.';
                    foreach (var (dest, steps) in GetDests(layout, i)) {
                        // if (layout == "...DD...BC.ABCA" && cost == 5510) {
                        //     var hej = 0;
                        // }
                        var step_cost = steps * (c switch { 'A' => 1, 'B' => 10, 'C' => 100, 'D' => 1000 });
                        if (step_cost > minCost) continue;
                        new_chars[dest] = c;
                        var new_layout = new string(new_chars);
                        var walk_cost = Walk(cost + step_cost, new_layout, block11 || dest == 11, block12 || dest == 12, block13 || dest == 13, block14 || dest == 14);
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


        static IEnumerable<(int, int)> GetDests(string layout, int from) {
            char c = layout[from];
            bool e(int x) => layout[x] == '.';
            bool s(int x) => layout[x] == c;

            switch (from) {
                case 0:
                    if (e(1) && e(7) && s(11)) yield return (7, 3);
                    if (e(1) && e(7) && e(11)) yield return (11, 4);
                    if (e(1) && e(2) && e(8) && s(12)) yield return (8, 5);
                    if (e(1) && e(2) && e(8) && e(12)) yield return (12, 6);
                    if (e(1) && e(2) && e(3) && e(9) && s(13)) yield return (9, 7);
                    if (e(1) && e(2) && e(3) && e(9) && e(13)) yield return (13, 8);
                    if (e(1) && e(2) && e(3) && e(4) && e(10) && s(14)) yield return (10, 9);
                    if (e(1) && e(2) && e(3) && e(4) && e(10) && e(14)) yield return (14, 10);
                    break;
                case 1:
                    if (e(7) && s(11)) yield return (7, 2);
                    if (e(7) && e(11)) yield return (11, 3);
                    if (e(2) && e(8) && s(12)) yield return (8, 4);
                    if (e(2) && e(8) && e(12)) yield return (12, 5);
                    if (e(2) && e(3) && e(9) && s(13)) yield return (9, 6);
                    if (e(2) && e(3) && e(9) && e(13)) yield return (13, 7);
                    if (e(2) && e(3) && e(4) && e(10) && s(14)) yield return (10, 8);
                    if (e(2) && e(3) && e(4) && e(10) && e(14)) yield return (14, 9);
                    break;
                case 2:
                    if (e(7) && s(11)) yield return (7, 2);
                    if (e(7) && e(11)) yield return (11, 3);
                    if (e(8) && s(12)) yield return (8, 2);
                    if (e(8) && e(12)) yield return (12, 3);
                    if (e(3) && e(9) && s(13)) yield return (9, 4);
                    if (e(3) && e(9) && e(13)) yield return (13, 5);
                    if (e(3) && e(4) && e(10) && s(14)) yield return (10, 6);
                    if (e(3) && e(4) && e(10) && e(14)) yield return (14, 7);
                    break;
                case 3:
                    if (e(2) && e(7) && s(11)) yield return (7, 4);
                    if (e(2) && e(7) && e(11)) yield return (11, 5);
                    if (e(8) && s(12)) yield return (8, 2);
                    if (e(8) && e(12)) yield return (12, 3);
                    if (e(9) && s(13)) yield return (9, 2);
                    if (e(9) && e(13)) yield return (13, 3);
                    if (e(3) && e(10) && s(13)) yield return (10, 4);
                    if (e(3) && e(10) && e(13)) yield return (14, 5);
                    break;
                case 4:
                    if (e(10) && s(14)) yield return (10, 2);
                    if (e(10) && e(14)) yield return (14, 3);
                    if (e(9) && s(13)) yield return (9, 2);
                    if (e(9) && e(13)) yield return (13, 3);
                    if (e(3) && e(8) && s(12)) yield return (8, 4);
                    if (e(3) && e(8) && e(12)) yield return (12, 5);
                    if (e(3) && e(2) && e(7) && s(11)) yield return (7, 6);
                    if (e(3) && e(2) && e(7) && e(11)) yield return (11, 7);
                    break;
                case 5:
                    if (e(10) && s(14)) yield return (10, 2);
                    if (e(10) && e(14)) yield return (14, 3);
                    if (e(4) && e(9) && s(13)) yield return (9, 4);
                    if (e(4) && e(9) && e(13)) yield return (13, 5);
                    if (e(4) && e(3) && e(8) && s(12)) yield return (8, 6);
                    if (e(4) && e(3) && e(8) && e(12)) yield return (12, 7);
                    if (e(4) && e(3) && e(2) && e(7) && s(11)) yield return (7, 8);
                    if (e(4) && e(3) && e(2) && e(7) && e(11)) yield return (11, 9);
                    break;
                case 6:
                    if (e(5) && e(10) && s(14)) yield return (10, 3);
                    if (e(5) && e(10) && e(14)) yield return (14, 4);
                    if (e(5) && e(4) && e(9) && s(13)) yield return (9, 5);
                    if (e(5) && e(4) && e(9) && e(13)) yield return (13, 6);
                    if (e(5) && e(4) && e(3) && e(8) && s(12)) yield return (8, 7);
                    if (e(5) && e(4) && e(3) && e(8) && e(12)) yield return (12, 8);
                    if (e(5) && e(4) && e(3) && e(2) && e(7) && s(11)) yield return (7, 9);
                    if (e(5) && e(4) && e(3) && e(2) && e(7) && e(11)) yield return (11, 10);
                    break;
                case 7:
                    if (s(11)) yield break;
                    if (e(1) && e(0)) yield return (0, 3);
                    if (e(1)) yield return (1, 2);
                    if (e(2)) yield return (2, 2);
                    if (e(2) && e(3)) yield return (3, 4);
                    if (e(2) && e(3) && e(4)) yield return (4, 6);
                    if (e(2) && e(3) && e(4) && e(5)) yield return (5, 8);
                    if (e(2) && e(3) && e(4) && e(5) && e(6)) yield return (6, 9);
                    break;
                case 8:
                    if (s(12)) yield break;
                    if (e(2) && e(1) && e(0)) yield return (0, 5);
                    if (e(2) && e(1)) yield return (1, 4);
                    if (e(2)) yield return (2, 2);
                    if (e(3)) yield return (3, 2);
                    if (e(3) && e(4)) yield return (4, 4);
                    if (e(3) && e(4) && e(5)) yield return (5, 6);
                    if (e(3) && e(4) && e(5) && e(6)) yield return (6, 7);
                    break;
                case 9:
                    if (s(13)) yield break;
                    if (e(3) && e(2) && e(1) && e(0)) yield return (0, 7);
                    if (e(3) && e(2) && e(1)) yield return (1, 6);
                    if (e(3) && e(2)) yield return (2, 4);
                    if (e(3)) yield return (3, 2);
                    if (e(4)) yield return (4, 2);
                    if (e(4) && e(5)) yield return (5, 4);
                    if (e(4) && e(5) && e(6)) yield return (6, 5);
                    break;
                case 10:
                    if (s(14)) yield break;
                    if (e(4) && e(3) && e(2) && e(1) && e(0)) yield return (0, 9);
                    if (e(4) && e(3) && e(2) && e(1)) yield return (1, 8);
                    if (e(4) && e(3) && e(2)) yield return (2, 6);
                    if (e(4) && e(3)) yield return (3, 4);
                    if (e(4)) yield return (4, 2);
                    if (e(5)) yield return (5, 2);
                    if (e(5) && e(6)) yield return (6, 3);
                    break;
                case 11:
                    if (s(7)) yield break;
                    if (e(7) && e(1) && e(0)) yield return (0, 4);
                    if (e(7) && e(1)) yield return (1, 3);
                    if (e(7) && e(2)) yield return (2, 3);
                    if (e(7) && e(2) && e(3)) yield return (3, 5);
                    if (e(7) && e(2) && e(3) && e(4)) yield return (4, 7);
                    if (e(7) && e(2) && e(3) && e(4) && e(5)) yield return (5, 9);
                    if (e(7) && e(2) && e(3) && e(4) && e(5) && e(6)) yield return (6, 10);
                    break;
                case 12:
                    if (s(8)) yield break;
                    if (e(8) && e(2) && e(1) && e(0)) yield return (0, 6);
                    if (e(8) && e(2) && e(1)) yield return (1, 5);
                    if (e(8) && e(2)) yield return (2, 3);
                    if (e(8) && e(3)) yield return (3, 3);
                    if (e(8) && e(3) && e(4)) yield return (4, 5);
                    if (e(8) && e(3) && e(4) && e(5)) yield return (5, 7);
                    if (e(8) && e(3) && e(4) && e(5) && e(6)) yield return (6, 8);
                    break;
                case 13:
                    if (s(9)) yield break;
                    if (e(9) && e(3) && e(2) && e(1) && e(0)) yield return (0, 8);
                    if (e(9) && e(3) && e(2) && e(1)) yield return (1, 7);
                    if (e(9) && e(3) && e(2)) yield return (2, 5);
                    if (e(9) && e(3)) yield return (3, 3);
                    if (e(9) && e(4)) yield return (4, 3);
                    if (e(9) && e(4) && e(5)) yield return (5, 5);
                    if (e(9) && e(4) && e(5) && e(6)) yield return (6, 6);
                    break;
                case 14:
                    if (s(10)) yield break;
                    if (e(10) && e(4) && e(3) && e(2) && e(1) && e(0)) yield return (0, 10);
                    if (e(10) && e(4) && e(3) && e(2) && e(1)) yield return (1, 9);
                    if (e(10) && e(4) && e(3) && e(2)) yield return (2, 7);
                    if (e(10) && e(4) && e(3)) yield return (3, 5);
                    if (e(10) && e(4)) yield return (4, 3);
                    if (e(10) && e(5)) yield return (5, 3);
                    if (e(10) && e(5) && e(6)) yield return (6, 4);
                    break;
            }
        }
        static void PrintLayout(string layout) {
            // #############
            // #...........#
            // ###A#B#C#D###
            //   #A#B#C#D#
            //   #########

            Console.WriteLine("\\ #############");
            Console.WriteLine($"/ #{layout[0]}{layout[1]}.{layout[2]}.{layout[3]}.{layout[4]}.{layout[5]}{layout[6]}#");
            Console.WriteLine($"\\ ###{layout[7]}#{layout[8]}#{layout[9]}#{layout[10]}###");
            Console.WriteLine($"/ ###{layout[11]}#{layout[12]}#{layout[13]}#{layout[14]}###");
            Console.WriteLine("\\  #########");
            Console.WriteLine();
        }
    }
}
