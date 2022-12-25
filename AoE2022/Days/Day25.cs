using AoE2022.Utils;

public class Day25 : StringListDay
{
    protected override object FirstTask()
    {
        var sum = this.Input.Select(GetDecimalValue).Sum();
        var digits = GetNumberOfDigits(sum);
        var length = digits;
        var result = "";
        while (sum != 0)
        {
            var obj = FindVal(sum, digits - 1);
            result += obj.Item1;
            sum = obj.Item2;
            digits--;
        }

        return result.PadRight((int)length, '0');
    }

    private (char, long) FindVal(long sum, long pow)
    {
        return new char[] { '0', '1', '2', '-', '=' }.Select(c =>
        {
            var val = sum - GetVal(c) * (long)Math.Pow(5, pow);
            return (c, val);
        }).MinBy(c => Math.Abs(c.val));
    }


    private long GetDecimalValue(string arg1)
    {
        return arg1.Reverse().Select((c, index) => GetVal(c) * (long)Math.Pow(5, index)).Sum();
    }

    private long GetVal(char c)
        => c switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '-' => -1,
            '=' => -2,
        };

    private long GetNumberOfDigits(long number)
    {
        number = Math.Abs(number);
        var places = 0;
        var x = 0L;
        while (x < number)
        {
            x += (long)Math.Pow(5, places++) * 2;
        }

        return places;
    }

    protected override object SecondTask()
    {
        return "COMPLETE";
    }
}
