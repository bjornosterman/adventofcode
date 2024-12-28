using System.Numerics;

public class Day15a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var grid = Grid.Construct(input.TakeWhile(x => x.Length > 0));
        var operations = string.Join("", input.SkipWhile(x => x.Length > 0).Skip(1).ToList());
        var robot_pos = grid.FindChars('@').First();

        foreach (var op in operations)
        {
            // Console.WriteLine("Move: " + op);
            // Console.WriteLine();

            var move_delta = Grid.GetMoveDelta(op);
            var next_pos = robot_pos + move_delta;

            Func<Pos?> FindNextFree = () => 
            {
                var next_free_pos = next_pos;
                while (true)
                {
                    switch (grid[next_free_pos])
                    {
                        case '#':
                            return null;
                        case '.':
                            return next_free_pos;
                    }
                    next_free_pos = next_free_pos + move_delta;
                }
            };

            switch (grid[next_pos])
            {
                case '.':
                    grid[next_pos] = '@';
                    grid[robot_pos] = '.';
                    robot_pos = next_pos;
                    break;
                case 'O':
                    var next_free_pos = FindNextFree();
                    if (next_free_pos != null)
                    {
                        grid[next_free_pos.Value] = 'O';
                        grid[next_pos] = '@';
                        grid[robot_pos] = '.';
                        robot_pos = next_pos;
                    }
                    break;
            }
            // Console.WriteLine();
            // grid.Print();
        }

        Console.WriteLine();
        grid.Print();
        var posses = grid.FindChars('O');
        var result = posses.Select(x => x.X + x.Y * 100).Sum();
        print(result);

    }
}

