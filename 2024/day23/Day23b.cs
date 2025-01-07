
public class Day23b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var connections = new List<(string, string)>();

        foreach (var line in input)
        {
            var nodes = line.Split('-');
            connections.Add((nodes[0], nodes[1]));
            connections.Add((nodes[1], nodes[0]));
        }

        var connections_by_computer = connections.ToLookup(x => x.Item1, x => x.Item2);

        var triplets = new List<(string, string, string)>();

        // foreach (var conn in connections_by_computer.OrderBy(x => x.Count()))
        //     print($"{conn.Key,4}: {conn.Count()}");
        // Varje dator är kopplade mot 13 andra datorer
        var computers = connections_by_computer.Select(x => x.Key).ToList();

        foreach (var computer1 in computers)
        {
            foreach (var computer2 in connections_by_computer[computer1].Where(x => x.CompareTo(computer1) > 0))
            {
                foreach (var computer3 in connections_by_computer[computer2].Where(x => x.CompareTo(computer2) > 0))
                {
                    if (connections_by_computer[computer3].Contains(computer1))
                    {
                        triplets.Add((computer1, computer2, computer3));
                    }
                }
            }
        }

        var quadlets = new List<(string, string, string, string)>();
        foreach (var triplet in triplets)
        {
            foreach (var computer in computers.Where(x => x.CompareTo(triplet.Item3) > 0))
            {
                if (
                    connections_by_computer[triplet.Item1].Contains(computer) &&
                connections_by_computer[triplet.Item2].Contains(computer) &&
                connections_by_computer[triplet.Item3].Contains(computer))
                {
                    quadlets.Add((triplet.Item1, triplet.Item2, triplet.Item3, computer));
                }
            }
        }

        var pentlets = new List<(string, string, string, string, string)>();
        foreach (var quadlet in quadlets)
        {
            foreach (var computer in computers.Where(x => x.CompareTo(quadlet.Item4) > 0))
            {
                if (
                    connections_by_computer[quadlet.Item1].Contains(computer) &&
                connections_by_computer[quadlet.Item2].Contains(computer) &&
                connections_by_computer[quadlet.Item3].Contains(computer) &&
                connections_by_computer[quadlet.Item4].Contains(computer))
                {
                    pentlets.Add((quadlet.Item1, quadlet.Item2, quadlet.Item3, quadlet.Item4, computer));
                }
            }
        }

        var sixlets = new List<(string, string, string, string, string, string)>();
        foreach (var c1 in pentlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item5) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2))
                {
                    sixlets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c2));
                }
            }
        }

        var sevenlets = new List<(string, string, string, string, string, string, string)>();
        foreach (var c1 in sixlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item6) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2))
                {
                    sevenlets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c2));
                }
            }
        }

        var eightlets = new List<(string, string, string, string, string, string, string, string)>();
        foreach (var c1 in sevenlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item7) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2))
                {
                    eightlets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c2));
                }
            }
        }

        var ninelets = new List<(string, string, string, string, string, string, string, string, string)>();
        foreach (var c1 in eightlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item8) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2) &&
                connections_by_computer[c1.Item8].Contains(c2))
                {
                    ninelets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c1.Item8, c2));
                }
            }
        }

        var tenlets = new List<(string, string, string, string, string, string, string, string, string, string)>();
        foreach (var c1 in ninelets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item9) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2) &&
                connections_by_computer[c1.Item8].Contains(c2) &&
                connections_by_computer[c1.Item9].Contains(c2))
                {
                    tenlets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c1.Item8, c1.Item9, c2));
                }
            }
        }

        var elevenlets = new List<(string, string, string, string, string, string, string, string, string, string, string)>();
        foreach (var c1 in tenlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item10) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2) &&
                connections_by_computer[c1.Item8].Contains(c2) &&
                connections_by_computer[c1.Item9].Contains(c2) &&
                connections_by_computer[c1.Item10].Contains(c2))
                {
                    elevenlets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c1.Item8, c1.Item9, c1.Item10, c2));
                }
            }
        }

        var twelvelets = new List<(string, string, string, string, string, string, string, string, string, string, string, string)>();
        foreach (var c1 in elevenlets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item11) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2) &&
                connections_by_computer[c1.Item8].Contains(c2) &&
                connections_by_computer[c1.Item9].Contains(c2) &&
                connections_by_computer[c1.Item10].Contains(c2) &&
                connections_by_computer[c1.Item11].Contains(c2))
                {
                    twelvelets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c1.Item8, c1.Item9, c1.Item10, c1.Item11, c2));
                }
            }
        }

        var alllets = new List<(string, string, string, string, string, string, string, string, string, string, string, string, string)>();
        foreach (var c1 in twelvelets)
        {
            foreach (var c2 in computers.Where(x => x.CompareTo(c1.Item12) > 0))
            {
                if (
                    connections_by_computer[c1.Item1].Contains(c2) &&
                connections_by_computer[c1.Item2].Contains(c2) &&
                connections_by_computer[c1.Item3].Contains(c2) &&
                connections_by_computer[c1.Item4].Contains(c2) &&
                connections_by_computer[c1.Item5].Contains(c2) &&
                connections_by_computer[c1.Item6].Contains(c2) &&
                connections_by_computer[c1.Item7].Contains(c2) &&
                connections_by_computer[c1.Item8].Contains(c2) &&
                connections_by_computer[c1.Item9].Contains(c2) &&
                connections_by_computer[c1.Item10].Contains(c2) &&
                connections_by_computer[c1.Item11].Contains(c2) &&
                connections_by_computer[c1.Item12].Contains(c2))
                {
                    alllets.Add((c1.Item1, c1.Item2, c1.Item3, c1.Item4, c1.Item5, c1.Item6, c1.Item7, c1.Item8, c1.Item9, c1.Item10, c1.Item11, c1.Item12, c2));
                }
            }
        }

        print("Number of connections / computer: " + 13);
        print("Number of computers: " + connections_by_computer.Count());
        print("Number of connections: " + input.Count());
        print("Number of triplets" + triplets.Count());
        print("Number of triplets starting with 't': " + triplets.Where(x => x.Item1.StartsWith("t") || x.Item2.StartsWith("t") || x.Item3.StartsWith("t")).Count());
        print("Number of computers in triplets: " + triplets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3 }).Distinct().Count());
        print("Number of quadlets: " + quadlets.Count());
        print("Number of computers in quadlets: " + quadlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4 }).Distinct().Count());
        print("Number of pentlets: " + pentlets.Count());
        print("Number of computers in pentlets: " + pentlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5 }).Distinct().Count());
        print("Number of sixlets: " + sixlets.Count());
        print("Number of computers in sixlets: " + sixlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6 }).Distinct().Count());
        print("Number of sevenlets: " + sevenlets.Count());
        print("Number of computers in sevenlets: " + sevenlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7 }).Distinct().Count());
        print("Number of eightlets: " + eightlets.Count());
        print("Number of computers in eightlets   : " + eightlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8 }).Distinct().Count());
        print("Number of ninelets: " + ninelets.Count());
        print("Number of computers in ninelets   : " + ninelets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8, x.Item9 }).Distinct().Count());
        print("Number of tenlets: " + tenlets.Count());
        print("Number of computers in tenlets   : " + tenlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8, x.Item9, x.Item10 }).Distinct().Count());
        print("Number of elevenlets: " + elevenlets.Count());
        print("Number of computers in elevenlets   : " + elevenlets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8, x.Item9, x.Item10, x.Item11 }).Distinct().Count());
        print("Number of twelvelets: " + twelvelets.Count());
        print("Number of computers in twelvelets   : " + twelvelets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8, x.Item9, x.Item10, x.Item11, x.Item12 }).Distinct().Count());
        print("Number of alllets: " + alllets.Count());
        print("Number of computers in alllets: " + alllets.SelectMany(x => new string[] { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5, x.Item6, x.Item7, x.Item8, x.Item9, x.Item10, x.Item11, x.Item12, x.Item13 }).Distinct().Count());
        print(alllets[0]);
    }

}

