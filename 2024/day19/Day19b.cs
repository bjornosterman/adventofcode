using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Numerics;

public class Day19b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var patterns = input[0].Split(", ").ToArray();
        var designs = input.Skip(2).ToArray();

        var cache = new Dictionary<string, long>();

        long IsConstructable(string design)
        {
            if (!cache.ContainsKey(design))
            {
                cache[design] = design.Length == 0 ? (long)1 :
                patterns.Sum(x => design.StartsWith(x) ? IsConstructable(design.Substring(x.Length)) : 0);
            }
            return cache[design];
        }

        var result = designs.Sum(IsConstructable);

        print(result);
    }

}

