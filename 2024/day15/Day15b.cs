using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Day15b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        static string transform(string line)
        {
            static string t(char c) => c switch { '#' => "##", 'O' => "[]", '@' => "@.", _ => "..", };
            return string.Join("", line.Select(t));
        }

        var grid = Grid.Construct(input.TakeWhile(x => x.Length > 0).Select(transform));
        var operations = string.Join("", input.SkipWhile(x => x.Length > 0).Skip(1).ToList());
        var robot_pos = grid.FindChars('@').First();
        grid.Print();

        foreach (var op in operations)
        {
            if (sample)
            {
                Console.WriteLine("Move: " + op);
                Console.WriteLine();
                // Console.Read();
            }

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
                case '[':
                case ']':
                    if (op == '<' || op == '>')
                    {
                        var next_free_pos = FindNextFree();
                        if (next_free_pos != null)
                        {
                            while (next_free_pos != robot_pos)
                            {
                                grid[next_free_pos.Value] = grid[next_free_pos.Value - move_delta];
                                next_free_pos -= move_delta;
                            }
                            grid[robot_pos] = '.';
                            robot_pos = next_pos;
                        }
                    }
                    else
                    {
                        var parts = new List<Pos>([robot_pos]);
                        var blocked = false;
                        for (int i = 0; i < parts.Count() && !blocked; i++)
                        {
                            var pos = parts[i] + move_delta;
                            void add(Pos x) { if (!parts.Contains(x)) parts.Add(x); };
                            switch (grid[pos])
                            {
                                case '[':
                                    add(pos);
                                    add(pos.Right);
                                    break;
                                case ']':
                                    add(pos);
                                    add(pos.Left);
                                    break;
                                case '#':
                                    blocked = true;
                                    break;
                            }
                        }
                        if (!blocked)
                        {
                            parts.Reverse();
                            foreach (var part in parts)
                            {
                                grid[part + move_delta] = grid[part];
                                grid[part] = '.';
                            }
                            grid[robot_pos + move_delta] = '@';
                            robot_pos = next_pos;
                        }
                    }
                    break;
            }
            // grid.Print();
            // Console.WriteLine();
        }

        Console.WriteLine();
        grid.Print();
        var posses = grid.FindChars('[');
        var result = posses.Select(x => x.X + x.Y * 100).Sum();
        print(result);

    }
}

