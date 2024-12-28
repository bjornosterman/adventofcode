using System.Numerics;
using System.Runtime.InteropServices;

public class Day10a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var grid = Util.ToCharGrid(input, 1, ' ');

        int result = 0;
        int result2 = 0;

        foreach (var pos in grid.EnumeratePositions())
        {
            var trails = FindTrails(grid, pos, '0');
            var trail_ends = trails.Distinct(); // Select(x => x.Y * 1000 + x.X).();
            result += trail_ends.Count();
            result2 += trails.Count();
            if (trail_ends.Any()) print($"{pos}: {trail_ends.Count()}");
        }

        print(result);
        print(result2);
    }

    private IEnumerable<Pos> FindTrails(char[,] grid, Pos pos, char searchChar)
    {
        if (grid[pos.X, pos.Y] != searchChar) yield break;
        if (searchChar == '9') {
            // print(pos);
            yield return pos;
        }
        else
        {
            var c2 = (char)(searchChar + 1);
            foreach (var p in FindTrails(grid, new Pos(pos.X + 1, pos.Y), c2)) yield return p;
            foreach (var p in FindTrails(grid, new Pos(pos.X, pos.Y + 1), c2)) yield return p;
            foreach (var p in FindTrails(grid, new Pos(pos.X - 1, pos.Y), c2)) yield return p;
            foreach (var p in FindTrails(grid, new Pos(pos.X, pos.Y - 1), c2)) yield return p;
        }
    }

}

