using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Day13a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        int result = 0;

        for (int i = 0; i < input.Length; i += 4)
        {
            var (dx_a, dy_a) = GetDeltas(input[i]);
            var (dx_b, dy_b) = GetDeltas(input[i + 1]);
            var (px, py) = GetGoal(input[i + 2]);
            print($"{dx_a} {dy_a} {dx_b} {dy_b} {px} {py}");
            var (a, b) = GetCheepestCombo(dx_a, dy_a, dx_b, dy_b, px, py);
            if (a == 0 && b == 0)
            {
                print("Bust");
            }
            else
            {
                print($"{a} * 3 + {b} * 1 == {a * 3 + b}");
                result += a * 3 + b;
            }
        }

        print(result);
    }

    private (int, int) GetCheepestCombo(int dx_a, int dy_a, int dx_b, int dy_b, int px, int py)
    {
        for (int b = 0; b < 100; b++)
            for (int a = 0; a < 100; a++)
                if (a * dx_a + b * dx_b == px && a * dy_a + b * dy_b == py)
                    return (a, b);
        return (0, 0);
    }

    private (int, int) GetDeltas(string line)
    {
        var match = Regex.Match(line, @"Button .: X\+(?<dx>\d+), Y\+(?<dy>\d+)");
        return (int.Parse(match.Groups["dx"].Value), int.Parse(match.Groups["dy"].Value));
    }

    private (int, int) GetGoal(string line)
    {
        var match = Regex.Match(line, @"Prize: X=(?<px>\d+), Y=(?<py>\d+)");
        return (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value));
    }
}

