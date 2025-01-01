

public class Grid
{
    public Grid(char[,] data)
    {
        this.data = data;
    }

    public Grid(int width, int height, char filler = ' ', int margin = 0, char marginFiller = '#')
    {
        data = new char[width + margin + margin, height + margin + margin];
        foreach (var pos in Positions()) this[pos] = marginFiller;
        foreach (var pos in Positions(margin)) this[pos] = filler;
    }

    public char[,] data;

    public int Width => data.GetLength(0);
    public int Height => data.GetLength(1);

    public Pos Start => FindChars('S').Single();
    public Pos End => FindChars('E').Single();

    public static Grid Construct(IEnumerable<string> enumerable, int margin = 0, char marginFIll = ' ')
    {
        var lines = enumerable.ToArray();
        return new Grid(Util.ToCharGrid(lines, margin, marginFIll));
    }

    public IEnumerable<Pos> Positions(int margin = 0)
    {
        for (var y = margin; y < Height - margin; y++)
            for (var x = margin; x < Width - margin; x++)
                yield return new Pos(x, y);
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

    public char this[int x, int y]
    {
        get => data[x, y];
        set => data[x, y] = value;
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

    internal void Print(bool hideWalls = false)
    {
        var grid_width = data.GetLength(0);
        var grid_height = data.GetLength(1);

        for (var y = 0; y < grid_height; y++)
        {
            for (var x = 0; x < grid_width; x++)
            {
                var c = data[x, y];
                Console.Write(hideWalls && c == '#' ? ' ' : c);
            }
            Console.WriteLine();
        }
    }

    internal Grid Clone()
    {
        return new Grid((char[,])data.Clone());
    }
}