using AoE2023.Utils;
using MoreLinq;

namespace AoE2023;

public class Day12 : StringListDay
{
    private readonly Dictionary<(string, int), long?> cache = [];
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
                return GetPermutations(condition, springs, 0);
            });

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

                condition = Enumerable.Range(0, 5).SelectMany(c => condition).ToArray();

                var springs = line
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).First();
                springs = string.Join("?", Enumerable.Range(0, 5).Select(s => springs));
                return GetPermutations(condition, springs, 0);
            });

        return numbers.Sum();
    }

    private long GetPermutations(int[] condition, string src, int segmentIndex)
    {
        if (this.cache.GetValueOrDefault((src, segmentIndex)) is { } cachedValue)
            return cachedValue;

        var sum = 0L;
        foreach (var r in GetSegment(src, condition[segmentIndex]))
        {
            if (segmentIndex + 1 == condition.Length)
            {
                if (NoHashes(r))
                {
                    sum += 1;
                }
            }
            else if (AnySpring(r))
            {
                sum += GetPermutations(condition, r, segmentIndex + 1);
            }
        }

        this.cache.Add((src, segmentIndex), sum);
        return sum;

        bool NoHashes(string r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] == '#')
                    return false;
            }

            return true;
        }

        bool AnySpring(string r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                if (r[i] != '.')
                    return true;
            }

            return false;
        }
    }

    private IEnumerable<string> GetSegment(string src, int length)
    {
        for (int i = 0; i <= src.Length - length; i++)
        {
            if (!CanStart(i) || !CanEnd(i + length - 1))
                continue;

            if (src.IndexOf('.', i, length) == -1)
            {
                var segmentEndIndex = i + length;
                if (segmentEndIndex != src.Length && src[segmentEndIndex] == '?')
                    segmentEndIndex += 1;

                var remainder = src[segmentEndIndex..];
                yield return remainder;
            }

            if (AllHashes(src, i, length))
                yield break;
        }

        bool CanStart(int index)
        {
            var indexOfHash = src.IndexOf('#', 0, index);
            return index == 0 || (src[index - 1] == '?' || src[index - 1] == '.') && indexOfHash == -1;
        }

        bool CanEnd(int index) => index == src.Length - 1 || src[index + 1] == '?' || src[index + 1] == '.';

        bool AllHashes(string src, int start, int length)
        {
            for (int i = start; i < start + length; i++)
            {
                if (src[i] != '#')
                    return false;
            }

            return true;
        }
    }
}