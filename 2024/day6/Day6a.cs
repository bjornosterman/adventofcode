public class Day06a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var room = Util.ToCharGrid(input, 1, 'Q');

        // find person
        var (x, y) = Util.FindChar(room, '^');
        var direction = '^';

        while (room[x, y] != 'Q')
        {
            room[x, y] = 'X';
            var (dx, dy) = Util.GetMoveDelta(direction);
            var (nx, ny) = (x + dx, y + dy);
            switch (room[nx, ny])
            {
                case '#':
                    direction = Util.RotateClockwise(direction);
                    break;
                default:
                    x = nx; y = ny;
                    break;
            }
        }

        var count = Util.CountChar(room, 'X');
        print(count);
        Util.Print(room);

    }

}

