public class Day08b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var width = input[0].Length;
        var height = input.Length;

        var grid = Util.ToCharGrid(input);

        var items = new List<(int, int, char)>();

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                items.Add((x, y, input[y][x]));

        items.RemoveAll(x => x.Item3 == '.');

        var IsInside = (int x, int y) => x >= 0 && x < width && y >= 0 && y < height;

        foreach (var group in items.GroupBy(x => x.Item3))
        {
            var g = group.ToArray();
            for (int i = 0; i < group.Count(); i++)
                for (int j = i + 1; j < group.Count(); j++)
                {
                    var a = g[i];
                    var b = g[j];
                    var dx = b.Item1 - a.Item1;
                    var dy = b.Item2 - a.Item2;
                    var x = a.Item1 - dx;
                    var y = a.Item2 - dy;
                    while (IsInside(x, y))
                    {
                        grid[x, y] = '#';
                        x -= dx;
                        y -= dy;
                    }
                    x = b.Item1 + dx;
                    y = b.Item2 + dy;
                    while (IsInside(x, y))
                    {
                        grid[x, y] = '#';
                        x += dx;
                        y += dy;
                    }
                }
        }


        Util.Print(grid);
        print(Util.CountChar(grid, '#'));
        print(width * height - Util.CountChar(grid, '.'));

        // print(result);
    }

}

