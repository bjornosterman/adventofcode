
public class Day06b : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var room = Util.ToCharGrid(input, 1, 'Q');

        var grid_width = room.GetLength(0);
        var grid_height = room.GetLength(1);

        int number_of_loops = 0;

        foreach ((int x, int y) in Util.Coordinates(grid_width, grid_height))
        {
            room = Util.ToCharGrid(input, 1, 'Q');
            if (room[x, y] == '.')
            {
                room[x, y] = '#';

                if (IsLoop(room))
                {
                    number_of_loops++;
                }
            }
        }


        print(number_of_loops);
        // Util.Print(room);

    }

    private bool IsLoop(char[,] room)
    {
        // find person
        var (x, y) = Util.FindChar(room, '^');
        var direction = '^';
        room[x, y] = '.';

        while (room[x, y] != 'Q')
        {
            switch (room[x, y])
            {
                case '.': room[x, y] = '1'; break;
                case '1': room[x, y] = '2'; break;
                case '2': room[x, y] = '3'; break;
                case '3': return true;
            }
            // room[x, y] = 'X';
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

        return false;
    }

}

