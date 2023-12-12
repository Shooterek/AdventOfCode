using AoE2023.Utils;
using System.Text.RegularExpressions;

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

                var permutations = GetPermutations(line, 0, 0, "");

                Console.WriteLine(line);
                return permutations.Where(c => ConditionSucceeded(condition, c)).Count();
                IEnumerable<string> GetPermutations(string src, int startFrom, int segment, string prefix)
                {
                    if (SegmentMatched(condition, src, segment, out var index) is { } length)
                    {
                        prefix += src.Substring(0, length);
                        src = src.ReplaceAt(0, length, "");
                        segment++;
                        startFrom = 0;
                    }
                    var nextIndex = startFrom + 1;
                    if (src.All(c => c != '?'))
                        yield return prefix + src;

                    foreach (var c in src.Select((s, i) => (s, i)).Where(s => s.i >= startFrom && s.s == '?'))
                    {
                        var next = src.ReplaceAt(c.i, 1, "#");
                        var next2 = src.ReplaceAt(c.i, 1, ".");

                        if (!ConditionFailed(condition, next, segment))
                        {
                            var result = GetPermutations(next, c.i + 1, segment, prefix).ToArray();
                            foreach (var r in result)
                            {
                                yield return r;
                            }
                        }

                        if (!ConditionFailed(condition, next2, segment))
                        {
                            var result = GetPermutations(next2, c.i + 1, segment, prefix).ToArray();
                            foreach (var r in result)
                            {
                                yield return r;
                            }
                        }
                    }
                }
            }).ToArray();

        return numbers.Sum();
    }

    protected override object SecondTask()
    {
        return null;
    }

    private int? SegmentMatched(int[] numbers, string src, int segment, out int? index)
    {
        index = null;
        var matches = this.regex.Match(src);
        if (!matches.Success)
            return null;
        if (matches.Value.Count(c => c == '#') == numbers[segment])
        {
            index = matches.Index;
            return matches.Length;
        }

        return null;
    }

    private bool ConditionFailed(int[] numbers, string src, int segment)
    {
        var matches = this.regex.Match(src);
        if (!matches.Success)
            return false;

        if (segment >= numbers.Length )
            return true;

        if (matches.Value.Length > numbers[segment])
            return false;

        var missingHashes = numbers.Skip(segment).Sum();
        var allHashes = src.Count(c => c == '#');
        var allQuestionsMarks = src.Count(c => c == '?');
        return allHashes > missingHashes || allHashes + allQuestionsMarks < missingHashes;
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