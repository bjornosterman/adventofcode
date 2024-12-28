using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Day11b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        var stones = input[0].Split(" ").Select(long.Parse).ToList();
        var iterations = sample ? 25 : 75;
        print(string.Join(", ", stones));
        long result = 0;
        for (int i = 0; i < stones.Count(); i++)
        {
            var number = ExplodeStone(stones[i], iterations);
            result += number;
            print(number);
        }
        // print(string.Join(", ", stones));

        print(result);
    }

    private Dictionary<(long, int), long> CachedResults = new Dictionary<(long, int), long>();

    private long ExplodeStone(long value, int iterations)
    {
        if (iterations == 0) return 1;
        var key = (value, iterations);
        if (CachedResults.ContainsKey(key)) return CachedResults[key];
        long ret = 0;
        if (value == 0) ret = ExplodeStone(1, iterations - 1);
        else
        {
            var str = value.ToString();
            if (str.Length % 2 == 0)
            {
                ret = ExplodeStone(long.Parse(str.Substring(0, str.Length / 2)), iterations - 1) +
                ExplodeStone(long.Parse(str.Substring(str.Length / 2, str.Length / 2)), iterations - 1);
            }
            else
            {
                ret = ExplodeStone(value * 2024, iterations - 1);
            }
        }
        CachedResults[key] = ret;
        return ret;
    }

}

