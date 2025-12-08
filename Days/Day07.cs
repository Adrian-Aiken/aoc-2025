namespace AOC.Y2025
{
    public class Day07 : IProblem
    {
        private string[] Floor = Array.Empty<string>();

        public void Parse(string[] input)
        {
            Floor = input;
        }

        public object PartOne()
        {
            int splits = 0;

            var (_, startCol) = Utils.Find2D(Floor, 'S');
            var beams = new HashSet<int> { startCol };

            foreach (var row in Floor)
            {
                foreach (var beam in beams.ToList())
                {
                    if (row[beam] == '^')
                    {
                        beams.Remove(beam);
                        beams.Add(beam + 1);
                        beams.Add(beam - 1);
                        splits++;
                    }
                }
            }

            return splits;
        }

        public object PartTwo()
        {
            var (_, startCol) = Utils.Find2D(Floor, 'S');
            var beams = new Dictionary<int, long>();
            beams[startCol] = 1;

            foreach (var row in Floor)
            {
                foreach (var beam in beams.ToList())
                {
                    var (col, count) = beam;

                    if (row[col] == '^')
                    {
                        if (beams.ContainsKey(col - 1)) beams[col - 1] += count;
                        else beams[col - 1] = count;

                        if (beams.ContainsKey(col + 1)) beams[col + 1] += count;
                        else beams[col + 1] = count;

                        beams.Remove(col);
                    }
                }
            }

            return beams.Sum(kvp => kvp.Value);
        }
    }
}