using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day13 : StringBatchesDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        return this.Input.Select((batch, index) =>
        {
            var left = new ArrayValue(batch.Split("\r ")[0]);
            var right = new ArrayValue(batch.Split("\r ")[1]);
            
            var r = left.Compare(right);
            return r == 1 ? index + 1 : 0;
        }).Sum();
    }

    protected override object SecondTask()
    {
        var start = "[[2]]";
        var end = "[[6]]";
        var allPackets = this.Input
            .SelectMany(b => b.Split("\r "))
            .ToList();

        allPackets.Add(start);
        allPackets.Add(end);

        allPackets = allPackets.OrderDescending(new Comparer()).ToList();

        return (allPackets.FindIndex(p => p == start) + 1) * (allPackets.FindIndex(p => p == end) + 1);
    }

    private class ArrayValue
    {
        public ArrayValue(int i)
        {
            this.Values.Enqueue(i);
        }

        public ArrayValue(string array)
        {
            var val = array[1..^1];
            while (val.Length > 0)
            {
                if (val[0] == '[')
                {
                    var index = 1;
                    for (int level = 1; index < val.Length && level > 0; index++)
                    {
                        if (val[index] == '[')
                            level++;
                        
                        if (val[index] == ']')
                            level--;
                    }
                    var nextArr = val[0..index];
                    Values.Enqueue(new ArrayValue(nextArr));
                    val = val[nextArr.Length..];
                }
                else if (char.IsDigit(val[0]))
                {
                    var number = NumberPattern.Match(val);
                    Values.Enqueue(int.Parse(number.Captures[0].Value));
                    val = val[number.Length..];
                }
                else
                {
                    val = val[1..];
                }
            }
        }

        public Queue<object> Values { get; set; } = new();

        

    public int Compare(ArrayValue right)
    {
        var result = 0;
        while (result == 0)
        {
            var leftOk = this.Values.TryDequeue(out var leftValue);
            var rightOk = right.Values.TryDequeue(out var rightValue);
            if (!leftOk && rightOk)
                return 1;
            if (leftOk && !rightOk)
                return -1;
            if (!leftOk && !rightOk)
                return 0;

            result = (leftValue, rightValue) switch
            {
                (ArrayValue l, ArrayValue r) => l.Compare(r),
                (ArrayValue l, int r) => l.Compare(new ArrayValue(r)),
                (int l, ArrayValue r) => new ArrayValue(l).Compare(r),
                (int l, int r) => Compare(l, r),
            };
        }

        return result;
    }

    private int Compare(int left, int right)
    {
        if (left < right)
            return 1;
        if (right < left)
            return -1;

        return 0;
    }
}

    private class Comparer : IComparer<string>
    {
        public int Compare(string? x, string? y) => new ArrayValue(x).Compare(new ArrayValue(y));
    }
}