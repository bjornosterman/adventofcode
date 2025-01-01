
public struct Pos(int X, int Y)
{
    public int X { get; } = X;
    public int Y { get; } = Y;
    // public Pos Rigth => new Pos(X + 1, Y);
    // public Pos Down => new Pos(X, Y + 1);
    // public Pos Left => new Pos(X - 1, Y);
    public int Length => Math.Abs(X) + Math.Abs(Y);
    public Pos Up => this + DeltaUp; // new Pos(X, Y - 1);
    public Pos Down => this + DeltaDown;
    public Pos Right => this + DeltaRight;
    public Pos Left => this + DeltaLeft;
    public Pos UpRight => this + DeltaUpRight;
    public Pos DownRight => this + DeltaDownRight;
    public Pos UpLeft => this + DeltaUpLeft;
    public Pos DownLeft => this + DeltaDownLeft;

    public static Pos DeltaUp => new Pos(0, -1);
    public static Pos DeltaDown => new Pos(0, 1);
    public static Pos DeltaLeft => new Pos(-1, 0);
    public static Pos DeltaRight => new Pos(1, 0);

    public static Pos DeltaUpRight => DeltaUp + DeltaRight;
    public static Pos DeltaDownRight => DeltaDown + DeltaRight;
    public static Pos DeltaUpLeft => DeltaUp + DeltaLeft;
    public static Pos DeltaDownLeft => DeltaDown + DeltaLeft;

    public static Pos[] DeltaClockwize4Steps => [
        DeltaRight, DeltaDown,DeltaLeft, DeltaUp,];
    public static Pos[] DeltaClockwize8Steps =>
        [DeltaRight, DeltaDownRight, DeltaDown, DeltaDownLeft, DeltaLeft, DeltaUpLeft, DeltaUp, DeltaUpRight];

    public static Pos operator +(Pos p1, Pos p2)
    {
        return new Pos(p1.X + p2.X, p1.Y + p2.Y);
    }

    public static Pos operator -(Pos p1, Pos p2)
    {
        return new Pos(p1.X - p2.X, p1.Y - p2.Y);
    }

    public static bool operator ==(Pos p1, Pos p2)
    {
        return p1.X == p2.X && p1.Y == p2.Y;
    }

    public static bool operator !=(Pos p1, Pos p2)
    {
        return p1.X != p2.X || p1.Y != p2.Y;
    }

    public override string ToString()
    {
        return $"[{X},{Y}]";
    }

    public override bool Equals(object? obj)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8605 // Unboxing a possibly null value.
        var pos2 = (Pos)obj;
#pragma warning restore CS8605 // Unboxing a possibly null value.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return X == pos2.X && pos2.Y == Y;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    internal static Pos GetDelta(char direction)
    {
        switch (direction)
        {
            case '^':
                return DeltaUp;
            case '>':
                return DeltaRight;
            case 'v':
                return DeltaDown;
            case '<':
                return DeltaLeft;
            default:
                throw new Exception("Unknown direction");
        }
    }

    public override int GetHashCode()
    {
        return (X * 1000000 + Y).GetHashCode();
    }
}
