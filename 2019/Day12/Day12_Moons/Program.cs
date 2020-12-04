using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day12_Moons
{
    class Program
    {
        static void Main(string[] args)
        {
            //MoonsCalculator.Run(
            //               new [] { -1, 0, 2, 2, -10, -7, 4, -8, 8, 3, 5, -1 },
            //               new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            //               );

            //MoonsCalculator.Run(
            //   new [] { 1, -4, 3, -14, 9, -4, -4, -6, 7, 6, -9, -11 },
            //   new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            //   );

            //MoonsCalculator.Run(
            //   new[] { -8, -10, 0, 5, 5, 10, 2, -7, 3, 9, -8, -3 },
            //   new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            //   );

            //var x1 = MoonsCalculator.RunOne(-1, 2, 4, 3).First();
            //var x2 = MoonsCalculator.RunOne(0, -10, -8, 5).First();
            //var x3 = MoonsCalculator.RunOne(2, -7, 8, -1).First();

            var x1 = MoonsCalculator.RunOne(1, -14, -4, 6).First();
            var x2 = MoonsCalculator.RunOne(-4, 9, -6, -9).First();
            var x3 = MoonsCalculator.RunOne(3, -4, 7, -11).First();

            var f1 = Factorize(x1);
            var f2 = Factorize(x2);
            var f3 = Factorize(x3);

            long result = 1;

            foreach (var key in f1.Concat(f2).Concat(f3).Distinct())
            {
                var c1 = f1.Where(x => x == key).Count();
                var c2 = f2.Where(x => x == key).Count();
                var c3 = f3.Where(x => x == key).Count();
                var max = Math.Max(Math.Max(c1, c2), c3);
                for (int x = 0; x < max; x++) result *= key;
            }

            MoonsCalculator.RunParallel(
               new[] { 1, -4, 3, -14, 9, -4, -4, -6, 7, 6, -9, -11 }
               );

            //MoonsCalculator.RunParallel(
            //   new[] { -8, -10, 0, 5, 5, 10, 2, -7, 3, 9, -8, -3 }
            //   );


            return;
            //var positions = new Point[4] {
            //    new Point(-1,0,2),
            //    new Point(2,-10,-7),
            //    new Point(4,-8,8),
            //    new Point(3,5,-1),
            //};

            //< x = 1, y = -4, z = 3 >
            //< x = -14, y = 9, z = -4 >
            //< x = -4, y = -6, z = 7 >
            //< x = 6, y = -9, z = -11 >

            var origPos = new Point[4] {
                new Point(1,-4,3),
                new Point(-14,9,-4),
                new Point(-4,-6,7),
                new Point(6,-9,-11),
            };

            var positions = Clone(origPos);


            var velocities = new Point[4];
            var origVel = Clone(velocities);
            var steps = 1000;
            var seenStates = new HashSet<string>();
            int step;
            for (step = 0; true; step++)
            {
                //if (step == 1000000)
                //{
                //    origPos = Clone(positions);
                //    origVel = Clone(velocities);
                //}
                if (step % 1000000 == 0)
                {
                    var e1 = GetEnery(positions);
                    Console.WriteLine("Step: " + step + " ");
                }

                //var sb = new StringBuilder();
                //for (int a = 0; a < 4; a++)
                //    sb.Append(positions[a].SimpleToString() + velocities[a].SimpleToString());
                //    //sb.AppendLine($"pos= <{positions[a]}>, vel= <{velocities[a]}>");

                //Console.WriteLine(sb);
                //if (seenStates.Add(sb.ToString()) == false) break;

                //adjust velocities
                for (int a = 0; a < 4; a++)
                    for (int b = 0; b < 4; b++)
                    {
                        velocities[a] += GetVeloChange(positions[a], positions[b]);
                    }

                for (int a = 0; a < 4; a++)
                    positions[a] += velocities[a];

                if (
                    positions[0] == origPos[0] &&
                    positions[1] == origPos[1] &&
                    positions[2] == origPos[2] &&
                    positions[3] == origPos[3] &&
                    velocities[0] == origVel[0] &&
                    velocities[1] == origVel[1] &&
                    velocities[2] == origVel[2] &&
                    velocities[3] == origVel[3]
                    )
                {
                    break;
                }
            }
            Console.WriteLine("Step = " + step);

            var totalEnergy = Enumerable.Range(0, 4).Select(i => positions[i].Energy * velocities[i].Energy).Sum();
            Console.WriteLine("Total Energy: " + totalEnergy);
        }

        private static long[] Factorize(long number)
        {
            var test = number;
            var factor = 2;
            var factors = new List<long>();
            do
            {
                if (test % factor == 0)
                {
                    test /= factor;
                    factors.Add(factor);
                }
                else
                {
                    factor++;
                }
            } while ((factor != number));
            return factors.ToArray();
        }

        private static object GetEnery(Point[] positions)
        {
            throw new NotImplementedException();
        }

        private static Point[] Clone(Point[] points)
        {
            var clone = new Point[points.Length];
            for (int i = 0; i < points.Length; i++) clone[i] = points[i].Clone();
            return clone;
        }

        private static Point GetVeloChange(Point point1, Point point2)
        {
            return new Point(
                GetVeloChange(point1.X, point2.X),
                GetVeloChange(point1.Y, point2.Y),
                GetVeloChange(point1.Z, point2.Z)
                );
        }
        private static int GetVeloChange(int value1, int value2)
        {
            if (value1 == value2) return 0;
            return value1 > value2 ? -1 : 1;
        }
    }
    public struct Point
    {
        public int X, Y, Z;
        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z;
        }
        public static bool operator !=(Point point1, Point point2)
        {
            return point1.X != point2.X || point1.Y != point2.Y || point1.Z != point2.Z;
        }
        public override bool Equals(object obj)
        {
            var point1 = this;
            var point2 = (Point)obj;
            return point1.X == point2.X && point1.Y == point2.Y && point1.Z == point2.Z;
        }

        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y, point1.Z + point2.Z);
        }
        public override string ToString()
        {
            return $"X={X,3}, Y={Y,3}, Z={Z,3}";
        }
        public string SimpleToString() => X + "," + Y + "," + Z + "|";

        public Point Clone()
        {
            return new Point(X, Y, Z);
        }

        public int Energy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
    }
}
