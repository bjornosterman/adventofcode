using System.Numerics;

public class Day19a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var patterns = input[0].Split(", ").ToArray();
        var designs = input.Skip(2).ToArray();

        bool IsConstructable(string design)
        {
            // print(design);
            return design.Length == 0 ||
            patterns.Any(x => design.StartsWith(x) && IsConstructable(design.Substring(x.Length)));
        }

        var result = designs.Where(IsConstructable).Count();

        print(result);
    }

}

