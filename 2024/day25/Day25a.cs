public class Day25a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;
        var grids = new List<Grid>();
        while (true)
        {
            var lines = input.TakeWhile(x => x != "").ToArray();
            if (lines.Length == 0) break;
            grids.Add(Grid.Construct(lines));
            input = input.SkipWhile(x => x != "").Skip(1).ToArray();
        }

        int GetColValue(Grid grid, int col)
        {
            return Enumerable
                .Range(0, 7)
                .Select(x => grid[col, x])
                .Count(x => x == '#') - 1;
        }

        var keys = new List<(int, int, int, int, int)>();
        var locks = new List<(int, int, int, int, int)>();
        foreach (var grid in grids)
        {
            grid.Print();
            var values = (
                GetColValue(grid, 0),
                GetColValue(grid, 1),
                GetColValue(grid, 2),
                GetColValue(grid, 3),
                GetColValue(grid, 4)
            );
            print(values);
            if (grid[0, 0] == '#')
            {
                locks.Add(values);
            }
            else
            {
                keys.Add(values);
            }
        }
        keys.ForEach(x => print(x));
        locks.ForEach(x => print(x));

        bool Fit((int, int, int, int, int) key, (int, int, int, int, int) @lock)
        {
            return
            key.Item1 + @lock.Item1 <= 5 &&
            key.Item2 + @lock.Item2 <= 5 &&
            key.Item3 + @lock.Item3 <= 5 &&
            key.Item4 + @lock.Item4 <= 5 &&
            key.Item5 + @lock.Item5 <= 5;
        }

        int result = keys.SelectMany(x=>@locks, Fit).Count(x => x);

        print(result);
    }
}
