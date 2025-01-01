using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;

public class Day17a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var computer = Computer.Construct(input);

        var result = string.Join(",", computer.Run().ToList());

        print(result);
    }

}

public class Computer
{
    public Computer(int[] program, int a = 0, int b = 0, int c = 0)
    {
        A = a;
        B = b;
        C = c;
        Program = program;
    }

    public BigInteger A;
    public string BinA => A.ToString("B");
    public BigInteger B;
    public string BinB => B.ToString("B");
    public BigInteger C;
    public string BinC => C.ToString("B");

    internal static Computer Construct(string[] input)
    {
        int a = int.Parse(input[0].Split(":")[1]);
        int b = int.Parse(input[1].Split(":")[1]);
        int c = int.Parse(input[2].Split(":")[1]);

        var program = input[4].Split(":")[1].Split(",").Select(int.Parse).ToArray();

        return new Computer(program, a, b, c);
    }

    public int[] Program { get; }
    public bool InsideProgram => IP < Program.Length;

    public int IP;

    public OpCode Op => (OpCode)Program[IP];
    public int Literal => Program[IP + 1];
    public BigInteger Combo => Literal switch
    {
        4 => A,
        5 => B,
        6 => C,
        _ => Literal
    };

    public IEnumerable<int> Run()
    {
        IP = 0;
        // if (A % 1_000_000 == 0)
        //     Console.WriteLine($"Running program with A = {A}");
        while (InsideProgram)
        {
            checked
            {
                bool supress_step = false;
                switch (Op)
                {
                    case OpCode.Adv:
                        A = A / (int)Math.Pow(2, (int)Combo);
                        break;
                    case OpCode.Bxl:
                        B ^= Literal;
                        break;
                    case OpCode.Bst:
                        B = Combo % 8;
                        break;
                    case OpCode.Jnz:
                        if (A != 0)
                        {
                            IP = Literal;
                            supress_step = true;
                        }
                        break;
                    case OpCode.Bxc:
                        B ^= C;
                        break;
                    case OpCode.Out:
                        yield return (int)(Combo % 8);
                        break;
                    case OpCode.Bdv:
                        B = A / (int)Math.Pow(2, (int)Combo);
                        break;
                    case OpCode.Cdv:
                        C = A / (int)Math.Pow(2, (int)Combo);
                        break;
                }
                if (!supress_step) IP += 2;
            }
        }

    }

}
public enum OpCode
{
    Adv, // int(A / 2^Combo) => A
    Bxl, // B XOR Literal => B 
    Bst, // Combo Mod 8 => B
    Jnz, // A == 0 ? Jump to Literal
    Bxc, // B XOR C => B
    Out, // Print Combo MOD 8
    Bdv, // int(A / 2^Combo) => B
    Cdv  // int(A / 2^Combo) => B
}