using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;

public class Day17b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var program = Computer.Construct(input).Program;
        var program_string = new string(program.Select(x => (char)('0' + x)).ToArray());
        Console.WriteLine("Looking for program: " + program_string);

        new Computer(program, 13107).Run().ToArray();

        int a = 0;
        while (true)
        {
            var computer = new Computer(program, a);
            
            var new_program = computer.Run().Take(program.Length+1);
            if (program.SequenceEqual(new_program)) {
                break;
            }
            
            // var run_enumerator = computer.Run().GetEnumerator();
            // var success = true;
            // for (int i = 0; i < program.Length; i++)
            // {
            //     if (run_enumerator.MoveNext() == false || run_enumerator.Current != program[i])
            //     {
            //         success = false;
            //         break;
            //     }
            // }
            // foreach (var value in program)
            // {
            //     if (run_enumerator.MoveNext() == false || run_enumerator.Current != value)
            //     {
            //         success = false;
            //         break;
            //     }
            // }
            // if (run_enumerator.MoveNext()) success = false;
            // if (success)
            // {
            //     break;
            // }

            a++;
        }


        // if (a == 656026) {
        //     Console.WriteLine("Before false");
        // }
        // var output = computer.Run().ToArray();
        // var output_string = new string(output.Select(x => (char)('0' + x)).ToArray());
        // // if (a == 656026) {
        // //     Console.WriteLine(output_string);
        // // }
        // if (output_string == program_string)
        // {
        //     break;
        // }
        // a++;
        //}

        print(a);
    }

}

