using System.Numerics;

public class Day18b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        foreach (var i in Enumerable.Range(0, input.Length))
        {
            print(i);
            var mem = new Grid(sample ? 7 : 71, sample ? 7 : 71, '.', 1, '#');
            foreach (var line in input.Take(input.Length-i))
            {
                var split = line.Split(",");
                mem[int.Parse(split[0]) + 1, int.Parse(split[1]) + 1] = '#';
                // print(line);
            }

            var end = new Pos(mem.Width - 2, mem.Height - 2);
            mem[end] = 'E';
            // mem.Print();

            var costs = new int[mem.Width, mem.Height];

            void ProcessGrid(Pos pos, int cost)
            {
                if (mem[pos] == '#') return;
                var existing_cost = costs[pos.X, pos.Y];
                if (existing_cost == 0 || existing_cost > cost)
                {
                    costs[pos.X, pos.Y] = cost;
                    if (mem[pos] == 'E') return;
                    mem[pos] = 'O';
                    ProcessGrid(pos.Right, cost + 1);
                    ProcessGrid(pos.Down, cost + 1);
                    ProcessGrid(pos.Left, cost + 1);
                    ProcessGrid(pos.Up, cost + 1);
                }
            }

            ProcessGrid(new Pos(1, 1), 0);
            var end_cost = costs[end.X, end.Y];
            if (end_cost != 0) {
                mem.Print();
                print(input[input.Length-i]);
                return;
            }
        }
    }

}

