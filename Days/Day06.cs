using System.Globalization;

namespace AOC.Y2025
{
    public class Day06 : IProblem
    {
        private readonly List<string[]> lines = new();
        private string[] input;

        public void Parse(string[] input)
        {
            this.input = input;
            lines.AddRange(input.Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)));
        }

        public object PartOne()
        {
            long total = 0;
            for (var i = 0; i < lines[0].Length; i++)
            {
                long sum = int.Parse(lines[0][i]);
                if (lines.Last()[i] == "+")
                {
                    for (var j = 1; j < lines.Count - 1; j++)
                    {
                        sum += int.Parse(lines[j][i]);
                    }
                }
                else
                {
                    for (var j = 1; j < lines.Count - 1; j++)
                    {
                        sum *= int.Parse(lines[j][i]);
                    }
                }

                total += sum;
            }

            return total;
        }

        public object PartTwo()
        {
            char op = '+';
            long total = 0, sum = 0;

            for (int col = 0; col < input[0].Length; col++)
            {
                // Base case - starting new problem, so record last
                if (input.Last()[col] != ' ')
                {
                    total += sum;
                    sum = 0;
                    op = input.Last()[col];
                }

                // Parse digits of column
                var cnum = 0;
                for (int row = 0; row < input.Length - 1; row++)
                {
                    if (Utils.IsDigit(input[row][col]))
                    {
                        cnum = (cnum * 10) + (input[row][col] - '0');
                    }
                }

                // Set first number, skip blank column, or do operation
                if (sum == 0) sum = cnum;
                else if (cnum == 0) continue;
                else sum = op == '+' ? sum + cnum : sum * cnum;
            }

            return total + sum;
        }
    }
}