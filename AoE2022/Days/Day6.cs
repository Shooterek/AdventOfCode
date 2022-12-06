using AoE2022.Utils;

public class Day6 : StringListDay
{
    protected override object FirstTask()
    {
        const int DistinctChars = 4;
        return this.Input.Sum(l => {
            for (int i = 0; i < l.Length; i++)
            {
                if (l.Substring(i, DistinctChars).Distinct().Count() == DistinctChars)
                    return i + DistinctChars;
            }

            throw new Exception();
        });
    }

    protected override object SecondTask()
    {
        const int DistinctChars = 14;
        return this.Input.Sum(l => {
            for (int i = 0; i < l.Length; i++)
            {
                if (l.Substring(i, DistinctChars).Distinct().Count() == DistinctChars)
                    return i + DistinctChars;
            }

            throw new Exception();
        });
    }
}
