using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day8 : StringBatchesDay
{
    protected override object FirstTask()
    {
        var instructions = this.Input[0].Select(x => x).ToArray();
        var map = this.Input[1].Split("\r").Select(line =>
        {
            var currentPosition = line.Trim()[0..3];
            var left = line.Trim()[7..10];
            var right = line.Trim()[12..15];
            return (current: currentPosition, left, right);
        }).ToDictionary(x => x.current, x => (x.left, x.right));

        var currentPosition = "AAA";
        var stepCounter = 0;

        while (currentPosition != "ZZZ")
        {
            var instr = map[currentPosition];
            currentPosition = instructions[stepCounter % (instructions.Length)] == 'R' ? instr.right : instr.left;
            stepCounter++;
        }

        return stepCounter;
    }

    protected override object SecondTask()
    {
        var instructions = this.Input[0].Select(x => x).ToArray();
        var map = this.Input[1].Split("\r").Select(line =>
        {
            var currentPosition = line.Trim()[0..3];
            var left = line.Trim()[7..10];
            var right = line.Trim()[12..15];
            return (current: currentPosition, left, right);
        }).ToDictionary(x => x.current, x => (x.left, x.right));

        var startingPositions = map.Keys.Where(k => k[2] == 'A');
        var endPosition = map.Keys.Where(k => k[2] == 'Z');

        var cycles = new Dictionary<string, HashSet<string>>();
        var solutions = new Dictionary<string, HashSet<int>>();
        foreach (var start in startingPositions)
        {
            cycles.Add(start, new HashSet<string>());
            solutions.Add(start, new HashSet<int>());
        }

        var cycleIndexes = new List<int>();
        foreach (var start in startingPositions)
        {
            var currentPosition = start;
            var stepCounter = 0;

            while (true)
            {
                var instr = map[currentPosition];
                currentPosition = instructions[stepCounter % (instructions.Length)] == 'R' ? instr.right : instr.left;
                stepCounter++;

                if (currentPosition[2] == 'Z') {
                    solutions[start].Add(stepCounter / instructions.Length);
                    var result = cycles[start].Add($"{start}{currentPosition}{stepCounter % instructions.Length}");
                    if (!result) {
                        cycleIndexes.Add(stepCounter);
                        break;
                    }
                }
            }
        }

        var x = solutions.Select(s => (long)s.Value.First()).ToArray();
        var lcm = FindLCM(x);

        return FindLCM(x) * instructions.Length;
    }

    private long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private long LCM(long a, long b)
    {
        return (a * b) / GCD(a, b);
    }

    private long FindLCM(long[] numbers)
    {
        long lcm = numbers[0];

        for (long i = 1; i < numbers.Length; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }

        return lcm;
    }
}
