
using System.Numerics;
using System.Xml;

public class Day17b2 : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var computer = Computer.Construct(input);
        var program = computer.Program;
        // var program_string = new string(program.Select(x => (char)('0' + x)).ToArray());
        // Console.WriteLine("Looking for program: " + program_string);

        // var filler = new BigInteger(1) << 60;

        BigInteger FindBest(int level, BigInteger currentValue)
        {
            BigInteger best = -1;
            if (level == program.Length) return currentValue;
            var program_part = program.Skip(program.Length - level - 1).ToArray();
            foreach (var i in Enumerable.Range(0, 8))
            {
                // var test_value = filler + (i << (level * 3)) + currentValue;
                var test_value = (currentValue << 3) + i;
                computer.A = test_value;
                var output = computer.Run().ToArray();
                if (output.SequenceEqual(program_part))
                {
                    if (test_value == 117440) {
                        Console.WriteLine("Whoop");
                    }
                    Console.WriteLine("Found: " + level + ": " + test_value);
                    var res = FindBest(level + 1, test_value);
                    if (res != -1 && (best == -1 || res < best))
                    {
                        best = res;
                    }
                }
            }
            return best;
        }

        var result = FindBest(0, 0);

        print(result);
    }

}

