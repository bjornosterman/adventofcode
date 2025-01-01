using System.ComponentModel;
using System.Numerics;
using System.Reflection;

public class Day20b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        int uncheeted_time = 0;

        int cheet_jump = 20;
        int margin = cheet_jump + 2;

        var track = Grid.Construct(input, margin, '#');
        var start = track.Start;
        var end = track.End;

        track.Print();

        var costs = new int[track.Width, track.Height];
        for (int y = 0; y < track.Height; y++)
            for (int x = 0; x < track.Width; x++)
                costs[x, y] = 99999;


        void Step(Pos pos, int cost)
        {
            if (track[pos] == '#') return;

            var current_cost = costs[pos.X, pos.Y];
            if (current_cost > cost)
            {
                costs[pos.X, pos.Y] = cost;
                foreach (var delta in Pos.DeltaClockwize4Steps)
                    Step(pos + delta, cost + 1);
            }
        }

        var deltas = new List<Pos>();
        for (int i = -cheet_jump; i <= cheet_jump; i++)
            for (int j = -cheet_jump; j <= cheet_jump; j++)
                if (Math.Abs(i) + Math.Abs(j) <= cheet_jump)
                    deltas.Add(new Pos(j, i));
        deltas = deltas.Distinct().ToList();
        deltas.Remove(new Pos(0, 0));


        Step(start, 0);

        var cheets = new List<int>();

        foreach (var pos in track.Positions(margin))
        {
            var cost = costs[pos.X, pos.Y];
            if (cost == 99999) continue;
            foreach (var delta in deltas)
            {
                var delta_pos = pos + delta;
                var jump_cost = costs[delta_pos.X, delta_pos.Y];
                if (jump_cost == 99999) continue;
                var cheet = jump_cost - cost - delta.Length;
                if (cheet > 0) cheets.Add(cheet);
            }
        }


        PrintCost(costs);

        uncheeted_time = costs[end.X, end.Y];

        track.Print();

        foreach (var group in cheets.GroupBy(x => uncheeted_time - x).OrderBy(x => x.Key))
        {
            Console.WriteLine($"{group.Key}: {group.Count()} st");
        }

        var result = cheets.Where(x => x >= 100).Count();

        print(result);
    }
    public void PrintCost(int[,] costs)
    {
        for (int y = 0; y < costs.GetLength(1); y++)
        {
            for (int x = 0; x < costs.GetLength(0); x++)
            {
                var c = costs[x, y];
                Console.Write(c == 99999 ? "    " : $"{c,4}");
            }
            Console.WriteLine();
        }
    }
}

