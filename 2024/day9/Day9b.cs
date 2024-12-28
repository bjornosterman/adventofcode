using System.Numerics;
using System.Runtime.CompilerServices;

public class Day09b : Day
{
    public interface IPos
    {
        int Pos { get; set; }
    }
    public class File : IPos
    {
        public int Pos { get; set; }
        public int Id;
        public int Size;

        internal BigInteger Checksum()
        {
            var result = new BigInteger(0);
            for (int i = Pos; i < Pos + Size; i++) result += i * Id;
            return result;
        }
    }

    public class Gap
    {
        public int Pos { get; set; }
        public int Size;
    }
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        var line = input[0] + "9";
        var c = 0;

        var cdisk = new int[line.Length + 1];

        var files = new List<File>();
        var gaps = new List<Gap>();

        for (int i = 0; i < line.Length; i += 2)
        {
            int file_id = i / 2;
            int file_size = line[i] - '0';
            int gap_size = line[i + 1] - '0';
            files.Add(new File() { Pos = c, Id = file_id, Size = file_size });
            gaps.Add(new Gap() { Pos = c + file_size, Size = gap_size });
            c += file_size + gap_size;
        }

        files.Reverse();
        foreach (var file in files)
        {
            var gap = gaps.FirstOrDefault(x => x.Size >= file.Size);
            if (gap != null && gap.Pos < file.Pos)
            {
                file.Pos = gap.Pos;
                gap.Pos += file.Size;
                gap.Size -= file.Size;
            }
        }


        var result = new BigInteger(0);
        foreach (var value in files.Select(x => x.Checksum()))
        {
            result += value;
        }
        print(result);
    }

}

