using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class Day11a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        var stones = input[0].Split(" ").Select(long.Parse).ToList();
        var iterations = sample ? 25 : 25;
        print(string.Join(", ", stones));
        foreach (var _ in Enumerable.Range(0, iterations))
        {
            for (int i = 0; i < stones.Count(); i++)
            {
                var value = stones[i];
                if (value == 0) stones[i] = 1;
                else
                {
                    var str = value.ToString();
                    if (str.Length % 2 == 0)
                    {
                        stones.Insert(i, long.Parse(str.Substring(0, str.Length / 2)));
                        stones[i + 1] = long.Parse(str.Substring(str.Length / 2, str.Length / 2));
                        i++;
                    }
                    else
                    {
                        stones[i] = value * 2024;
                    }
                }
            }
            print($"{_}: {stones.Count()}");
        }
        // print(string.Join(", ", stones));

        print(stones.Count());
    }

}

