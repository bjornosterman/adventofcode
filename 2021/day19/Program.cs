using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            // var is_b = true;
            var filename = is_test ? "sample.txt" : "input.txt";
            var lines = File.ReadAllLines(filename);

            Scanner scanner = null;
            var scanners = new List<Scanner>();
            foreach (var line in lines.Where(x => x != "")) {
                if (line.StartsWith("---")) {
                    scanner = new Scanner() { Name = line };
                    scanners.Add(scanner);
                }
                else {
                    var coords = line.Split(',').Select(int.Parse).ToArray();
                    scanner.Add(coords[0], coords[1], coords[2]);
                }
            }

            var map = new Map();
            do {
                foreach (Scanner s in scanners.ToList()) {
                    Console.WriteLine("Trying to add " + s.Name);
                    if (map.Apply(s)) {
                        Console.WriteLine("Applied " + s.Name);
                        Console.WriteLine("Number of beacons: " + map.Points.Count);
                        scanners.Remove(s);
                    }
                }
            } while (scanners.Any());

            var max_manhattan = map.KnownScannerPositions
            .SelectMany(x => map.KnownScannerPositions.Select(y => new Space(x, y).ManhattanLength)).Max();
            Console.WriteLine("Max Manhattan: " + max_manhattan);

        }
    }

    public class Map {
        public HashSet<Point> Points = new();
        readonly List<Space> _knownSpaces = new();
        public readonly List<Point> KnownScannerPositions = new();

        public bool Apply(Scanner scanner) {
            if (_knownSpaces.Any() == false) {
                ApplyInternal(scanner);
                return true;
            }

            var transformed_scanner = FindTransformed(scanner);
            if (transformed_scanner != null) {
                ApplyInternal(transformed_scanner);
                return true;
            }

            return false;
        }

        private Scanner FindTransformed(Scanner scanner) {
            foreach (var rotated_scanner in scanner.Rotates) {
                // Console.WriteLine("Trying a rotate");
                foreach (var map_point in Points) {
                    foreach (var scanner_point in rotated_scanner.Points) {

                        var moved_scanner = rotated_scanner.Move(map_point - scanner_point);

                        // if (map_point == new Point(-618, -824, -621) && scanner_point == new Point(686, 422, 578)) {
                        //     Console.WriteLine(moved_scanner.ScannerPosition);
                        // }


                        // var moved_scanner_points_inside_known_map = moved_scanner.Points.Where(IsInside).ToList();
                        // var moved_scanner_points_hits = moved_scanner_points_inside_known_map.Where(Points.Contains).ToList();
                        // var known_points_inside_scanner_space = Points.Where(x => moved_scanner.Space.IsIn(x)).ToList();
                        // var known_points_hits = known_points_inside_scanner_space.Where(x => moved_scanner.Points.Contains(x)).ToList();

                        if (
                            moved_scanner.Points.Where(IsInside).All(x => Points.Contains(x))
                            && Points.Where(x => moved_scanner.Space.IsIn(x)).All(x => moved_scanner.Points.Contains(x))
                            && Points.Where(x => moved_scanner.Space.IsIn(x)).Count() >= 12) {
                            Console.WriteLine(moved_scanner.ScannerPosition);
                            return moved_scanner;
                        }
                    }
                }
            }
            return null;
        }

        public bool IsInside(Point p) => _knownSpaces.Any(x => x.IsIn(p));
        public void ApplyInternal(Scanner scanner) {
            KnownScannerPositions.Add(scanner.ScannerPosition);
            _knownSpaces.Add(scanner.Space);
            foreach (var point in scanner.Points) Points.Add(point);
        }
    }

    public class Space {
        public Point P1;
        public Point P2;
        public int ManhattanLength => Math.Abs(P1.X - P2.X) + Math.Abs(P1.Y - P2.Y) + Math.Abs(P1.Z - P2.Z);

        public Space(Point p1, Point p2) {
            P1 = p1;
            P2 = p2;
        }
        public bool IsIn(Point p) {
            return
            IsIn(P1.X, P2.X, p.X) &&
            IsIn(P1.Y, P2.Y, p.Y) &&
            IsIn(P1.Z, P2.Z, p.Z);
        }
        public static bool IsIn(int from, int to, int value) {
            return to > from
            ? value >= from && value <= to
            : value <= from && value >= to;
        }
    }

    public class Point {
        public int X;
        public int Y;
        public int Z;
        public Point(int x, int y, int z) => (X, Y, Z) = (x, y, z);

        public static Point operator +(Point p1, Point p2) {
            return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point operator -(Point p1, Point p2) {
            return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static bool operator ==(Point p1, Point p2) {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }

        public static bool operator !=(Point p1, Point p2) {
            return p1.X != p2.X || p1.Y != p2.Y || p1.Z == p2.Z;
        }

        public override int GetHashCode() {
            return
                X.GetHashCode() ^
                Y.GetHashCode() ^
                Z.GetHashCode();
        }

        public override string ToString() {
            return $"({X},{Y},{Z})";
        }

        public Point RotateX(int count) => count switch {
            0 => this,
            1 => new Point(X, Z, -Y),
            2 => new Point(X, -Y, -Z),
            3 => new Point(X, -Z, Y),
            _ => throw new Exception("Unexpected count")
        };

        public Point RotateY(int count) => count switch {
            0 => this,
            1 => new Point(-Z, Y, X),
            2 => new Point(-X, Y, -Z),
            3 => new Point(Z, Y, -X),
            _ => throw new Exception("Unexpected count")
        };
        public Point RotateZ(int count) {
            return count switch {
                0 => this,
                1 => new Point(Y, -X, Z),
                2 => new Point(-X, -Y, Z),
                3 => new Point(-Y, X, Z),
                _ => throw new Exception("Unexpected count")
            };
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (ReferenceEquals(obj, null)) {
                return false;
            }

            return this == (Point)obj;
        }
    }
    public class Scanner {
        public readonly HashSet<Point> Points;
        public readonly Point ScannerPosition;

        public void Add(int x, int y, int z) {
            Points.Add(new Point(x, y, z));
        }

        public Scanner(Point scannerPosition, IEnumerable<Point> points) {
            ScannerPosition = scannerPosition;
            Points = new HashSet<Point>(points);
        }
        public Scanner() {
            ScannerPosition = new Point(0, 0, 0);
            Points = new HashSet<Point>();
        }

        public Scanner Move(Point point) {
            return new Scanner(point, Points.Select(x => x + point));
        }


        public Space Space {
            get {
                return new Space(
                    ScannerPosition - new Point(1000, 1000, 1000),
                    ScannerPosition + new Point(1000, 1000, 1000)
                );
                // var from_space_point = new Point(
                //     Points.Select(x => x.X).Min(),
                //     Points.Select(x => x.Y).Min(),
                //     Points.Select(x => x.Z).Min()
                // );
                // var to_space_point = new Point(
                //     Points.Select(x => x.X).Max(),
                //     Points.Select(x => x.Y).Max(),
                //     Points.Select(x => x.Z).Max()
                // );
                // return new Space(from_space_point, to_space_point);
            }
        }

        public IEnumerable<Scanner> Rotates {
            get {
                yield return this;
                yield return this.RotateX(1);
                yield return this.RotateX(2);
                yield return this.RotateX(3);

                yield return this.RotateY(1);
                yield return this.RotateX(1).RotateY(1);
                yield return this.RotateX(2).RotateY(1);
                yield return this.RotateX(3).RotateY(1);

                yield return this.RotateY(2);
                yield return this.RotateX(1).RotateY(2);
                yield return this.RotateX(2).RotateY(2);
                yield return this.RotateX(3).RotateY(2);

                yield return this.RotateY(3);
                yield return this.RotateX(1).RotateY(3);
                yield return this.RotateX(2).RotateY(3);
                yield return this.RotateX(3).RotateY(3);

                yield return this.RotateZ(1);
                yield return this.RotateX(1).RotateZ(1);
                yield return this.RotateX(2).RotateZ(1);
                yield return this.RotateX(3).RotateZ(1);

                yield return this.RotateZ(3);
                yield return this.RotateX(1).RotateZ(3);
                yield return this.RotateX(2).RotateZ(3);
                yield return this.RotateX(3).RotateZ(3);
            }
        }

        public string Name { get; set; }

        public Scanner RotateX(int count) {
            return new Scanner(ScannerPosition.RotateX(count), Points.Select(x => x.RotateX(count)));
        }
        public Scanner RotateY(int count) {
            return new Scanner(ScannerPosition.RotateY(count), Points.Select(x => x.RotateY(count)));
        }
        public Scanner RotateZ(int count) {
            return new Scanner(ScannerPosition.RotateZ(count), Points.Select(x => x.RotateZ(count)));
        }
    }
}