using System.Numerics;
using System.Text.RegularExpressions;

public class Day14 : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var width = sample ? 11 : 101;
        var mid = (width - 1) / 2;
        var height = sample ? 7 : 103;
        var split = (height - 1) / 2;
        int q1 = 0, q2 = 0, q3 = 0, q4 = 0;

        foreach (var line in input)
        {
            var match = Regex.Match(line, @"p=(?<x>\d+),(?<y>\d+) v=(?<dx>\-?\d+),(?<dy>\-?\d+)");
            int px = int.Parse(match.Groups["x"].Value);
            int py = int.Parse(match.Groups["y"].Value);
            int dx = int.Parse(match.Groups["dx"].Value);
            int dy = int.Parse(match.Groups["dy"].Value);
            int nx = (px + 100 * width + 100 * dx) % width;
            int ny = (py + 100 * height + 100 * dy) % height;
            if (nx < mid && ny < split) q1++;
            if (nx > mid && ny < split) q2++;
            if (nx < mid && ny > split) q3++;
            if (nx > mid && ny > split) q4++;
            print($"{nx}, {ny}");
            // print($"{q1} * {q2} * {q3} * {q4} = {q1 * q2 * q3 * q4}");
        }
        print($"{q1} * {q2} * {q3} * {q4} = {q1 * q2 * q3 * q4}");
    }

}

