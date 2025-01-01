using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Day12a : Day
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
                    var (plots, fences) = Eat(grid, new Pos(x, y));
                    Util.Change(grid, '.', ' ');
                    print($"{p}: {plots} - {fences}");
                    result += plots * fences;
                }
            }

        print(result);
    }

    private (int, int) Eat(char[,] grid, Pos pos)
    {
        var plant = grid[pos.X, pos.Y];
        grid[pos.X, pos.Y] = '.';
        var ps = new List<Pos>() { pos.Right, pos.Down, pos.Left, pos.Up };
        var fences = 0;
        var plots = 1;
        foreach (var p in ps)
        {
            var next_plant = grid[p.X, p.Y];
            if (next_plant == '.') continue;
            if (next_plant != plant) fences++;
            else
            {
                var (np, nf) = Eat(grid, p);
                fences += nf;
                plots += np;
            }
        }
        return (plots, fences);
    }
}

