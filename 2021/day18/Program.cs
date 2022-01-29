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
            var lines = File.ReadAllLines(filename);


            var parser = new Parser(lines[0]);
            Number number = parser.Parse();

            if (is_b == false) {
                foreach (var line in lines.Skip(1)) {
                    Console.WriteLine("= " + number);
                    Console.WriteLine("+ " + line);
                    parser = new Parser(line);
                    Number number2 = parser.Parse();
                    number = new Pair() { Left = number, Right = number2 };

                    Reduce(number);
                    Console.WriteLine("= " + number + " --> Magnitude: " + number.Magnitude);
                }
            }
            else {
                // var numbers = lines.Select(x => new Parser(x).Parse()).ToList();

                int max_magnitude = 0;
                foreach (var al in lines)
                    foreach (var bl in lines)
                        if (al != bl) {
                            var a = new Parser(al).Parse();
                            var b = new Parser(bl).Parse();
                            var pair = new Pair() { Left = a, Right = b };
                            Reduce(pair);
                            if (pair.Magnitude > max_magnitude) {
                                max_magnitude = pair.Magnitude;
                                Console.WriteLine("  " + a);
                                Console.WriteLine("+ " + b);
                                Console.WriteLine("= " + pair);
                                Console.WriteLine("-------------------------------------------");
                            }
                            max_magnitude = Math.Max(max_magnitude, pair.Magnitude);
                        }

                Console.WriteLine("Max Magnitude: " + max_magnitude);
            }
        }
        static void Reduce(Number number) {
            while (true) {
                number.UpdateLevel(0);
                var all = number.All.ToList();
                var to_explode = all.OfType<Pair>().FirstOrDefault(x => x.Level == 4);
                if (to_explode != null) {
                    var index_of_pair = all.IndexOf(to_explode);
                    var before = all.Take(index_of_pair).OfType<Regular>().LastOrDefault();
                    var after = all.Skip(index_of_pair + 3).OfType<Regular>().FirstOrDefault();
                    if (before != null) before.Value += ((Regular)to_explode.Left).Value;
                    if (after != null) after.Value += ((Regular)to_explode.Right).Value;
                    foreach (var pair in all.OfType<Pair>()) {
                        if (pair.Left == to_explode) {
                            pair.Left = new Regular() { Value = 0 };
                            break;
                        }
                        if (pair.Right == to_explode) {
                            pair.Right = new Regular() { Value = 0 };
                            break;
                        }
                    }
                }
                else if (all.OfType<Regular>().Any(x => x.Value >= 10)) {
                    var to_split = all.OfType<Regular>().First(x => x.Value >= 10);
                    var new_pair = new Pair() {
                        Left = new Regular() { Value = (int)(to_split.Value / 2) },
                        Right = new Regular() { Value = (int)(to_split.Value / 2) + to_split.Value % 2 }
                    };
                    foreach (var pair in all.OfType<Pair>()) {
                        if (pair.Left == to_split) {
                            pair.Left = new_pair;
                            break;
                        }
                        if (pair.Right == to_split) {
                            pair.Right = new_pair;
                            break;
                        }
                    }
                }
                else {
                    break;
                }
                // Console.Write(number + " --> ");
            }
        }
    }


    public class Parser {
        int Pos = 0;
        string Text;

        public Number Parse() {
            var c = Pop();
            if (c == '[') {
                var left = Parse();
                PopExpected(',');
                var right = Parse();
                PopExpected(']');
                return new Pair { Left = left, Right = right };
            }
            if (c >= '0' && c <= '9') {
                return new Regular() { Value = c - '0' };
            }
            throw new Exception($"Unexpected char '{c}'");
        }


        private char Pop() => Text[Pos++];

        private bool IsAtEnd => Pos >= Text.Length;
        private void PopExpected(char expected) {
            var c = Pop();
            if (c != expected) {
                throw new Exception($"Expected '{expected}', but got '{c}'");
            }
        }
        public Parser(string text) {
            Text = text;
        }
    }
    public abstract class Number {
        public int Level;
        public abstract int Magnitude { get; }
        public abstract void UpdateLevel(int level);
        public abstract IEnumerable<Number> All { get; }
    }
    public class Regular : Number {
        public int Value;
        public override int Magnitude => Value;
        public override void UpdateLevel(int level) {
            Level = level;
        }
        public override IEnumerable<Number> All {
            get {
                yield return this;
            }
        }

        public override string ToString() {
            return Value.ToString();
        }
    }

    public class Pair : Number {
        public Number Left;
        public Number Right;

        public override int Magnitude => Left.Magnitude * 3 + Right.Magnitude * 2;
        public override void UpdateLevel(int level) {
            Level = level;
            Left.UpdateLevel(level + 1);
            Right.UpdateLevel(level + 1);
        }

        public override IEnumerable<Number> All {
            get {
                yield return this;
                foreach (var number in Left.All) yield return number;
                foreach (var number in Right.All) yield return number;
            }
        }
        public override string ToString() {
            return "[" + Left + "," + Right + "]";
        }
    }
}