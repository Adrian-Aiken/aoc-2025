namespace AOC.Y2025
{
    public class Day05 : IProblem
    {
        private readonly List<(long, long)> freshRanges = new();
        private readonly List<long> ingredients = new();

        public void Parse(string[] input)
        {
            var longRanges = new Queue<(long, long)>();

            int i = -1;
            while (input[++i] != string.Empty)
            {
                longRanges.Enqueue(Utils.ParseRangeLong(input[i]));
            }

            while (++i < input.Length)
            {
                ingredients.Add(long.Parse(input[i]));
            }

            while (longRanges.TryDequeue(out var newRange))
            {
                bool didMerge = false;
                foreach (var range in freshRanges)
                {
                    if (Math.Max(range.Item1, newRange.Item1) <= Math.Min(range.Item2, newRange.Item2))
                    {
                        freshRanges.Remove(range);
                        longRanges.Enqueue((Math.Min(range.Item1, newRange.Item1), Math.Max(range.Item2, newRange.Item2)));
                        didMerge = true;
                        break;
                    }
                }

                if (!didMerge)
                {
                    freshRanges.Add(newRange);
                }
            }
        }

        public object PartOne()
        {
            return ingredients.Where(IsFresh).Count();
        }

        public object PartTwo()
        {
            return freshRanges.Select(r => r.Item2 - r.Item1 + 1).Sum();
        }

        private bool IsFresh(long ingredient)
        {
            return freshRanges.Any(range => range.Item1 <= ingredient && ingredient <= range.Item2);
        }
    }
}