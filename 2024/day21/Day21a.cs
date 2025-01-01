public class Day21a : Day
{
    public Day21a()
    {
        DirKeyStepPaths = ToMoveDict2(dirkeys);
        NumKeyStepPaths = ToMoveDict2(numkeys);
    }
    public override void Run()
    {
        var sample = true;
        var input = sample ? samplePuzzleInput : puzzleInput;
        int result = 0;


        result = 0;

        foreach (var line in input)
        {
            var alts = DirKeyPresser(DirKeyPresser(NumKeyPresser(line)));
            var min = alts.Select(x => x.Length).Min();
            var code_int = int.Parse(line[..3]);
            print($"{min} * {code_int} = {min * code_int}");
            result += min * code_int;
        }

        print(result);
    }

    public IEnumerable<string> ToSteps(string text)
    {
        return Enumerable
            .Range(0, text.Length - 1)
            .Select(x => text.Substring(x, 2));
    }

    public IEnumerable<string> DirKeyPresser(IEnumerable<string> inner)
    {
        foreach (var line in inner)
        {
            IEnumerable<string> combos = [""];
            foreach (var step in ToSteps("A" + line))
            {
                combos = combos.SelectMany(x => DirKeyStepPaths[step], (x, y) => x + y);
            }
            combos = combos.ToList();
            foreach (var combo in combos) yield return combo;
        }
    }

    public Dictionary<string, string[]> DirKeyStepPaths = [];
    public Dictionary<string, string[]> NumKeyStepPaths = [];

    public IEnumerable<string> NumKeyPresser(string keys)
    {
        IEnumerable<string> combos = [""];
        foreach (var step in ToSteps("A" + keys))
        {
            combos = combos.SelectMany(x => NumKeyStepPaths[step], (x, y) => x + y).ToList();
        }
        return combos.ToList();
    }

    Dictionary<string, string> ToMoveDict(string text)
    {
        var dict = new Dictionary<string, string>();
        foreach (var dir_key in text.Split(Environment.NewLine))
        {
            var key = dir_key.Substring(0, 2);
            var value = dir_key.Substring(2);
            dict[key] = value;
            var rev_key = new string(key.Reverse().ToArray());
            var rev_value = new string(value.Reverse().Select(Util.RotateClockwise).Select(Util.RotateClockwise).ToArray());
            dict[rev_key] = rev_value;
        }
        return dict;
    }

    Dictionary<string, string[]> ToMoveDict2(string text)
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
        foreach (var c in text.Distinct()) dict.Add(new string([c, c]), ["A"]);
        return dict;
    }

    private string numkeys_simple =
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

    private string numkeys =
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

    private string dirkeys =
    @"^A>
^<v<
^vv
^>v>
A<v<<
Avv<
A>v
<v>
<>>>
v>>";

    private string dirkeys_all =
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
