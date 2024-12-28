

public class Grid
{
    public Grid(char[,] data)
    {
        this.data = data;
    }

    public char[,] data;

    public int Width => data.GetLength(0);
    public int Height => data.GetLength(1);

    public static Grid Construct(IEnumerable<string> enumerable)
    {
        var lines = enumerable.ToArray();
        return new Grid(Util.ToCharGrid(lines));

    }

    public IEnumerable<Pos> FindChars(char searchChar)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                if (data[x, y] == searchChar)
                    yield return new Pos(x, y);
    }

    public char this[Pos pos]
    {
        get => data[pos.X, pos.Y];
        set => data[pos.X, pos.Y] = value;
    }

    internal static Pos GetMoveDelta(char direction)
    {
        switch (direction)
        {
            case '^':
                return new Pos(0, -1);
            case '>':
                return new Pos(1, 0);
            case 'v':
                return new Pos(0, 1);
            case '<':
                return new Pos(-1, 0);
            default:
                throw new Exception("Unknown direction");
        }
    }

    internal void Print()
    {
        var grid_width = data.GetLength(0);
        var grid_height = data.GetLength(1);

        for (var y = 0; y < grid_height; y++)
        {
            for (var x = 0; x < grid_width; x++)
                Console.Write(data[x, y]);
            Console.WriteLine();
        }
    }
}