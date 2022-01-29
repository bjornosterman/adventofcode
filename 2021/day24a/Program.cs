using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day {
    class Program {

        static void Main(string[] args) {
            var is_test = false;
            var filename = is_test ? "sample.txt" : "input.txt";
            var lines = File.ReadAllLines(filename).TakeWhile(x => x.Length > 0);
            // var input = is_test ? "1" : "`00000000000001";

            // var input = new int[14] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            var input = "83499629198498".Select(x => x - '0').ToArray(); // för högt för lägsta
            // var input = "93499629198499".Select(x => x - '0').ToArray();
            // Denna ger 23: 94497429698943
            // var input = "93499629698999".Select(x => x - '0').ToArray(); // denna var korrekt
            // 1 <--> 14
            // 2 <--> 13
            // 3 <--> 4
            // 5 <--> 6
            // 7 <--> 8
            // 9 <--> 12
            // 10 <--> 11
            // "11164118121471" --> Minsta



            while (true) {
                Console.Write("              ");
                for (int i = 0; i < 14; i++) {
                    Console.Write($"{(char)('0' + input[i]),14}");
                }
                Console.WriteLine();

                var alu = new ALU(lines);
                var responses = alu.Run(input, false);

                for (int i = 0; i <= 14; i++) {
                    Console.Write($"{responses[i],14}");
                }
                Console.WriteLine();
                int index = -1;
                ConsoleKeyInfo key = default;
                while (index < 0) {
                    key = Console.ReadKey(true);
                    index = key.Key switch {
                        ConsoleKey.Q => 0,
                        ConsoleKey.W => 1,
                        ConsoleKey.E => 2,
                        ConsoleKey.R => 3,
                        ConsoleKey.T => 4,
                        ConsoleKey.Y => 5,
                        ConsoleKey.U => 6,
                        ConsoleKey.I => 7,
                        ConsoleKey.O => 8,
                        ConsoleKey.P => 9,
                        ConsoleKey.A => 10,
                        ConsoleKey.S => 11,
                        ConsoleKey.D => 12,
                        ConsoleKey.F => 13,
                        ConsoleKey.Escape => 100,
                        _ => -1
                    };
                    if (index == -1) {
                        Console.WriteLine(key.Key);
                        Console.WriteLine(key.Modifiers);
                        Console.WriteLine(key.KeyChar);
                    }
                }
                if (index == 100) return;
                input[index] += key.Modifiers == ConsoleModifiers.Shift ? -1 : 1;
            }



            // for (int i = 0; i < 2; i++) {
            //     bool stop = false;
            //     do {
            //         Console.WriteLine(new string(input.Select(x => (char)('0' + x)).ToArray()));
            //         try {
            //             var alu = new ALU(lines);
            //             alu.Run(input);
            //             input[i]--;
            //             // stop = true;
            //         }
            //         catch {
            //             input[i]--;
            //             stop = false;
            //         }
            //     } while (!stop && input[i] >= 0);
            // }
        }
    }
    public class ALU {
        IEnumerable<Instruction> _instructions;
        public ALU(IEnumerable<string> instructions) {
            _instructions = instructions.Select(Parse).ToList();

        }
        public Instruction Parse(string line) {
            var split = line.Split(" ");
            return new Instruction() {
                Line = line,
                Type = Enum.Parse<InstructionType>(split[0], true),
                To = split[1],
                From = split[0] == "inp" ? null : split[2]
            };
        }

        public int W, X, Y, Z;

        public void Store(string to, int value) {
            switch (to) {
                case "w":
                    W = value;
                    break;
                case "x":
                    X = value;
                    break;
                case "y":
                    Y = value;
                    break;
                case "z":
                    Z = value;
                    break;
            }
        }

        public int Load(string from) {
            switch (from) {
                case "w": return W;
                case "x": return X;
                case "y": return Y;
                case "z": return Z;
                default: return int.Parse(from);
            }
        }

        public int[] Run(int[] input, bool printSteps) {
            // int[] input_values = input.Select(x => x - '0').ToArray();
            int pos = 0;

            var responses = new List<int>();

            foreach (var instruction in _instructions) {
                if (instruction.Type == InstructionType.Inp) {
                    // if (Z != 0) throw new Exception();
                    responses.Add(Z);
                }
                var new_value = instruction.Type switch {
                    InstructionType.Inp => input[pos++],
                    InstructionType.Add => Load(instruction.To) + Load(instruction.From),
                    InstructionType.Mul => Load(instruction.To) * Load(instruction.From),
                    InstructionType.Div => (int)(Load(instruction.To) / Load(instruction.From)),
                    InstructionType.Mod => Load(instruction.To) % Load(instruction.From),
                    InstructionType.Eql => Load(instruction.To) == Load(instruction.From) ? 1 : 0
                };
                Store(instruction.To, new_value);
                if (printSteps) Print(instruction.Line);
            }
            responses.Add(Z);
            return responses.ToArray();
            // Print("end");
        }

        public void Print(string line) {
            Console.ForegroundColor = line.StartsWith("inp") ? ConsoleColor.Green : ConsoleColor.White;
            Console.Write($"{line,-20}");
            PrintValue(W);
            PrintValue(X);
            PrintValue(Y);
            PrintValue(Z);
            Console.WriteLine();
        }
        public void PrintValue(int value) {
            Console.ForegroundColor = value < 0 ? ConsoleColor.Red : ConsoleColor.White;
            Console.Write($"{value,20}");
        }
    }
    public enum InstructionType {
        Inp,
        Add,
        Mul,
        Div,
        Mod,
        Eql
    }
    public class Instruction {
        public string Line;
        public InstructionType Type;
        public string To;
        public string From;
    }
}