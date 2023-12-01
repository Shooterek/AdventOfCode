using AoE2023.Utils;

namespace AoE2023;

public class Day1 : StringBatchesDay
{
    protected override object FirstTask()
    {
        return this.Input.Select(l => l.Split("\r ").Select(int.Parse).Sum()).Max();
    }

    protected override object SecondTask()
    {
        return this.Input.Select(l => l.Split("\r ").Select(int.Parse).Sum()).OrderDescending().Take(3).Sum();
    }
}
