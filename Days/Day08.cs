namespace AOC.Y2025
{
    public class Day08 : IProblem
    {
        private readonly List<List<Point3>> Circuits = new();
        private readonly List<Point3> Boxes = new();
        private readonly List<(double, int, int)> Distances = new();

        public void Parse(string[] input)
        {
            Boxes.AddRange(input.Select(s => new Point3(s)));
            Circuits.AddRange(Boxes.Select(b => new List<Point3> { b }));
        }

        public object PartOne()
        {
            ParseDistances();

            var byShortest = Distances.OrderBy(d => d.Item1).Take(Program.IsRealInput ? 1000 : 10);
            foreach (var boxPair in byShortest)
            {
                int left = Circuits.FindIndex(c => c.Contains(Boxes[boxPair.Item2]));
                int right = Circuits.FindIndex(c => c.Contains(Boxes[boxPair.Item3]));

                if (left != right)
                {
                    var newList = Circuits[left].Concat(Circuits[right]).ToList();
                    Circuits.RemoveAt(Math.Max(left, right));
                    Circuits.RemoveAt(Math.Min(left, right));
                    Circuits.Add(newList);
                }
            }

            return Circuits.Select(c => c.Count)
                .OrderBy(c => c)
                .Reverse()
                .Take(3)
                .Aggregate(1, (a, v) => a * v);
        }

        public object PartTwo()
        {
            ParseDistances();

            var byShortest = Distances.OrderBy(d => d.Item1);
            foreach (var boxPair in byShortest)
            {
                int left = Circuits.FindIndex(c => c.Contains(Boxes[boxPair.Item2]));
                int right = Circuits.FindIndex(c => c.Contains(Boxes[boxPair.Item3]));

                if (left != right)
                {
                    var newList = Circuits[left].Concat(Circuits[right]).ToList();
                    Circuits.RemoveAt(Math.Max(left, right));
                    Circuits.RemoveAt(Math.Min(left, right));
                    Circuits.Add(newList);
                }

                // End condition
                if (Circuits.Count == 1)
                {
                    return Boxes[boxPair.Item2].X * Boxes[boxPair.Item3].X;
                }
            }

            return -1;
        }

        private void ParseDistances()
        {
            if (Distances.Count == 0)
            {
                for (int i = 0; i < Boxes.Count; i++)
                {
                    for (int j = i + 1; j < Boxes.Count; j++)
                    {
                        Distances.Add((Boxes[i].DistanceFrom(Boxes[j]), i, j));
                    }
                }
            }
        }

        private static double GetCircuitDistance(List<Point3> leftCircuit, List<Point3> rightCircuit)
        {
            double distance = double.MaxValue;
            foreach (var left in leftCircuit)
            {
                foreach (var right in rightCircuit)
                {
                    distance = Math.Min(distance, left.DistanceFrom(right));
                }
            }

            return distance;
        }
    }
}