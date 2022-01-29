using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = true;

            int dice = 0;
            int pos_1 = (is_test ? 4 : 10) - 1;
            int pos_2 = (is_test ? 8 : 3) - 1;
            int points_1 = 0;
            int points_2 = 0;

            int rolls = 0;

            Func<int> roll = () => {
                var r = dice + 1;
                Console.Write(r + " ");
                dice = dice + 1 % 10;
                rolls++;
                return r;
            };

            while (true) {
                Console.Write("P1:  ");
                pos_1 = (pos_1 + roll() + roll() + roll()) % 10;
                Console.Write((pos_1 + 1) + " ");
                points_1 += (pos_1 + 1);
                Console.WriteLine(points_1);
                if (points_1 >= 1000) {
                    Console.WriteLine("Player 1 won after " + rolls + ", looser got " + points_2 + " points => " + (rolls * points_2));
                    break;
                }


                Console.Write("P2:  ");
                pos_2 = (pos_2 + roll() + roll() + roll()) % 10;
                Console.Write((pos_2 + 1) + " ");
                points_2 += (pos_2 + 1);
                Console.WriteLine(points_2);
                if (points_2 >= 1000) {
                    Console.WriteLine("Player 2 won after " + rolls + ", looser got " + points_1 + " points => " + (rolls * points_1));
                    break;
                }
            }
        }
    }
}