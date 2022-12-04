using AoE2022.Utils;

public class Day3 : StringListDay
{
    protected override object FirstTask()
    {
        return this.Input.Select(l => {
            var first = l[0..(l.Length/2)];
            var second = l[(l.Length/2)..];

            var commons = first.Intersect(second);
            return commons.Select(l => {
                if (Char.IsUpper(l))
                    return (int)l - 38;

                return (int)l - 96;
            }).Sum();
        }).Sum();
    }

    protected override object SecondTask()
    {
        var counter = 0;
        var sum = 0;
        while (counter * 3 < this.Input.Count()) {
            var group = this.Input.Skip((counter * 3)).Take(3).ToList();
            var letter = group[0].Intersect(group[1]).Intersect(group[2]).Single();

            sum += letter switch {
                >= 'a' and <= 'z' => (int)letter - 96,
                >= 'A' and <= 'Z' => (int)letter - 38,
            };
            counter++;
        }

        return sum;
    }
}
