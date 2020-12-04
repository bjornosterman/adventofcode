namespace Day10_LineOfSite
{
    public struct Point
    {
        public int X, Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.X == point2.X && point1.Y == point2.Y;
        }
        public static bool operator !=(Point point1, Point point2)
        {
            return point1.X != point2.X || point1.Y != point2.Y;
        }
        public override bool Equals(object obj)
        {
            var point1 = this;
            var point2 = (Point)obj;
            return point1.X == point2.X && point1.Y == point2.Y;
        }

        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }
        public override string ToString()
        {
            return "X=" + X + ", Y=" + Y;
        }
    }
}
