using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var is_b = true;
            var filename = is_test ? "sample.txt" : "input.txt";

            var tx_min = is_test ? 20 : 192;
            var tx_max = is_test ? 30 : 251;
            var ty_min = is_test ? -10 : -89;
            var ty_max = is_test ? -5 : -59;

            // var x = 0;
            // var y = 0;

            // var dx = 6;
            // var dy = 9;

            var best_y = 0;
            var best_dx = 0;
            var best_dy = 0;
            var number_of_ivs = 0;

            for (int ix = 0; ix <= tx_max; ix++) {
                for (int iy = ty_min; iy < Math.Abs(ty_min * 3); iy++) {
                    var x = 0;
                    var y = 0;
                    var dx = ix;
                    var dy = iy;
                    var y_max = 0;
                    while (true) {
                        x += dx;
                        dx = dx == 0 ? 0 : dx - 1;
                        y += dy;
                        if (y > y_max) y_max = y;
                        dy--;
                        if (x >= tx_min && x <= tx_max && y >= ty_min && y <= ty_max) {
                            if (y_max > best_y) {
                                best_y = y_max;
                                best_dx = dx;
                                best_dy = dy;
                                Console.WriteLine($"{ix}, {iy}: {y_max}");
                            }
                            number_of_ivs++;
                            // Console.WriteLine("HIT!");
                            // Console.WriteLine("Max hight: " + y_max);
                            break;
                        }
                        if (x > tx_max || y < ty_min) {
                            // Console.WriteLine("MISS!");
                            break;
                        }

                    }


                }
            }
            Console.WriteLine("Number of ivs: " + number_of_ivs);

        }
    }
}