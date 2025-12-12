using System.Drawing;

namespace AOC.Y2025
{
    public class Day09 : IProblem
    {
        private readonly List<Point> Tiles = new();

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                var coords = line.Split(',').Select(int.Parse).ToArray();
                Tiles.Add(new(coords[0], coords[1]));
            }
        }

        public object PartOne()
        {
            long largest = 0;

            for (int i = 0; i < Tiles.Count; i++)
            {
                for (int j = i + 1; j < Tiles.Count; j++)
                {
                    long width = Math.Abs(Tiles[i].X - Tiles[j].X) + 1;
                    long height = Math.Abs(Tiles[i].Y - Tiles[j].Y) + 1;

                    largest = Math.Max(largest, width * height);
                }
            }

            return largest;
        }

        public object PartTwo()
        {
            long largest = 0;

            for (int i = 0; i < Tiles.Count; i++)
            {
                for (int j = i + 1; j < Tiles.Count; j++)
                {
                    long width = Math.Abs(Tiles[i].X - Tiles[j].X) + 1;
                    long height = Math.Abs(Tiles[i].Y - Tiles[j].Y) + 1;
                    long size = width * height;

                    if (size > largest && !HasIntersection(i, j))
                    {
                        largest = size;
                    }
                }
            }

            return largest;
        }

        private bool HasIntersection(int aIndex, int bIndex)
        {
            var a = Tiles[aIndex];
            var b = Tiles[bIndex];

            int minX = Math.Min(a.X, b.X); int maxX = Math.Max(a.X, b.X);
            int minY = Math.Min(a.Y, b.Y); int maxY = Math.Max(a.Y, b.Y);

            for (int i = 0; i < Tiles.Count; i++)
            {
                var j = (i + 1) % Tiles.Count;
                if (i == aIndex || j == aIndex || i == bIndex || j == bIndex) continue;

                var c = Tiles[i];
                var d = Tiles[j];

                int eMinX = Math.Min(c.X, d.X); int eMaxX = Math.Max(c.X, d.X);
                int eMinY = Math.Min(c.Y, d.Y); int eMaxY = Math.Max(c.Y, d.Y);

                if (minX < eMaxX && maxX > eMinX && minY < eMaxY && maxY > eMinY)
                {
                    return true;
                }
            }

            return false;
        }
    }
}