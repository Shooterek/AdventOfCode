using AoE2022.Utils;

public class Day6 : EntireStringDay
{
    protected override object FirstTask()
    {
        return CountProcessedCharacters(this.Input, 4);
    }

    protected override object SecondTask()
    {
        return CountProcessedCharacters(this.Input, 14);
    }

    private int CountProcessedCharacters(string source, int distinctChars) {
        for (int i = 0; i < source.Length; i++)
        {
            if (source.Substring(i, distinctChars).Distinct().Count() == distinctChars)
                return i + distinctChars;
        }

        throw new Exception();
    }
}
