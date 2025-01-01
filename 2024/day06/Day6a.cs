public class Day06a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        var room = Grid.Construct(input, 1, ' ');

        var direction = '^';

        var pos = room.FindChars('^').First();

        while (room[pos] != ' ')
        {
            var new_pos = pos + Pos.GetDelta(direction);
            switch (room[new_pos])
            {
                case '#':
                    direction = Util.RotateClockwise(direction);
                    break;
                default:
                    room[pos] = 'X';
                    pos = new_pos;
                    break;
            }
        }

        var count = room.FindChars('X').Count();
        room.Print();
        print(count);

    }

}

