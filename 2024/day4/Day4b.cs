public class Day04b : Day
{
    private char[,] grid;
    public override void Run()
    {
        var input = puzzleInput;

        var width = input[0].Length;
        var height = input.Length;

        grid = new char[width + 6, height + 6];

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                grid[x + 3, y + 3] = input[y][x];
            }

        int matches = 0;

        for (var y = 3; y < height + 3; y++)
            for (var x = 3; x < width + 3; x++)
            {
                var letters = grid[x, y].ToString() + grid[x - 1, y - 1] + grid[x + 1, y - 1] + grid[x + 1, y + 1] + grid[x - 1, y + 1];
                matches +=
                    letters == "AMMSS" ||
                    letters == "ASMMS" ||
                    letters == "ASSMM" ||
                    letters == "AMSSM" ? 1 : 0;
            }

        print(matches);
    }

    // public int checkXMAS(int x, int y, int dx, int dy)
    // {

    //     foreach (var letter in "XMAS")
    //     {
    //         if (grid[x, y] != letter) return 0;
    //         x += dx;
    //         y += dy;
    //     }
    //     return 1;
    // }
}

