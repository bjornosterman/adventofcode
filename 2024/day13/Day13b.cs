using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Day13b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        BigInteger result = 0;

        // 50a + 14b = 5272
        // 37a + 60b = 8766
        print(GetCheepestCombo(50, 37, 14, 60, 5272, 8766));


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
                var x = a * dx_a + b * dx_b;
                var y = a * dy_a + b * dy_b;
                if (x != px || y != py)
                {
                    throw new Exception("ASSERT FAIL!!!");
                }
                print($"{a} * 3 + {b} * 1 == {a * 3 + b}");
                result += a * 3 + b;
            }
        }

        print(result);
    }

    private (BigInteger, BigInteger) GetCheepestCombo(int dx_a, int dy_a, int dx_b, int dy_b, BigInteger px, BigInteger py)
    {
        var ass = dx_a * py - dy_a * px;
        var bss = dy_b * px - dx_b * py;
        if (ass < 0 && bss < 0)
        {
            ass = -ass;
            bss = -bss;
        }
        BigInteger gcd = 1;
        if (ass < 0 || bss < 0)
        {

        }
        else
        {
            // print($"Finding GCD for {ass} and {bss}");
            gcd = FindGCD2(ass, bss);
        }
        if (gcd == 1)
        {
            if (px % dx_b == 0 && px / dx_b * dy_b == py) return (0, px / dx_b);
            if (px % dx_a == 0 && px / dx_a * dy_a == py) return (px / dx_a, 0);
            return (0, 0);
        }
        else
        {
            if (px % (ass / gcd * dx_b + bss / gcd * dx_a) != 0) return (0,0);
            var multiplier = px / (ass / gcd * dx_b + bss / gcd * dx_a);
            return (bss * multiplier / gcd, ass * multiplier / gcd);
        }

    }

    private (int, int) GetDeltas(string line)
    {
        var match = Regex.Match(line, @"Button .: X\+(?<dx>\d+), Y\+(?<dy>\d+)");
        return (int.Parse(match.Groups["dx"].Value), int.Parse(match.Groups["dy"].Value));
    }

    private (BigInteger, BigInteger) GetGoal(string line)
    {
        var match = Regex.Match(line, @"Prize: X=(?<px>\d+), Y=(?<py>\d+)");
        return (new BigInteger(10_000_000_000_000) + int.Parse(match.Groups["px"].Value), new BigInteger(10_000_000_000_000) + int.Parse(match.Groups["py"].Value));
        // return (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value));
    }

    private BigInteger FindGCD(BigInteger n1, BigInteger n2)
    {
        while (n1 != n2)
        {
            if (n1 > n2)
                n1 %= n2;
            else
                n2 %= n1;
        }
        return n1;
    }
    private static BigInteger FindGCD2(BigInteger a, BigInteger b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}

