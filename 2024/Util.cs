using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Collections.Generic;

public static class Util
{
    public static char[,] ToCharGrid(string[] lines, int margin = 0, char fill = ' ')
    {
        var width = lines[0].Length;
        var height = lines.Length;
        var grid_width = width + 2 * margin;
        var grid_height = height + 2 * margin;

        var grid = new char[grid_width, grid_height];

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                grid[x, y] = fill;

        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                grid[x + margin, y + margin] = lines[y][x];
            }

        return grid;
    }

    public static IEnumerable<(int, int)> Coordinates(int width, int height)
    {
        for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                yield return (x, y);
    }

    public static (int, int) FindChar(this char[,] grid, char searchChar)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                if (grid[x, y] == searchChar)
                {
                    return (x, y);
                }

        throw new Exception("Couldn't find it");
    }

    public static IEnumerable<(int X, int Y)> FindAllChar(this char[,] grid, char searchChar)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                if (grid[x, y] == searchChar)
                {
                    yield return (x, y);
                }
    }

    internal static (int dx, int dy) GetMoveDelta(char direction)
    {
        switch (direction)
        {
            case '^':
                return (0, -1);
            case '>':
                return (1, 0);
            case 'v':
                return (0, 1);
            case '<':
                return (-1, 0);
            default:
                throw new Exception("Unknown direction");
        }
    }

    internal static char RotateClockwise(char direction)
    {
        return direction switch
        {
            '^' => '>',
            '>' => 'v',
            'v' => '<',
            '<' => '^',
            _ => throw new Exception($"Unknown direction '{direction}'"),
        };
    }

    internal static char RotateCounterClockwise(char direction)
    {
        return direction switch
        {
            '^' => '<',
            '>' => '^',
            'v' => '>',
            '<' => 'v',
            _ => throw new Exception($"Unknown direction '{direction}'"),
        };
    }
    internal static int CountChar(this char[,] grid, char searchChar)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        var count = 0;

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                if (grid[x, y] == searchChar)
                {
                    count++;
                }
        return count;
    }

    public static IEnumerable<Pos> EnumeratePositions(this char[,] grid)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                yield return new Pos(x, y);
    }

    internal static void Print(this char[,] grid)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
        {
            for (var x = 0; x < grid_width; x++)
                Console.Write(grid[x, y]);
            Console.WriteLine();
        }
    }

    internal static void Change(this char[,] grid, char fromChar, char toChar)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < grid_width; x++)
                if (grid[x, y] == fromChar) grid[x, y] = toChar;
    }

    internal static (int, int) Plus((int, int) p1, (int, int) p2)
    {
        return (p1.Item1 + p2.Item1, p1.Item2 + p2.Item2);
    }
}
