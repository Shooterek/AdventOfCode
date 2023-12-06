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
                .Select(int.Parse)
                .ToArray())
            .ToArray();
        
        var timeDistance = numbers[0].Zip(numbers[1]);
        var zeros = timeDistance.Select(td => {
            var discriminant = td.First * td.First - (4 * td.Second);
            var x1 = (-td.First - Math.Sqrt(discriminant))/(-2);
            var x2 = (-td.First + Math.Sqrt(discriminant))/(-2);
            return (x1, x2, t: td.First, d: td.Second);
        });


        var winningCombos = zeros.Select(z => {
            var x1 = z.x1 % 1 == 0 ? z.x1 - 1 : Math.Floor(z.x1);
            var x2 = z.x2 % 1 == 0 ? z.x2 + 1 : Math.Ceiling(z.x2);
            return Math.Floor(x1) - Math.Ceiling(x2) + 1;
        });
        return winningCombos.Aggregate(1, (curr, next) => curr * (int)next);
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
        var zeros = timeDistance.Select(td => {
            var discriminant = td.First * td.First - (4 * td.Second);
            var x1 = (-td.First - Math.Sqrt(discriminant))/(-2);
            var x2 = (-td.First + Math.Sqrt(discriminant))/(-2);
            return (x1, x2, t: td.First, d: td.Second);
        });


        var winningCombos = zeros.Select(z => {
            var x1 = z.x1 % 1 == 0 ? z.x1 - 1 : Math.Floor(z.x1);
            var x2 = z.x2 % 1 == 0 ? z.x2 + 1 : Math.Ceiling(z.x2);
            return Math.Floor(x1) - Math.Ceiling(x2) + 1;
        });
        return winningCombos.Aggregate(1, (curr, next) => curr * (int)next);
    }
}
