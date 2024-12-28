using System.Numerics;
using System.Runtime.CompilerServices;

public class Day09a : Day
{
    public override void Run()
    {
        var sample = true;
        var input = sample ? samplePuzzleInput : puzzleInput;
        var line = input[0] + "9";
        var c = 0;
        var disk = new int[line.Length * 7];
        for (int i = 0; i < line.Length; i += 2)
        {
            int file_id = i / 2;
            int file_size = line[i] - '0';
            int gap_size = line[i+1] - '0';
            for (int j = 0; j < file_size; j++) disk[c + j] = file_id;
            c += file_size + gap_size;
        }

        var disk2 = new int[c];
        for (int i = 0; i < c; i++) disk2[i] = disk[i];
        disk = disk2;

        foreach (var x in disk) Console.Write(x);
        Console.WriteLine();

        int backer = disk.Length - 1;
        do { backer--; } while (disk[backer] == 0);
        for (int i = line[0] - '0'; i < backer; i++)
        {
            if (disk[i] == 0)
            {
                disk[i] = disk[backer];
                disk[backer] = 0;
                do { backer--; } while (disk[backer] == 0);
            }
        }

        foreach (var x in disk) Console.Write(x);
        Console.WriteLine();
        var result = new BigInteger(0);
        foreach (var value in disk.Select((x, y) => new BigInteger(x * y))) {
            result += value;
        }
        print(result);
    }

}

