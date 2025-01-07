public class Day21b : Day
{
    readonly Dictionary<string, string> DirKeyStepPaths = ConstructTransitionDictionary(dirkeys);
    readonly Dictionary<string, string[]> NumKeyStepPaths = ConstructTransitionOptionsDictionary(numkeys);

    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        long result = 0;

        var number_of_dirkey_pressers = 25;

        foreach (var line in input)
        {
            var alts = GetNumKeyPaths(line);
            var counts = new List<long>();
            foreach (var alt in alts)
            {
                var presser = NumKeyPresser(alt);
                for (int i = 0; i < number_of_dirkey_pressers; i++)
                {
                    presser = DirKeyPresser(presser);
                }
                counts.Add(presser.Sum(x => x.Count));
            }
            var min = counts.Min();
            var code_int = int.Parse(line[..3]);
            print($"{min} * {code_int} = {min * code_int}");
            result += min * code_int;
        }

        print(result);
    }

    IEnumerable<TransitionCount> DirKeyPresser(IEnumerable<TransitionCount> inner)
    {
        var outer = inner.SelectMany(tc => DirKeyStepPaths[tc.Transition].ToTransitions().Select(x => new TransitionCount(x, tc.Count)));
        var returns = outer.Merge();
        return returns;
    }

    private IEnumerable<string> GetNumKeyPaths(string keys)
    {
        IEnumerable<string> combos = [""];
        foreach (var step in keys.ToTransitions()) //.TrimEnd('A')))
        {
            combos = combos.SelectMany(x => NumKeyStepPaths[step], (x, y) => x + y).ToList();
        }
        return combos.ToList();
    }

    private static IEnumerable<TransitionCount> NumKeyPresser(string keys)
    {
        return keys.ToTransitions().Select(x => new TransitionCount(x, 1)).Merge();
    }

    private static Dictionary<string, string[]> ConstructTransitionOptionsDictionary(string text)
    {
        var dict = new Dictionary<string, string[]>();
        foreach (var dir_key in text.Split(Environment.NewLine))
        {
            var key = dir_key.Substring(0, 2);
            var values = dir_key.Substring(2).Split(",");
            dict[key] = values.Select(x => x + "A").ToArray();
            var rev_key = new string(key.Reverse().ToArray());
            var rev_values = values.Select(x => new string(x.Reverse().Select(Util.RotateClockwise).Select(Util.RotateClockwise).ToArray()));
            dict[rev_key] = rev_values.Select(x => x + "A").ToArray();
        }
        foreach (var c in text.Distinct()) dict.Add(new string([c, c]), ["AA"]);
        return dict;
    }

    private static Dictionary<string, string> ConstructTransitionDictionary(string text)
    {
        var dict = new Dictionary<string, string>();
        foreach (var dir_key in text.Split(Environment.NewLine))
        {
            var key = dir_key[..2];
            var value = dir_key[2..];
            dict[key] = value + "A";
            var rev_key = new string(key.Reverse().ToArray());
            var rev_value = new string(value.Reverse().Select(Util.RotateClockwise).Select(Util.RotateClockwise).ToArray());
            dict[rev_key] = rev_value + "A";
        }
        foreach (var c in text.Distinct()) dict.Add(new string([c, c]), "AA");
        return dict;
    }

    public const string numkeys_simple =
    @"01^<
02^
03^>
04^^<
05^^
06^^>
07^^^<
08^^^
09^^^>
0A>
12>
13>>
14^
15^>
16^>>
17^^
18^^>
19^^>>
1A>>v
23>
24^<
25^
26^>
27^^<
28^^
29^^>
2A>v
34^<<
35^<
36^
37^^<<
38^^<
39^^
3Av
45>
46>>
47^
48^>
49^>>
4A>>vv
56>
57^<
58^
59^>
5A>vv
67^<<
68^<
69^
6Avv
78>
79>>
7A>>vvv
89>
8A>vvv
9Avvv";

    public const string numkeys =
    @"01^<
02^
03^>,>^
04^^<,^<^
05^^
06^^>,^>^,>^^
07^^^<,^^<^,^<^^
08^^^
09^^^>,^^>^,^>^^,>^^^
0A>
12>
13>>
14^
15^>,>^
16^>>,>^>,>>^
17^^
18^^>,^>^,>^^
19^^>>,^>^>,^>>^,>^^>,>^>^,>>^^
1A>>v,>v>
23>
24^<,<^
25^
26^>,>^
27^^<,^<^,<^^
28^^
29^^>,^>^,>^^
2A>v,v>
34^<<,<^<,<<^
35^<,<^
36^
37<<^^,<^<^,<^^<,^<<^,^<^<,^^<<
38^^<,^<^,<^^
39^^
3Av
45>
46>>
47^
48^>,>^
49^>>,>^>,>>^
4A>>vv,>v>v,>vv>,v>>v,v>v>
56>
57^<,<^
58^
59^>,>^
5A>vv,v>v,vv>
67^<<,<^<,<<^
68^<,<^
69^
6Avv
78>
79>>
7A>>vvv,>v>vv,>vv>v,>vvv>,v>>vv,v>v>v,v>vv>,vv>>v,vv>v>
89>
8A>vvv,v>vv,vv>v,vvv>
9Avvv";

    private static string dirkeys =
    @"^A>
^<v<
^vv
^>v>
A<v<<
Av<v
A>v
<v>
<>>>
v>>";

    public const string dirkeys_all =
    @"^A>
^<v<
^vv
^>v>,>v
A<<v<,v<<
Avv<,<v
A>v
<v>
<>>>
v>>";

}

public class TransitionCount(string transition, long count)
{
    public string Transition { get; } = transition;
    public long Count { get; set; } = count;
}

public static class XDay21Extensions
{
    public static void Print(this IEnumerable<TransitionCount> transitionCounts, string prefix = "")
    {
        if (prefix != "") Console.Write(prefix + ": ");
        foreach (var tc in transitionCounts.OrderBy(x => x.Transition))
        {
            Console.Write("  " + tc.Transition + ":" + tc.Count);
        }
        Console.WriteLine(" - " + transitionCounts.Count() + " - " + +transitionCounts.Where(x => x.Transition != "" && x.Transition[0] != x.Transition[1]).Sum(x => x.Count) + " - " + transitionCounts.Sum(x => x.Count));
    }
    public static IEnumerable<TransitionCount> ToTransitionCounts(this string text)
    {
        var text2 = text + "A";
        return Enumerable
            .Range(0, text2.Length - 1)
            .Select(x => new TransitionCount(text2.Substring(x, 2), 1));
    }

    public static IEnumerable<TransitionCount> Merge(this IEnumerable<TransitionCount> items)
    {
        return items.GroupBy(x => x.Transition).Select(x => new TransitionCount(x.Key, x.Sum(y => y.Count)));
    }

    public static IEnumerable<string> ToTransitions(this string text)
    {
        var text2 = text;
        if (text2.StartsWith("A") == false) text2 = "A" + text2;
        if (text2.EndsWith("A") == false) text2 = text2 + "A";

        return Enumerable
            .Range(0, text2.Length - 1)
            .Select(x => text2.Substring(x, 2));
    }
}
