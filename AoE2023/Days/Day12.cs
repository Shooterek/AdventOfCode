using AoE2023.Utils;
using System.Text.RegularExpressions;
using System.Text;
using MoreLinq;

namespace AoE2023;

public class Day12 : StringListDay
{
    private readonly Regex regex2 = new Regex(@"#+");
    private readonly Dictionary<(string, int, int), long?> cache = new();
    protected override object FirstTask()
    {
        var numbers = this.Input
            .Select(line =>
            {
                this.cache.Clear();
                var condition = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1).First()
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var springs = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).First();
                var permutations = GetPermutations(condition, springs, condition, 0);
                Console.WriteLine(permutations);

                return permutations;
            }).ToArray();

        return numbers.Sum();
    }

    protected override object SecondTask()
    {
        var numbers = this.Input
            .Select((line, index) =>
            {
                this.cache.Clear();
                var condition = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1).First()
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                Console.WriteLine(index);
                condition = Enumerable.Range(0, 5).SelectMany(c => condition).ToArray();

                var springs = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).First();
                springs = string.Join("?", Enumerable.Range(0, 5).Select(s => springs));
                var permutations = GetPermutations(condition, springs, condition, 0);
                Console.WriteLine(permutations);

                return permutations;
            }).ToArray();

        return numbers.Sum();
    }

    private long GetPermutations(int[] condition, string src, int[] numbers, int segmentIndex)
    {
        var results = GetSegment(src, numbers[segmentIndex]).ToArray();
        var sum = 0L;
        foreach (var r in results)
        {
            if (segmentIndex + 1 == numbers.Length)
            {
                if (r.All(r => r != '#')) {
                    sum += 1;
                }
            }
            else if (r.Any(c => c == '?' || c == '#'))
            {
                sum += GetPermutations(condition, r, numbers, segmentIndex + 1);
            }
        }

        return sum;
    }

    private IEnumerable<string> GetSegment(string src, int length)
    {
        for (int i = 0; i <= src.Length - length; i++)
        {
            if (!CanStart(i) || !CanEnd(i + length - 1))
                continue;

            var segment = src.Substring(i, length);

            if (segment.All(c => c != '.'))
            {
                var segmentEndIndex = i + length;
                if (segmentEndIndex != src.Length && src[segmentEndIndex] == '?')
                    segmentEndIndex += 1;

                var remainder = src[segmentEndIndex..];
                yield return remainder;
            }
            if (segment.All(c => c == '#'))
                yield break;
        }

        bool CanStart(int index) => index == 0 || (src[index - 1] == '?' || src[index - 1] == '.') && src[0..index].All(c => c != '#');
        bool CanEnd(int index) => index == src.Length - 1 || src[index + 1] == '?' || src[index + 1] == '.';
    }

    private bool ConditionSucceeded(int[] numbers, string src)
    {
        var matches = this.regex2.Matches(src);
        var index = 0;
        var matchCount = matches.Count();
        if (matchCount != numbers.Length)
            return false;

        foreach (Match match in matches)
        {
            if (match.Value.Length != numbers[index++])
                return false;
        }
        return true;
    }
}

public static class StringExtensions
{
    public static string ReplaceAt(this string str, int index, int length, string replace)
    {
        return str.Remove(index, Math.Min(length, str.Length - index))
                .Insert(index, replace);
    }
}