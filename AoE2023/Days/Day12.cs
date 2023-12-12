using AoE2023.Utils;
using System.Text.RegularExpressions;
using System.Text;

namespace AoE2023;

public class Day12 : StringListDay
{
    private readonly Regex regex = new Regex(@"^\.*#+[\.|$]");
    private readonly Regex regex2 = new Regex(@"#+");
    protected override object FirstTask()
    {
        var numbers = this.Input
            .Select(line =>
            {
                var condition = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1).First()
                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var springs = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).First();
                var permutations = GetPermutations(springs, condition, 0, 0).Distinct().ToArray();

                return permutations.Where(c => ConditionSucceeded(condition, c)).Count();
                IEnumerable<string> GetPermutations(string src, int[] numbers, int segmentIndex, int startFrom)
                {
                    var results = GetSegment(src, numbers[segmentIndex], startFrom).ToArray();
                    foreach (var r in results)
                    {
                        if (segmentIndex + 1 == numbers.Length)
                        {
                            yield return r.Item1.Replace('?', '.');
                        }
                        else
                        {

                            var nextResults = GetPermutations(r.Item1, numbers, segmentIndex + 1, r.Item2).ToArray();
                            foreach (var nr in nextResults)
                            {
                                yield return nr;
                            }
                        }
                    }
                }
            }).ToArray();

        return numbers.Sum();
    }

    protected override object SecondTask()
    {
        var numbers = this.Input
            .Select((line, index) =>
            {
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
                var permutations = GetPermutations(springs, condition, 0, 0).Distinct().ToArray();

                return permutations.Where(c => ConditionSucceeded(condition, c)).Count();
                IEnumerable<string> GetPermutations(string src, int[] numbers, int segmentIndex, int startFrom)
                {
                    var results = GetSegment(src, numbers[segmentIndex], startFrom).ToArray();
                    foreach (var r in results)
                    {
                        if (segmentIndex + 1 == numbers.Length)
                        {
                            yield return r.Item1.Replace('?', '.');
                        }
                        else
                        {

                            var nextResults = GetPermutations(r.Item1, numbers, segmentIndex + 1, r.Item2).ToArray();
                            foreach (var nr in nextResults)
                            {
                                yield return nr;
                            }
                        }
                    }
                }
            }).ToArray();

        return numbers.Sum();
    }

    private IEnumerable<(string, int)> GetSegment(string src, int length, int startIndex)
    {
        for (int i = startIndex; i <= src.Length - length; i++)
        {
            if (!CanStart(i) || !CanEnd(i + length - 1))
                continue;

            var segment = src.Substring(i, length);
            if (segment.All(c => c != '.'))
            {
                var next = new StringBuilder();
                next.Append(src.Substring(0, i).Replace('?', '.'));

                for (int z = 0; z < length; z++)
                {
                    next.Append('#');
                }
                next.Append(src.Substring(i + length));
                var result = next.ToString();
                var segmentEndIndex = i + length - 1;
                if (segmentEndIndex != src.Length - 1 && src[segmentEndIndex + 1] == '?')
                    result = result.ReplaceAt(segmentEndIndex + 1, 1, ".");

                yield return (result, segmentEndIndex + 1);
            }

        }

        bool CanStart(int index) => index == 0 || src[index - 1] == '?' || src[index - 1] == '.';
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