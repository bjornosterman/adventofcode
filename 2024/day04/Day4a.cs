using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class Day04a : Day
{
    public override void Run()
    {
        var input = puzzleInput;

        var grid = Grid.Construct(input, 3);

        int matches = 0;

        bool checkXmas(Pos pos, Pos delta)
        {
            foreach (var letter in "XMAS")
            {
                if (grid[pos] != letter) return false;
                pos += delta;
            }
            return true;
        }

        foreach (var pos in grid.Positions(3))
            foreach (var delta in Pos.DeltaClockwize8Steps)
                matches += checkXmas(pos, delta) ? 1 : 0;

        print(matches);
    }
}

