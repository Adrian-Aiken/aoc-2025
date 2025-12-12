using Microsoft.Z3;

namespace AOC.Y2025
{
    public class Machine
    {
        public bool[] TargetLamps;
        public int[] Joltages;
        public List<List<int>> Buttons = new();

        public Machine(string input)
        {
            var parts = input.Split(' ');

            TargetLamps = parts[0].Trim('[', ']').Select(c => c == '#').ToArray();
            Joltages = parts.Last().Trim('{', '}').Split(',').Select(int.Parse).ToArray();

            for (int i = 1; i < parts.Count() - 1; i++)
            {
                Buttons.Add(parts[i].Trim('(', ')').Split(',').Select(int.Parse).ToList());
            }
        }

        public List<List<int>> GetMachineStartSequence()
        {
            foreach (var sequence in Utils.PowerSet(Buttons).ToList().OrderBy(l => l.Count))
            {
                bool[] lamps = new bool[TargetLamps.Length];
                foreach (var button in sequence)
                {
                    foreach (var wire in button) lamps[wire] = !lamps[wire];
                }

                if (Utils.ArrayEquals(TargetLamps, lamps))
                {
                    return sequence;
                }
            }

            return new();
        }

        public int GetMachineOperationSequencePressCount()
        {
            using var context = new Microsoft.Z3.Context();
            using var optimize = context.MkOptimize();

            var presses = Enumerable.Range(0, Buttons.Count).Select(i => context.MkIntConst($"p{i}")).ToArray();

            foreach (var press in presses)
                optimize.Add(context.MkGe(press, context.MkInt(0)));

            for (int i = 0; i < Joltages.Length; i++)
            {
                var affected = presses.Where((_, j) => Buttons[j].Contains(i)).ToArray();
                if (affected.Length > 0)
                {
                    var sum = affected.Length == 1 ? affected[0] : context.MkAdd(affected);
                    optimize.Add(context.MkEq(sum, context.MkInt(Joltages[i])));
                }
            }

            optimize.MkMinimize(presses.Length == 1 ? presses[0] : context.MkAdd(presses));
            optimize.Check();

            var model = optimize.Model;
            return presses.Sum(p => ((IntNum)model.Evaluate(p, true)).Int);
        }
    }

    public class Day10 : IProblem
    {
        private readonly List<Machine> Machines = new();

        public void Parse(string[] input)
        {
            Machines.AddRange(input.Select(s => new Machine(s)));
        }

        public object PartOne()
        {
            return Machines.AsParallel().Sum(m => m.GetMachineStartSequence().Count);
        }

        public object PartTwo()
        {
            return Machines.AsParallel().Sum(m => m.GetMachineOperationSequencePressCount());
        }
    }
}