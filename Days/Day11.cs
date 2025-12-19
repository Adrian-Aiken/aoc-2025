namespace AOC.Y2025
{
    public class Day11 : IProblem
    {
        private Dictionary<string, List<string>> connections = new();
        private Dictionary<(string, string), long> memo = new();

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                var parts = line.Split();
                connections[parts[0].Replace(":", "")] = new List<string>(parts.Skip(1));
            }

            connections["out"] = new();
        }

        public object PartOne()
        {
            return CountPaths("you", "out");
        }

        public object PartTwo()
        {
            return (CountPaths("svr", "dac") * CountPaths("dac", "fft") * CountPaths("fft", "out"))
                + (CountPaths("svr", "fft") * CountPaths("fft", "dac") * CountPaths("dac", "out"));
        }

        private long CountPaths(string node, string end)
        {
            if (memo.TryGetValue((node, end), out var memoCount)) return memoCount;

            long count = 0;
            foreach (var machine in connections[node])
            {
                if (machine == end) count++;
                else count += CountPaths(machine, end);
            }

            memo.Add((node, end), count);
            return count;
        }
    }
}