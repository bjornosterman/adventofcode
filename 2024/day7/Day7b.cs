using System.Numerics;

public class Day07b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        BigInteger correct_found = 0;
        foreach (string line in input)
        {
            var split = line.Split(':');
            var test_value = BigInteger.Parse(split[0]);
            var parts = split[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(BigInteger.Parse).ToArray();
            if (FindCorrect(parts, test_value, 1, parts[0]))
            {
                correct_found += test_value;
            }
        }
        print(correct_found);
    }

    private bool FindCorrect(BigInteger[] parts, BigInteger test_value, int current_index, BigInteger current_value)
    {
        if (current_index == parts.Length)
        {
            return current_value == test_value;
        }
        if (FindCorrect(parts, test_value, current_index + 1, current_value + parts[current_index])) return true;
        if (FindCorrect(parts, test_value, current_index + 1, current_value * parts[current_index])) return true;
        if (FindCorrect(parts, test_value, current_index + 1, BigInteger.Parse($"{current_value}{parts[current_index]}"))) return true;
        return false;
    }
}

