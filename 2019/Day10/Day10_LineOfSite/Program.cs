using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10_LineOfSite
{
    class Program
    {
        static void Main(string[] args)
        {
            //            var points = Parse(
            //                @"
            //.#..#
            //.....
            //#####
            //....#
            //...##");

            var points = Parse(@"
            .###..#######..####..##...#
            ########.#.###...###.#....#
            ###..#...#######...#..####.
            .##.#.....#....##.#.#.....#
            ###.#######.###..##......#.
            #..###..###.##.#.#####....#
            #.##..###....#####...##.##.
            ####.##..#...#####.#..###.#
            #..#....####.####.###.#.###
            #..#..#....###...#####..#..
            ##...####.######....#.####.
            ####.##...###.####..##....#
            #.#..#.###.#.##.####..#...#
            ..##..##....#.#..##..#.#..#
            ##.##.#..######.#..#..####.
            #.....#####.##........#####
            ###.#.#######..#.#.##..#..#
            ###...#..#.#..##.##..#####.
            .##.#..#...#####.###.##.##.
            ...#.#.######.#####.#.####.
            #..##..###...###.#.#..#.#.#
            .#..#.#......#.###...###..#
            #.##.#.#..#.#......#..#..##
            .##.##.##.#...##.##.##.#..#
            #.###.#.#...##..#####.###.#
            #.####.#..#.#.##.######.#..
            .#.#####.##...#...#.##...#.");

            //            var points = Parse(@"
            //.#....#####...#..
            //##...##.#####..##
            //##...#...#.#####.
            //..#.....X...###..
            //..#.#.....#....##");

            Func<Point, Point, bool> IsInSight = (Point a, Point b) =>
            {
                if (a == b) return false;
                var step = GetStepPoint(a, b);
                var walk = a + step;
                while (walk != b)
                {
                    if (points.Contains(walk)) return false;
                    walk += step;
                }
                return true;
            };

            var bestPoint = points.Select(a => new { Point = a, InSight = points.Where(b => IsInSight(a, b)).ToList() })
                .OrderByDescending(a => a.InSight.Count)
                .First();

            Console.WriteLine("Best: " + bestPoint.Point + " with " + bestPoint.InSight.Count + " sights");

            Console.WriteLine(" 0, 1: " + Math.Atan2(0, 1) * 180 / Math.PI);
            Console.WriteLine(" 1, 0: " + Math.Atan2(2, 0) * 180 / Math.PI);
            Console.WriteLine(" 0,-1: " + Math.Atan2(0, -1) * 180 / Math.PI);
            Console.WriteLine("-1, 0: " + Math.Atan2(-1, 0) * 180 / Math.PI);
            var selectedPoint = bestPoint.Point;
            var sortedPoints = points
                .Where(x => x != selectedPoint)
                .Select(p =>
                {
                    var angle = GetAngle(selectedPoint, p);
                    var length = GetLength(selectedPoint, p);
                    return new
                    {
                        Point = p,
                        Angle = angle,
                        Length = length
                    };
                })
                .OrderBy(x => x.Angle).ThenBy(x => x.Length)
                .ToList();

            double angle = -1;
            var count = 1;
            while (sortedPoints.Any())
            {
                var asteroid = sortedPoints.FirstOrDefault(x => x.Angle > angle);
                if (asteroid == null)
                {
                    angle = -1;
                }
                else
                {
                    sortedPoints.Remove(asteroid);
                    angle = asteroid.Angle;
                    Console.WriteLine($"{count++:###} {asteroid.Point:-10} {angle}");
                }
            }
        }


        private static int GetLength(Point point1, Point point2)
        {

            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        private static double GetAngle(Point from, Point to)
        {
            var angle = Math.Atan2(to.X - from.X, from.Y - to.Y);
            angle *= 180;
            angle /= Math.PI;
            if (angle < 0) angle += 360;
            return angle;
        }

        private static Point GetStepPoint(Point a, Point b)
        {
            var xDelta = b.X - a.X;
            var yDelta = b.Y - a.Y;
            if (xDelta == 0) return new Point(xDelta, yDelta / Math.Abs(yDelta));
            if (yDelta == 0) return new Point(xDelta / Math.Abs(xDelta), yDelta);
            int divider = 2;
            int tmpX = Math.Abs(xDelta);
            int tmpY = Math.Abs(yDelta);
            while (divider <= tmpX && divider <= tmpY)
            {
                if (tmpX % divider == 0 && tmpY % divider == 0)
                {
                    tmpX /= divider;
                    tmpY /= divider;
                    xDelta /= divider;
                    yDelta /= divider;
                }
                else
                {
                    divider++;
                }
            }
            return new Point(xDelta, yDelta);
        }

        public static Point[] Parse(string map)
        {
            var points = new List<Point>();
            var sr = new StringReader(map);
            int y = 0;
            for (string line = sr.ReadLine().Trim(); line != null; line = sr.ReadLine()?.Trim())
            {
                if (line.Length == 0) continue;
                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] != '.') points.Add(new Point(x, y));
                }
                y++;
            }
            return points.ToArray();
        }
    }
}
