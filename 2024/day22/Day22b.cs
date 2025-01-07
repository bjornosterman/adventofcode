using System.Numerics;

public class Day22b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        ulong result = 0;

        var dicts = new List<Dictionary<string, int>>();
        foreach (var initial_number in input)
        {
            var iterator = SecretNumberGenerator(ulong.Parse(initial_number));
            var values = iterator.Take(2000).Select(x => (int)x % 10).Cast<int>().ToArray();
            var changes = new int[2000];
            for (int i = 0; i < 2000 - 1; i++)
            {
                changes[i + 1] = values[i + 1] - values[i];
            }
            var dict = new Dictionary<string, int>();
            for (int i = 1; i < 2000 - 4; i++)
            {
                var key = $"{changes[i]},{changes[i + 1]},{changes[i + 2]},{changes[i + 3]}";
                if (dict.ContainsKey(key) == false) dict[key] = values[i + 3];
            }
            dicts.Add(dict);
        }
        print($"Number of dicts: {dicts.Count}");
        var keys = dicts.SelectMany(x => x.Keys).Distinct().ToList();
        print($"Number of keys: {keys.Count}");
        var lookups = dicts.SelectMany(x => x).ToLookup(x => x.Key);
        var sums = lookups.Select(x => (x.Key, Value: x.Sum(y => y.Value)));
        var best = sums.OrderByDescending(x=>x.Value).First();

        // Wrong: 1,0,-1,1
        print(best.Value);

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