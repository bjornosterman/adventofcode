using System.Numerics;

public class Day22a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        ulong result = 0;

        // foreach ( var secret in SecretNumberGenerator(123).Take(10)) print(secret);
        checked
        {
            foreach (var initial_number in input)
            {
                var iterator = SecretNumberGenerator(ulong.Parse(initial_number));
                var value_2000 = iterator.Skip(2000 - 1).First();
                result += value_2000;
            }
        }
        print(result);

    }

    private IEnumerable<ulong> SecretNumberGenerator(ulong intialValue)
    {
        var secret = intialValue;

        while (true)
        {
            secret ^= secret * 64; // test <<= 6
            secret %= 16777216; // prune
            secret ^= secret / 32; // test >>= 5
            secret %= 16777216; // prune
            secret ^= secret * 2048; // test <<= 11
            secret %= 16777216; // prune
            yield return secret;
        }
    }

}