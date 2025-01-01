public class Day04b : Day
{
    public override void Run()
    {
        var input = puzzleInput;

        var grid = Grid.Construct(input, 1);

        int matches = 0;

        foreach (var pos in grid.Positions(1))
        {
            var letters = new string(new Pos[] { pos, pos.UpLeft, pos.UpRight, pos.DownRight, pos.DownLeft }
                .Select(x => grid[x])
                .ToArray());
            matches +=
                letters == "AMMSS" ||
                letters == "ASMMS" ||
                letters == "ASSMM" ||
                letters == "AMSSM" ? 1 : 0;
        }

        print(matches);
    }
}

