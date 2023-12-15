using AoE2023.Utils;
using System.Text;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day15 : EntireStringDay
{
    protected override object FirstTask()
    {
        var sum = 0;
        var currentValue = 0;
        foreach (var c in this.Input) {
            if (c == ',') {
                sum += currentValue;
                currentValue = 0;
                continue;
            }
            currentValue = (currentValue + c) * 17 % 256;
        }

        return sum + currentValue;
    }

    protected override object SecondTask()
    {
        return null;
    }
}
