using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";
            var lines = File.ReadAllLines(filename);

            var reboot_steps = new List<Space>();

            var step_i = 1;

            foreach (var line in lines) {
                var split1 = line.Split(' ');
                var split2 = split1[1].Split(',');
                var split_x = split2[0][2..].Split("..").Select(long.Parse).ToArray();
                var split_y = split2[1][2..].Split("..").Select(long.Parse).ToArray();
                var split_z = split2[2][2..].Split("..").Select(long.Parse).ToArray();

                var reboot_step = new Space() {
                    On = split1[0] == "on",
                    FromX = Math.Min(split_x[0], split_x[1]),
                    ToX = Math.Max(split_x[0], split_x[1]),
                    FromY = Math.Min(split_y[0], split_y[1]),
                    ToY = Math.Max(split_y[0], split_y[1]),
                    FromZ = Math.Min(split_z[0], split_z[1]),
                    ToZ = Math.Max(split_z[0], split_z[1])
                };
                reboot_steps.Add(reboot_step);
                Console.WriteLine($"Line {step_i++}: Size: " + reboot_step.Size);
            }

            var reactor = new List<Space>{new Space() {
                On = false,
                FromX = reboot_steps.Select(x => x.FromX).Min(),
                ToX = reboot_steps.Select(x => x.ToX).Max(),
                FromY = reboot_steps.Select(x => x.FromY).Min(),
                ToY = reboot_steps.Select(x => x.ToY).Max(),
                FromZ = reboot_steps.Select(x => x.FromZ).Min(),
                ToZ = reboot_steps.Select(x => x.ToZ).Max(),
            }};

            step_i = 1;

            foreach (var step in reboot_steps) {
                Console.Write($"Step {step_i++}: ");

                reactor = reactor.SelectMany(x => x.SplitX(step.FromX, step.FromY, step.ToY, step.FromZ, step.ToZ))
                    .SelectMany(x => x.SplitX(step.ToX + 1, step.FromY, step.ToY, step.FromZ, step.ToZ))
                    .SelectMany(x => x.SplitY(step.FromY, step.FromX, step.ToX, step.FromZ, step.ToZ))
                    .SelectMany(x => x.SplitY(step.ToY + 1, step.FromX, step.ToX, step.FromZ, step.ToZ))
                    .SelectMany(x => x.SplitZ(step.FromZ, step.FromX, step.ToX, step.FromY, step.ToY))
                    .SelectMany(x => x.SplitZ(step.ToZ + 1, step.FromX, step.ToX, step.FromY, step.ToY))
                    .ToList();

                Console.WriteLine($": Pieces: " + reactor.Count);
            }

            step_i = 0;

            foreach (var step in reboot_steps) {
                Console.WriteLine("Processing step : " + step_i++);
                foreach (var piece in reactor) {
                    if (step.IsIn(piece.FromX, piece.FromY, piece.FromZ)) {
                        piece.On = step.On;
                    }
                }
            }

            long ons = reactor.Where(x => x.On).Sum(x => x.Size);


            Console.WriteLine("Lit ones:" + ons);
        }

        public class Space {
            public bool On;
            public long FromX;
            public long ToX;
            public long FromY;
            public long ToY;
            public long FromZ;
            public long ToZ;

            public bool IsIn(long x, long y, long z) {
                return x >= FromX && x <= ToX && y >= FromY && y <= ToY && z >= FromZ && z <= ToZ;
            }

            public Space Clone() {
                return new Space {
                    On = On,
                    FromX = FromX,
                    ToX = ToX,
                    FromY = FromY,
                    ToY = ToY,
                    FromZ = FromZ,
                    ToZ = ToZ
                };
            }
            public IEnumerable<Space> SplitX(long x, long fromY, long toY, long fromZ, long toZ) {
                if (fromY > ToY || toY < FromY ||
                    fromZ > ToZ || toZ < FromZ ||
                    x <= FromX || x > ToX) {
                    // Console.Write("x");
                    yield return this;
                }
                else {
                    // Console.Write("X");
                    var clone1 = Clone(); clone1.ToX = x - 1; yield return clone1;
                    var clone2 = Clone(); clone2.FromX = x; yield return clone2;
                }
            }
            public IEnumerable<Space> SplitY(long y, long fromX, long toX, long fromZ, long toZ) {
                if (fromX > ToX || toX < FromX ||
                    fromZ > ToZ || toZ < FromZ ||
                    y <= FromY || y > ToY) {
                    // Console.Write("y");
                    yield return this;
                }
                else {
                    // Console.Write("Y");
                    var clone1 = Clone(); clone1.ToY = y - 1; yield return clone1;
                    var clone2 = Clone(); clone2.FromY = y; yield return clone2;
                }
            }
            public IEnumerable<Space> SplitZ(long z, long fromX, long toX, long fromY, long toY) {
                if (fromX > ToX || toX < FromX ||
                    fromY > ToY || toY < FromY ||
                    z <= FromZ || z > ToZ) {
                    // Console.Write("z");
                    yield return this;
                }
                else {
                    // Console.Write("Z");
                    var clone1 = Clone(); clone1.ToZ = z - 1; yield return clone1;
                    var clone2 = Clone(); clone2.FromZ = z; yield return clone2;
                }
            }

            public long Size { get { return (ToX - FromX + 1) * (ToY - FromY + 1) * (ToZ - FromZ + 1); } }
        }
    }
}