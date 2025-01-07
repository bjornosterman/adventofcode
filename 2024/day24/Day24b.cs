

public class Day24b : Day
{
    public override void Run()
    {
        // z35 <--> jbp
        // z15 <--> jgc
        // vdr <--> 

        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var gates = input.SkipWhile(x => x != "").Skip(1).Select(Gate.Construct).ToList();
        void flip(string gateName1, string gateName2)
        {
            var gate1 =gates.First(x => x.Name == gateName1);
            var gate2 =gates.First(x => x.Name == gateName2);
            (gate2.Name, gate1.Name) = (gate1.Name, gate2.Name);
        }

        flip("z35","jbp");
        flip("z15","jgc");
        flip("gvw","qjb");
        flip("drg","z22");
        // "drg,gvw,jbp,jgc,qjb,z15,z22,z35"

        void rename(string from, string to)
        {
            print($"renaming: {from} -> {to}");
            foreach (var gate in gates)
            {
                gate.Rename(from, to);
            }
        }

        rename("jjn", "d44");
        // rename("vdr", "c08");

        for (int i = 1; i <= 44; i++)
        {
            var gate_a = gates.FirstOrDefault(x => x.In1 == $"x{i:00}" && x.In2 == $"y{i:00}" && x.Op == "XOR");
            if (gate_a == null)
            {
                print($"Missing a{i:00}");
            }
            else
            {
                rename(gate_a.Name, $"a{i:00}");
            }
        }

        for (int i = 0; i <= 44; i++)
        {
            var gate_a = gates.FirstOrDefault(x => x.In1 == $"x{i:00}" && x.In2 == $"y{i:00}" && x.Op == "AND");
            if (gate_a == null)
            {
                print($"Missing b{i:00}");
            }
            else
            {
                rename(gate_a.Name, $"b{i:00}");
            }
        }

        for (int i = 1; i < 44; i++)
        {
            var gate_a = gates.FirstOrDefault(x => (x.In1 == $"b{i:00}" || x.In2 == $"b{i:00}") && x.Op == "OR");
            if (gate_a == null)
            {
                print($"Missing c{i:00}");
            }
            else
            {
                rename(gate_a.Name, $"c{i:00}");
                rename(gate_a.In1 == $"b{i:00}" ? gate_a.In2 : gate_a.In1, $"d{i:00}");
            }
        }

        foreach (var gate in gates.OrderBy(x => x.Name)) print(gate);



    }

}

public class Gate
{
    public string Op { get; set; }
    public string In1 { get; set; }
    public string In2 { get; set; }

    public string Name { get; set; }

    public override string ToString() => $"{In1} {Op} {In2} -> {Name}";
    public void Rename(string from, string to)
    {
        if (In1 == from) In1 = to;
        if (In2 == from) In2 = to;
        if (Name == from) Name = to;
        Fix();
    }
    public static Gate Construct(string line)
    {

        var split1 = line.Split(" -> ");
        var split2 = split1[0].Split(" ");
        var gate = new Gate();
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