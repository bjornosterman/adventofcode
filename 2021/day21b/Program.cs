using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;

            int dice = 0;
            int pos_1 = (is_test ? 4 : 10) - 1;
            int pos_2 = (is_test ? 8 : 3) - 1;

            var wins = Play(1, pos_1, pos_2, 0, 0);
            Console.WriteLine(wins);
        }

        static Dictionary<string, (long, long)> Games = new();

        static (long, long) Play(int player, int pos1, int pos2, long points1, long points2) {
            if (points1 >= 21) return (1, 0);
            if (points2 >= 21) return (0, 1);
            var key = $"{player}-{pos1}-{pos2}-{points1}-{points2}";
            if (Games.ContainsKey(key)) return Games[key];
            long p1_wins = 0, p2_wins = 0;
            for (int a = 1; a <= 3; a++)
                for (int b = 1; b <= 3; b++)
                    for (int c = 1; c <= 3; c++) {
                        if (player == 1) {
                            var (p1_inner_wins, p2_inner_wins) = Play(2, (pos1 + a + b + c) % 10, pos2, (points1 + (pos1 + a + b + c) % 10) + 1, points2);
                            p1_wins += p1_inner_wins;
                            p2_wins += p2_inner_wins;
                        }
                        else {
                            var (p1_inner_wins, p2_inner_wins) = Play(1, pos1, (pos2 + a + b + c) % 10, points1, (points2 + (pos2 + a + b + c) % 10) + 1);
                            p1_wins += p1_inner_wins;
                            p2_wins += p2_inner_wins;
                        }
                    }

            Games[key] = (p1_wins, p2_wins);
            return (p1_wins, p2_wins);
        }
    }
}