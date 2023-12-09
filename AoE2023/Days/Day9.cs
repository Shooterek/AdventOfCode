using AoE2023.Utils;

namespace AoE2023;

public class Day9 : StringListDay
{

    protected override object FirstTask()
    {
        var lines = this.Input
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();

        return lines.Select(l => {
            List<int> nums = new();
            var src = l.Reverse().ToArray();
            var x = GetDiff(src);
            nums.Add(src.First());
            while (true) {
                var all = x.ToArray();
                nums.Add(all.First());
                if (all.All(c => c == 0)) {
                    break;
                }

                x = GetDiff(x);
            }

            return nums.Sum();
        }).Sum();
    }

    protected override object SecondTask()
    {
        var lines = this.Input
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();

        return lines.Select(l => {
            List<int> nums = new();
            var src = l.ToArray();
            var x = GetDiff(src);
            nums.Add(src.First());
            while (true) {
                var all = x.ToArray();
                nums.Add(all.First());
                if (all.All(c => c == 0)) {
                    break;
                }

                x = GetDiff(x);
            }

            return nums.Sum();
        }).Sum();
    }

    private IEnumerable<int> GetDiff(IEnumerable<int> src) {
        var enumerator = src.GetEnumerator();
        enumerator.MoveNext();
        var current = enumerator.Current;
        while (enumerator.MoveNext()) {
            yield return -(enumerator.Current - current);
            current = enumerator.Current;
        }
    }
}
