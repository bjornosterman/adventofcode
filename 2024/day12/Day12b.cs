using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Day12b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        int result = 0;
        var grid = Util.ToCharGrid(input, 1);

        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
            {
                var p = grid[x, y];
                if (p != ' ')
                {
                    var horizontals = new List<int>();
                    var verticals = new List<int>();
                    var (plots, fences) = Eat(grid, new Pos(x, y), horizontals, verticals);
                    var segments = 2;
                    horizontals.Sort();
                    for (int i = 0; i < horizontals.Count() - 1; i++)
                        if (horizontals[i] + 1 != horizontals[i + 1]) segments++;
                    verticals.Sort();
                    for (int i = 0; i < verticals.Count() - 1; i++)
                        if (verticals[i] + 1 != verticals[i + 1]) segments++;
                    Util.Change(grid, '.', ' ');
                    print($"{p}: {plots} - {fences} - {segments}");
                    // result += plots * fences;
                    result += plots * segments;
                }
            }

        print(result);
    }

    private (int, int) Eat(char[,] grid, Pos pos, List<int> horizontals, List<int> verticals)
    {
        var plant = grid[pos.X, pos.Y];
        grid[pos.X, pos.Y] = '.';
        var fences = 0;
        var plots = 1;
        foreach (var p in Pos.DeltaClockwize4Steps)
        {
            var next_plant = grid[p.X, p.Y];
            if (next_plant == '.') continue;
            if (next_plant != plant)
            {
                fences++;
                if (p.X != pos.X)
                {
                    verticals.Add(pos.X * 1000 + pos.Y + (p.X > pos.X ? 1000000 : 0));
                }
                else
                {
                    horizontals.Add(pos.Y * 1000 + pos.X + (p.Y > pos.Y ? 1000000 : 0));
                }
            }
            else
            {
                var (np, nf) = Eat(grid, p, horizontals, verticals);
                fences += nf;
                plots += np;
            }
        }
        return (plots, fences);
    }
}

