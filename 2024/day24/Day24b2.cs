

public class Day24b2 : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var gates = input.SkipWhile(x => x != "").Skip(1).Select(Gate.Construct).ToList();

        void rename(string from, string to)
        {
            foreach (var gate in gates)
            {
                gate.Rename(from, to);
            }
        }

        var gates_by_out = gates.ToDictionary(x => x.Name);
        var i = 44;

        Gate Find(string op, string in1, string in2)
        {
            return gates.FirstOrDefault(x => x.Op == op && ((x.In1 == in1 && x.In2 == in2) || (x.In1 == in2 && x.In2 == in1)));
        }

        // var z = gates_by_out[$"z{i:00}"];
        var z = gates_by_out["z44"];
        while (true)
        {
            var a = Find("XOR", $"x{i:00}", $"y{i:00}");
            var b = Find("AND", $"x{i:00}", $"y{i:00}");

        }

    }
}

public class Gate2
{
    public string Name;
    public string Op;
    public string In1;
    public string In2;
    public override string ToString() => $"{In1} {Op} {In2} -> {Name}";
    public void Rename(string from, string to)
    {
        if (In1 == from) In1 = to;
        if (In2 == from) In2 = to;
        if (Name == from) Name = to;
        Fix();
    }
    public static Gate2 Construct(string line)
    {

        var split1 = line.Split(" -> ");
        var split2 = split1[0].Split(" ");
        var gate = new Gate2();
        gate.Name = split1[1];
        gate.Op = split2[1];
        gate.In1 = split2[0];
        gate.In2 = split2[2];
        gate.Fix();
        return gate;
    }

    private void Fix()
    {
        if (In1.CompareTo(In2) > 0)
        {
            (In2, In1) = (In1, In2);
        }
    }
}