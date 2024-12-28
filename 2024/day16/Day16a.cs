using System.Numerics;

public class Day16a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var grid = Grid.Construct(input);

        var costs = new int[grid.Width, grid.Height];

        void ProcessGrid(Pos pos, char direction, int cost)
        {
            if (grid[pos] == '#') return;
            var existing_cost = costs[pos.X, pos.Y];
            if (existing_cost == 0 || existing_cost > cost)
            {
                costs[pos.X, pos.Y] = cost;
                if (grid[pos] == 'E') return;
                ProcessGrid(pos + Grid.GetMoveDelta(direction), direction, cost + 1);
                var new_direction = Util.RotateClockwise(direction);
                ProcessGrid(pos + Grid.GetMoveDelta(new_direction), new_direction, cost + 1001);
                new_direction = Util.RotateCounterClockwise(direction);
                ProcessGrid(pos + Grid.GetMoveDelta(new_direction), new_direction, cost + 1001);
            }
        }

        var start = grid.FindChars('S').First();
        ProcessGrid(start, '>', 0);
        var end = grid.FindChars('E').First();
        int result = costs[end.X, end.Y];
        print(result);
    }

}

