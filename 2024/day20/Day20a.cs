using System.ComponentModel;
using System.Numerics;

public class Day20a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        int uncheeted_time = 0;

        var track = Grid.Construct(input, 1, '#');
        var start = track.Start;
        var end = track.End;

        track.Print();

        // var uncheat_time = Time(new Pos(0,0));
        var orig_costs = new int[track.Width, track.Height];
        for (int y = 0; y < track.Height; y++)
            for (int x = 0; x < track.Width; x++)
                orig_costs[x, y] = 99999;



        var times = new List<int>();

        void Step(Grid grid, Pos pos, int[,] costs, int cost, bool cheetUsed)
        {
            // if (grid[pos] == 'O') return;
            if (grid[pos] == '#')
            {
                if (!cheetUsed)
                {
                    var cheet_grid = grid.Clone();
                    cheet_grid[pos] = '.';
                    var cheet_costs = (int[,])costs.Clone();
                    Step(cheet_grid, pos, cheet_costs, cost, true);
                    var cheet_cost = cheet_costs[end.X, end.Y];
                    // if (cheet_cost == 64)
                    // {
                    //     Console.WriteLine();
                    //     grid.Print(true);
                    //     Console.WriteLine();
                    //     cheet_grid.Print(true);
                    //     Console.WriteLine();
                    //     PrintCost(cheet_costs);
                    // }
                    times.Add(cheet_cost);
                }
                return;
            }
            var current_cost = costs[pos.X, pos.Y];
            if (current_cost > cost)
            {
                // if (pos == new Pos(11, 8))
                // {
                //     Console.WriteLine("Here!");
                //     grid.Print(true);
                // }
                // grid[pos] = 'O';
                costs[pos.X, pos.Y] = cost;
                foreach (var delta in Pos.DeltaClockwize4Steps)
                    Step(grid, pos + delta, costs, cost + 1, cheetUsed);
            }
            // if (pos == end) return;
        }


        Step(track, start, orig_costs, 0, false);

        PrintCost(orig_costs);

        uncheeted_time = orig_costs[end.X, end.Y];

        track.Print();

        foreach (var group in times.Where(x => x <= uncheeted_time - 100).GroupBy(x => uncheeted_time - x).OrderBy(x => x.Key))
        {
            Console.WriteLine($"{group.Key}: {group.Count()} st");
        }

        var result = times.Where(x => x <= uncheeted_time - 100).GroupBy(x => uncheeted_time - x).Select(x=>x.Count()).Sum();

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

