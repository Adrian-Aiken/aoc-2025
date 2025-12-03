using System.ComponentModel;

namespace AOC.Y2025
{
    public class Day03 : IProblem
    {
        readonly List<int[]> batteries = new();

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                batteries.Add(line.Select(c => c - '0').ToArray());
            }
        }

        public object PartOne()
        {
            int sum = 0;
            foreach (var bank in batteries)
            {
                int maxJoltage = 0;
                for (int i = 0; i < bank.Length - 1; i++)
                {
                    for (int j = i + 1; j < bank.Length; j++)
                    {
                        int joltage = (bank[i] * 10) + bank[j];
                        maxJoltage = Math.Max(maxJoltage, joltage);
                    }
                }

                sum += maxJoltage;
            }

            return sum;
        }

        public object PartTwo()
        {
            return batteries.Select(b => FindLargest(b, 0, 12)).Sum();
        }

        private static long FindLargest(int[] bank, int start, int remaining)
        {
            if (remaining == 1)
            {
                return bank.Skip(start).Max();
            }

            long largest = 0;
            int digit = 10;

            while (largest == 0 && --digit > 0)
            {
                for (int i = start; i < bank.Length - remaining + 1; i++)
                {
                    if (bank[i] == digit)
                    {
                        largest = Math.Max(FindLargest(bank, i + 1, remaining - 1), largest);
                    }
                }
            }

            return largest + (digit * (long)Math.Pow(10, remaining - 1));
        }
    }
}