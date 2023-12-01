using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day1 : StringListDay
{
    private static Dictionary<string, int> digitDictionary = new Dictionary<string, int>()
        {
            {"zero", 0},
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };

    protected override object FirstTask()
    {
        return this.Input.Select(line => {
            char first = line.First(c => Char.IsDigit(c));
            char last = line.Last(c => Char.IsDigit(c));
            return int.Parse($"{first}{last}");
        })
        .Sum();
    }

    protected override object SecondTask()
    {
        var regex = new Regex(@"(?=(zero|one|two|three|four|five|six|seven|eight|nine|\d))");
        return this.Input.Select(line =>
        {
            var matches = regex.Matches(line);
            return $"{GetNumber(matches.First())}{GetNumber(matches.Last())}";
        })
        .Select(int.Parse)
        .Sum();
    }

    private static int GetNumber(Match match)
        => int.TryParse(match.Groups[1].Value, out var number) ? number : digitDictionary[match.Groups[1].Value];
}
