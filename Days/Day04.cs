namespace AOC.Y2025
{
    public class Day04 : IProblem
    {
        List<List<char>> baseFloor = new List<List<char>>();

        public void Parse(string[] input)
        {
            baseFloor = Utils.Parse2DGrid(input);
            Utils.PadGrid(baseFloor, '.');
        }

        public object PartOne()
        {
            var floor = Utils.Duplicate2DList(baseFloor);
            int count = 0;
            Utils.Iterate2DGrid(floor, rollCount, padding: 1);

            void rollCount(int x, int y)
            {
                if (floor[x][y] != '@') return;

                int neighbors = 0;
                if (floor[x - 1][y - 1] == '@') neighbors++;
                if (floor[x - 1][y] == '@') neighbors++;
                if (floor[x - 1][y + 1] == '@') neighbors++;
                if (floor[x][y - 1] == '@') neighbors++;
                if (floor[x][y + 1] == '@') neighbors++;
                if (floor[x + 1][y - 1] == '@') neighbors++;
                if (floor[x + 1][y] == '@') neighbors++;
                if (floor[x + 1][y + 1] == '@') neighbors++;

                if (neighbors < 4) count++;
            }

            return count;
        }

        public object PartTwo()
        {
            var floor = Utils.Duplicate2DList(baseFloor);
            int total = 0, count = 0;

            void tryRollRemove(int x, int y)
            {
                if (floor[x][y] != '@') return;

                int neighbors = 0;
                if (floor[x - 1][y - 1] == '@') neighbors++;
                if (floor[x - 1][y] == '@') neighbors++;
                if (floor[x - 1][y + 1] == '@') neighbors++;
                if (floor[x][y - 1] == '@') neighbors++;
                if (floor[x][y + 1] == '@') neighbors++;
                if (floor[x + 1][y - 1] == '@') neighbors++;
                if (floor[x + 1][y] == '@') neighbors++;
                if (floor[x + 1][y + 1] == '@') neighbors++;

                if (neighbors < 4)
                {
                    count++;
                    floor[x][y] = '.';
                }
            }

            do
            {
                count = 0;
                Utils.Iterate2DGrid(floor, tryRollRemove, padding: 1);
                total += count;

            } while (count != 0);

            return total;
        }
    }
}