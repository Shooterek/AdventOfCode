using AoE2023.Utils;

namespace AoE2023;

public class Day6 : StringListDay
{
    protected override object FirstTask()
    {
        var numbers = this.Input
            .Select(line => line
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(long.Parse)
                .ToArray())
            .ToArray();
        
        var timeDistance = numbers[0].Zip(numbers[1]);
        var zeros = timeDistance.Select(td => CalculateRoots(td.First, td.Second));

        var winningCombos = zeros.Select(GetNumberOfWinningTimePossibilites);
        return winningCombos.Aggregate(1l, (curr, next) => curr * (long)next);
    }

    protected override object SecondTask()
    {
        var numbers = this.Input
            .Select(line => line
                .Replace(" ", "")
                .Split(":")
                .Skip(1)
                .Select(long.Parse)
                .ToArray())
            .ToArray();
        
        var timeDistance = numbers[0].Zip(numbers[1]);
        var zeros = timeDistance.Select(td => CalculateRoots(td.First, td.Second));

        var winningCombos = zeros.Select(GetNumberOfWinningTimePossibilites);
        return winningCombos.Aggregate(1l, (curr, next) => curr * (long)next);
    }

    private static (double x1, double x2) CalculateRoots(long totalTime, long distanceToBeat)
    {
        var discriminant = totalTime * totalTime - (4 * distanceToBeat);
        var x1 = (-totalTime - Math.Sqrt(discriminant)) / (-2);
        var x2 = (-totalTime + Math.Sqrt(discriminant)) / (-2);
        return (x1, x2);
    }

    private static double GetNumberOfWinningTimePossibilites((double x1, double x2) z)
    {
        var x1 = z.x1 % 1 == 0 ? z.x1 - 1 : Math.Floor(z.x1);
        var x2 = z.x2 % 1 == 0 ? z.x2 + 1 : Math.Ceiling(z.x2);
        return Math.Floor(x1) - Math.Ceiling(x2) + 1;
    }
}
