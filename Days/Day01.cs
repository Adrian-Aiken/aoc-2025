namespace AOC.Y2025
{
    public class Day01 : IProblem
    {
        private List<string> rotations = new List<string>();
        private int position = 50, zeros, passes;

        public void Parse(string[] input)
        {
            rotations.AddRange(input);
        }

        public object PartOne()
        {
            DoRotations();
            return zeros;
        }

        public object PartTwo()
        {
            DoRotations();
            return passes;
        }

        private void DoRotations()
        {
            if (passes != 0) return;

            foreach (string rotation in rotations)
            {
                int distance = int.Parse(rotation.Substring(1));

                if (rotation[0] == 'R')
                {
                    for (int i = 0; i < distance; i++)
                    {
                        position++;
                        if (position == 100) { position = 0; passes++; }
                    }
                }
                else
                {
                    for (int i = 0; i < distance; i++)
                    {
                        position--;
                        if (position == 0) passes++;
                        else if (position == -1) position = 99;
                    }
                }

                if (position == 0) zeros++;
            }
        }
    }
}