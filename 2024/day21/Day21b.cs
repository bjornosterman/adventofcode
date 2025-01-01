using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text.RegularExpressions;

public class Day21b : Day
{

    public override void Run()
    {
        checked
        {
            var sample = true;
            var input = sample ? samplePuzzleInput : puzzleInput;
            long result = 0;

            this.DirKeyStepPaths = ToMoveDict3(dirkeys);
            this.NumKeyStepPaths = ToMoveDict2(numkeys);

            var day21a = new Day21a();

            result = 0;

            var code = "029A";

            var text0a = day21a.NumKeyPresser(code).First();
            print(text0a);

            var text0b = NumKeyPresser2(code).First();
            print(text0b);

            var tcs0a = text0a.ToTransitionCounts().Merge();
            tcs0a.Print("text0a");

            var tcs0b = text0b.ToTransitionCounts().Merge();
            tcs0b.Print("text0b");
            print("#---#---#---#---#---#---#---#---#---#---#---#---#");

            var text1a = day21a.DirKeyPresser([text0a]).First();
            print("text1a: " + text1a);
            var tcs1a = text1a.ToTransitionCounts().Merge();
            tcs1a.Print("text1a");
            var tcs1b = DirKeyPresser(tcs0b).ToList();
            tcs1b.Print("text1b");

            print("#---#---#---#---#---#---#---#---#---#---#---#---#");


            var text2a = day21a.DirKeyPresser([text1a]).First();
            print("text2a: " + text1a);
            var tcs2a = text2a.ToTransitionCounts().Merge();
            tcs2a.Print("text2a");
            var tcs2b = DirKeyPresser(tcs1b).ToList();
            tcs2b.Print("text2b");


            // var tcs1b = DirKeyPresser(tcs0b.Concat([new TransitionCount("A<", 1)])).ToList();
            // var test = ToSteps(DirKeyStepPaths["A<"]).First();
            // var test2 = tcs1b.First(x=>x.Transition == test);
            // test2.Count--;
            // var tcs1b = DirKeyPresser(tcs0b);

            // var text1 = "<A^A>^^AvvvA";
            // text1.ToTransitionCounts().Merge().Print("corr1");
            // "v<<A>>^A<A>AvA<^AA>A<vAAA>^A".ToTransitionCounts().Merge().Print("corr2");

            var number_of_dirkey_pressers = 2;

            var line2 = "029A";
            var alts2 = NumKeyPresser2(line2);
            foreach (var alt in alts2)
            {
                var presser = NumKeyPresser(alt);
                for (int i = 0; i < number_of_dirkey_pressers; i++)
                {
                    presser = DirKeyPresser(presser);
                }
                print(line2 + ": " + alt + "=" + presser.Sum(x => x.Count));
            }



            foreach (var line in input)
            {
                var alts = NumKeyPresser2(line);
                var counts = new List<long>();
                foreach (var alt in alts)
                {
                    var presser = NumKeyPresser(alt);
                    for (int i = 0; i < number_of_dirkey_pressers; i++)
                    {
                        presser = DirKeyPresser(presser);
                    }
                    var count = presser.Sum(x => x.Count);
                    print(line + ": " + alt + "=" + count);
                    counts.Add(count);
                }
                var min = counts.Min();
                var code_int = int.Parse(line[..3]);
                print($"{min} * {code_int} = {min * code_int}");
                result += min * code_int;
            }

            // foreach (var line in new[] { "<A^A^^>AvvvA" })
            // {
            //     var alts = NumKeyPresser2(line);
            //     foreach (var alt in alts)
            //     {
            //         var presser = NumKeyPresser(alt);
            //         print(line + ": " + alt + ": " + presser.Sum(x => x.Count));
            //         for (int i = 0; i < number_of_dirkey_pressers; i++)
            //         {
            //             presser = DirKeyPresser(presser);
            //         }
            //         // print(line + ": " + alt + ": " + presser.Where(x=>x.Transition[0] != x.Transition[1]).Sum(x => x.Count));
            //         print(line + ": " + alt + "=" + presser.Sum(x => x.Count));
            //         // print(line + ": " + alt + ": " + presser.Where(x=>x.Transition != "AA").Sum(x => x.Count));
            //     }

            //     // var min = alts.Select(x => x.Length).Min();
            //     // var code_int = int.Parse(line[..3]);
            //     // print($"{min} * {code_int} = {min * code_int}");
            //     // result += min * code_int;
            // }

            print(result);

            // Felaktig, för hög: 337696870703934
        }
    }

    private IEnumerable<string> ToSteps(string text)
    {
        // if (text == "") return [""];
        // if (text == "AA") return ["AA"];
        // var text2 = "A" + text;
        var text2 = text;
        if (text2.StartsWith("A") == false) text2 = "A" + text2;
        if (text2.EndsWith("A") == false) text2 = text2 + "A";

        return Enumerable
            .Range(0, text2.Length - 1)
            .Select(x => text2.Substring(x, 2));
    }

    IEnumerable<TransitionCount> DirKeyPresser(IEnumerable<TransitionCount> inner)
    {
        // Console.Write("inner: ");
        // inner.Print();

        var outer = inner.SelectMany(tc => ToSteps(DirKeyStepPaths[tc.Transition]).Select(x => new TransitionCount(x, tc.Count)));
        var returns = outer.Merge();
        // var returns = outer.GroupBy(tc => tc.Transition).Select(tcs => new TransitionCount(tcs.Key, tcs.Sum(x => x.Count)));
        // Console.Write("outer: ");
        // returns.Print();
        return returns;
    }

    Dictionary<string, string> DirKeyStepPaths = [];
    Dictionary<string, string[]> NumKeyStepPaths = [];

    IEnumerable<string> NumKeyPresser2(string keys)
    {
        IEnumerable<string> combos = ["A"];
        foreach (var step in ToSteps(keys)) //.TrimEnd('A')))
        {
            combos = combos.SelectMany(x => NumKeyStepPaths[step], (x, y) => x + y).ToList();
        }
        return combos.ToList();
    }

    IEnumerable<TransitionCount> NumKeyPresser(string keys)
    {
        return ToSteps(keys.Trim('A')).Select(x => new TransitionCount(x, 1)).Merge();
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
        foreach (var c in text.Distinct()) dict.Add(new string([c, c]), ["AA"]);
        return dict;
    }

    Dictionary<string, string> ToMoveDict3(string text)
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

}
