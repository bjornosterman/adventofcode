using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;

public class Day14b : Day
{
    public class Robot
    {
        public int X;
        public int Y;
        public int VX;
        public int VY;

        internal void Step(int width, int height)
        {
            X = (X + width + VX) % width;
            Y = (Y + height + VY) % height;
        }

        public override int GetHashCode()
        {
            return (Y * 1000) + X;
        }
        public override bool Equals(object? obj)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            var r = (Robot)obj;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return r.X == X && r.Y == Y;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var width = sample ? 11 : 101;
        var mid = (width - 1) / 2;
        var height = sample ? 7 : 103;

        var robots = new List<Robot>();

        foreach (var line in input)
        {
            var match = Regex.Match(line, @"p=(?<x>\d+),(?<y>\d+) v=(?<dx>\-?\d+),(?<dy>\-?\d+)");
            var robot = new Robot()
            {
                X = int.Parse(match.Groups["x"].Value),
                Y = int.Parse(match.Groups["y"].Value),
                VX = int.Parse(match.Groups["dx"].Value),
                VY = int.Parse(match.Groups["dy"].Value)
            };
            robots.Add(robot);
            // print($"{q1} * {q2} * {q3} * {q4} = {q1 * q2 * q3 * q4}");
        }


        foreach (var _ in Enumerable.Range(1, 10000000))
        {
            foreach (var robot in robots)
                robot.Step(width, height);

            if (robots.Distinct().Count() == robots.Count())
            // if (robots.Count(x => x.X < mid) == robots.Count(x => x.X > mid))
            {
                var grid = new char[width, height];
                foreach (var robot in robots)
                {
                    grid[robot.X, robot.Y] = 'X';
                }
                grid.Change('\0', '.');
                grid.Print();
                // if (IsSymetrical(grid))
                // {
                //     return;
                // }
                print(_);
                Console.Read();
            }
        }
        // grid.Print();
    }

    private bool IsSymetrical(char[,] grid)
    {
        var grid_width = grid.GetLength(0);
        var grid_height = grid.GetLength(1);

        for (var y = 0; y < grid_height; y++)
            for (var x = 0; x < (grid_width - 1) / 2; x++)
                if (grid[x, y] != grid[grid_width - x - 1, y])
                    return false;
        return true;
    }
}

