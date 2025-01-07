
public class Day23a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var computers = new HashSet<string>();
        var connections = new List<(string, string)>();

        foreach (var line in input)
        {
            var nodes = line.Split('-');
            connections.Add((nodes[0], nodes[1]));
            connections.Add((nodes[1], nodes[0]));
        }

        var connections_by_computer = connections.ToLookup(x => x.Item1, x => x.Item2);

        var treesomes = new List<(string, string, string)>();

        foreach (var computer1 in connections_by_computer)
        {
            foreach (var computer2 in computer1)
            {
                foreach (var computer3 in connections_by_computer[computer2])
                {
                    if (connections_by_computer[computer3].Contains(computer1.Key))
                    {
                        treesomes.Add((computer1.Key, computer2, computer3));
                    }
                }
            }
        }

        int result = treesomes.Where(x => x.Item1.StartsWith("t") || x.Item2.StartsWith("t") || x.Item3.StartsWith("t")).Count() / 3 / 2;
        print(result);
    }

}

