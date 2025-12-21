namespace AOC.Y2025
{
    public class Day12 : IProblem
    {
        private struct Tree
        {
            public Tree(string input)
            {
                var parts = input.Split(": ");
                var size = parts[0].Split('x');
                width = int.Parse(size[0]);
                length = int.Parse(size[1]);
                neededPresents = [.. parts[1].Split(' ').Select(int.Parse)];
            }

            public List<int> neededPresents = [];
            public int width, length;
        }

        private readonly List<bool[,]> Presents = new(6);
        private readonly List<Tree> Trees = [];

        public void Parse(string[] input)
        {
            int lineNum = 0;
            for (int i = 0; i <= 5; i++)
            {
                var present = new bool[3, 3];
                present[0, 0] = input[lineNum + 1][0] == '#';
                present[0, 1] = input[lineNum + 1][1] == '#';
                present[0, 2] = input[lineNum + 1][2] == '#';
                present[1, 0] = input[lineNum + 2][0] == '#';
                present[1, 1] = input[lineNum + 2][1] == '#';
                present[1, 2] = input[lineNum + 2][2] == '#';
                present[2, 0] = input[lineNum + 3][0] == '#';
                present[2, 1] = input[lineNum + 3][1] == '#';
                present[2, 2] = input[lineNum + 3][2] == '#';
                Presents.Add(present);
                lineNum += 5;
            }

            Trees.AddRange([.. input.Skip(lineNum).Select(l => new Tree(l))]);
        }

        public object PartOne()
        {
            return Trees.AsParallel().Where(CanFitPresents).Count();
        }

        public object PartTwo()
        {
            return "<3";
        }

        private bool CanFitPresents(Tree tree)
        {
            return CanFitPresentsSimple(tree);
        }

        private static bool CanFitPresentsSimple(Tree tree)
        {
            int tiles = (tree.length / 3) * (tree.width / 3);
            return tiles >= tree.neededPresents.Sum();
        }
    }
}