using System.Numerics;

public class Day16b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var grid = Grid.Construct(input);

        var v_costs = new int[grid.Width, grid.Height];
        var h_costs = new int[grid.Width, grid.Height];

        void ProcessGrid(Pos pos, char direction, int cost)
        {
            var costs = direction == '>' || direction == '<' ? h_costs : v_costs;
            if (grid[pos] == '#') return;
            var existing_cost = costs[pos.X, pos.Y];
            if (existing_cost == 0 || existing_cost > cost)
            {
                costs[pos.X, pos.Y] = cost;
                if (grid[pos] == 'E') return;
                var new_direction = Util.RotateClockwise(direction);
                ProcessGrid(pos + Grid.GetMoveDelta(new_direction), new_direction, cost + 1001);
                new_direction = Util.RotateCounterClockwise(direction);
                ProcessGrid(pos + Grid.GetMoveDelta(new_direction), new_direction, cost + 1001);

                ProcessGrid(pos + Grid.GetMoveDelta(direction), direction, cost + 1);
            }
        }

        void BackTrack(Pos pos, int lastCost)
        {
            if (grid[pos] != '.' && grid[pos] != 'E') return;

            var existing_costs = new int[] { v_costs[pos.X, pos.Y], h_costs[pos.X, pos.Y] }
                .Where(x => x != 0 && x < lastCost);
            if (existing_costs.Any())
            {
                var existing_cost = existing_costs.OrderByDescending(x => x).First();
                grid[pos] = 'O';
                BackTrack(pos + Grid.GetMoveDelta('>'), existing_cost);
                BackTrack(pos + Grid.GetMoveDelta('^'), existing_cost);
                BackTrack(pos + Grid.GetMoveDelta('<'), existing_cost);
                BackTrack(pos + Grid.GetMoveDelta('v'), existing_cost);
            }
        }


        var start = grid.FindChars('S').First();
        ProcessGrid(start, '>', 0);
        var end = grid.FindChars('E').First();
        print(v_costs[end.X, end.Y]);
        print(h_costs[end.X, end.Y]);

        BackTrack(end, Math.Min(v_costs[end.X, end.Y], h_costs[end.X, end.Y])+1);

        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                Console.Write($"{v_costs[x, y],5}/{h_costs[x, y],5}");
            }
            Console.WriteLine();
        }

        grid.Print();

        // Lösningen gör 2 st avstickare av misstag som gör att den räknar 2 för mycket

        int result = grid.FindChars('O').Count() + 1;
        print(result);
    }

}

