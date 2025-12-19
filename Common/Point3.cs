namespace AOC
{
    public class Point3
    {
        public long X, Y, Z;

        public Point3() { }

        public Point3(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(string input, string delim = ",")
        {
            var chars = input.Split(delim);
            X = long.Parse(chars[0]);
            Y = long.Parse(chars[1]);
            Z = long.Parse(chars[2]);
        }

        public double DistanceFrom(Point3 other)
        {
            long dx = X - other.X;
            long dy = Y - other.Y;
            long dz = Z - other.Z;

            return Math.Sqrt((dx * dx) + (dy * dy) + (dz * dz));
        }
    }
}