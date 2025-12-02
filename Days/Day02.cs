namespace AOC.Y2025
{
    public class Day02 : IProblem
    {
        List<(long, long)> IdRanges = new List<(long, long)>();

        public void Parse(string[] input)
        {
            foreach (var line in input[0].Split(','))
            {
                var range = line.Split('-').Select(long.Parse).ToArray();
                IdRanges.Add((range[0], range[1]));
            }
        }

        private static bool IsValidId(long id)
        {
            string idString = id.ToString();

            // Odd-length strings should always be valid
            if (idString.Length % 2 == 1) return true;

            int halfLength = idString.Length / 2;

            return !idString[..halfLength].Equals(idString[halfLength..]);
        }

        public object PartOne()
        {
            long sum = 0;
            foreach (var (start, end) in IdRanges)
            {
                for (long i = start; i <= end; i++)
                {
                    if (!IsValidId(i)) sum += i;
                }
            }

            return sum;
        }

        private static bool IsValidIdOfSize(long id, int size)
        {
            string idString = id.ToString();

            // Odd-length strings should always be valid
            if (idString.Length % size != 0) return true;

            string pattern = idString[..size];

            for (int i = size; i < idString.Length; i += size)
            {
                if (!idString.Substring(i, size).Equals(pattern)) return true;
            }

            return false;
        }

        public object PartTwo()
        {
            long sum = 0;
            foreach (var (start, end) in IdRanges)
            {
                for (long i = start; i <= end; i++)
                {
                    int sizeLimit = i.ToString().Length / 2;
                    for (int j = 1; j <= sizeLimit; j++)
                    {
                        if (!IsValidIdOfSize(i, j))
                        {
                            sum += i;
                            break;
                        }
                    }
                }
            }

            return sum;
        }
    }
}