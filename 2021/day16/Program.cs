using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var is_b = false;
            var filename = is_test ? "sample.txt" : "input.txt";

            var lines = File.ReadAllLines(filename);
            foreach (var line in lines) {
                Console.WriteLine(line);
                var bits = Bits.ParseHexString(line);
                var parser = new Parser(bits);
                var thing = parser.Parse();

                Console.WriteLine(thing.Evaluate());
            }
        }
    }
    public class Parser {
        Bits _bits;
        public Parser(Bits bits) {
            _bits = bits;
        }

        public bool IsAtEnd { get { return _bits.IsAtEnd; } }

        // public IEnumerable<Thing> Parse() {
        //     while (true) {
        //         var thing = ParseOne();
        //         if (thing == null) yield break;
        //         yield return thing;
        //     }
        // }

        public Thing Parse() {
            // if (_bits.IsAtEnd) return null;
            var version = _bits.PopByte(3);
            // if (_bits.IsAtEnd) return null;
            var type = _bits.PopByte(3);
            // if (_bits.IsAtEnd) return null;
            switch (type) {
                case 4:
                    var more = true;
                    ulong value = 0;
                    do {
                        more = _bits.PopBool();
                        value <<= 4;
                        value |= _bits.PopByte(4);
                    } while (more);
                    return new Literal(version, value);
                    break;
                default:
                    var sub_things = new List<Thing>();
                    if (_bits.PopBool()) {
                        var number_of_subpackets = (int)_bits.PopLong(11);
                        for (int i = 0; i < number_of_subpackets; i++) {
                            var sub_thing = Parse();
                            if (sub_thing == null) {
                                var a = 1;
                            }
                            sub_things.Add(sub_thing);
                        }
                    }
                    else {
                        var number_of_subpacket_bytes = (int)_bits.PopLong(15);
                        var sub_parser = new Parser(new Bits(_bits.PopBits(number_of_subpacket_bytes)));
                        while (sub_parser.IsAtEnd == false) {
                            var sub_thing = sub_parser.Parse();
                            sub_things.Add(sub_thing);
                        }
                    }
                    var op = new Operator(version, (OperatorType)type, sub_things);
                    return op;
            }

        }
    }

    public class Bits {
        private List<bool> _bits = new List<bool>();
        public int Pos = 0;
        public Bits(IEnumerable<bool> bits) {
            _bits = bits.ToList();
        }
        public Bits() { }
        public static Bits ParseHexString(string text) {
            var bits = new Bits();
            foreach (var c in text.ToUpper()) {
                var nibble = Convert.ToByte(new string(c, 1), 16);
                bits.Add((nibble & 8) > 0);
                bits.Add((nibble & 4) > 0);
                bits.Add((nibble & 2) > 0);
                bits.Add((nibble & 1) > 0);
            }
            return bits;
        }
        public void Add(bool bit) {
            _bits.Add(bit);
        }

        public bool IsAtEnd {
            get {
                return Pos >= _bits.Count;
            }
        }

        public override string ToString() {
            return new string(_bits.Select(x => x ? '1' : '0').ToArray());
        }

        public ulong PopLong(int numberOfBits) {
            ulong value = 0;
            for (int i = 0; i < numberOfBits; i++) {
                value <<= 1;
                value += _bits[Pos++] ? (ulong)1 : 0;
            }
            return value;
        }

        public bool PopBool() {
            return _bits[Pos++];
        }
        public byte PopByte(int numberOfBits) {
            return (byte)PopLong(numberOfBits);
        }
        public bool[] PopBits(int numberOfBits) {
            var bits = new bool[numberOfBits];
            Array.Copy(_bits.ToArray(), Pos, bits, 0, numberOfBits);
            Pos += numberOfBits;
            return bits;
        }
    }
    public abstract class Thing {
        public abstract int VersionSum();
        public abstract ulong Evaluate();
        public int Version;
    }

    public class Literal : Thing {
        public ulong Value;
        public Literal(int version, ulong value) {
            this.Version = version;
            Value = value;
        }
        public override int VersionSum() {
            return Version;
        }
        public override ulong Evaluate() {
            return Value;
        }
    }

    public enum OperatorType {
        Sum,
        Product,
        Minimum,
        Maximum,
        Literal,
        GreaterThen,
        LessThen,
        Equal
    }
    public class Operator : Thing {
        public List<Thing> Things = new List<Thing>();
        public OperatorType Type;
        public Operator(int version, OperatorType type, IEnumerable<Thing> things) {
            base.Version = version;
            Type = type;
            Things = things.ToList();
        }

        public override int VersionSum() {
            return this.Version + Things.Sum(x => x.VersionSum());
        }

        public override ulong Evaluate() {
            return Type switch {
                OperatorType.Sum => Things.Select(x => x.Evaluate()).Aggregate((a, b) => a + b),
                OperatorType.Product => Things.Select(x => x.Evaluate()).Aggregate((a, b) => a * b),
                OperatorType.Minimum => Things.Min(x => x.Evaluate()),
                OperatorType.Maximum => Things.Max(x => x.Evaluate()),
                OperatorType.GreaterThen => Things[0].Evaluate() > Things[1].Evaluate() ? 1UL : 0,
                OperatorType.LessThen => Things[0].Evaluate() < Things[1].Evaluate() ? 1UL : 0,
                OperatorType.Equal => Things[0].Evaluate() == Things[1].Evaluate() ? 1UL : 0,
            };
        }
    }
}