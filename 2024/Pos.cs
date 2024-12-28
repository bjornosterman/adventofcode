public struct Pos(int X, int Y)
{
    public int X { get; } = X;
    public int Y { get; } = Y;
    public Pos Rigth => new Pos(X + 1, Y);
    public Pos Down => new Pos(X, Y + 1);
    public Pos Left => new Pos(X - 1, Y);
    public Pos Up => new Pos(X, Y - 1);

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
        var pos2 = (Pos)obj;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return X == pos2.X && pos2.Y == Y;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public override int GetHashCode()
    {
        return (X * 1000000 + Y).GetHashCode();
    }
}
