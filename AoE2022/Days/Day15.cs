using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day15 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"-?\d+");

    protected override object FirstTask()
    {
        var targetLine = 2000000;
        var ranges = new List<(int, int)>();
        foreach (var line in this.Input)
        {
            var n = NumberPattern.Matches(line).Select(m => m.Captures[0].Value).Select(int.Parse).ToList();
            var station = new Point(n[0], n[1]);
            var distance = ManhattanDistance(n[0], n[1], n[2], n[3]);

            var distanceToTargetLine = Math.Abs(station.Y - targetLine);
            if (distanceToTargetLine > distance)
                continue;

            var sideLength = distance - distanceToTargetLine;
            ranges.Add((station.X - sideLength, station.X + sideLength));
        }

        var x = ranges.OrderBy(r => r.Item1).ToList();

        for (int i = 1; i < x.Count; i++)
        {
            var previous = x[i - 1];
            var current = x[i];

            if (previous.Item2 >= current.Item1)
            {
                x.Remove(previous);
                x[x.FindIndex(v => v == current)] = (previous.Item1, Math.Max(previous.Item2, current.Item2));
                i--;
            }
        }

        var result = 0;
        foreach ((var start, var end) in x)
        {
            result += end - start;
        }

        return result;
    }

    protected override object SecondTask()
    {
        int min = 0, max = 4_000_000;
        for (int i = 0; i < max; i++)
        {
            if (GetBusySquares(i, min, max) is int x)
            {
                return (long)x * max + i;
            }
        }

        return 0;
    }

    private record Point(int X, int Y);

    private record SensorRange(int X1, int X2, int Y1, int Y2, int X3, int Y3, int Dist);

    private int ManhattanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    private int? GetBusySquares(int targetLine, int mapMin, int mapMax)
    {
        var ranges = new List<(int, int)>();
        foreach (var line in this.Input)
        {
            var n = NumberPattern.Matches(line).Select(m => m.Captures[0].Value).Select(int.Parse).ToList();
            var station = new Point(n[0], n[1]);
            var distance = ManhattanDistance(n[0], n[1], n[2], n[3]);

            var distanceToTargetLine = Math.Abs(station.Y - targetLine);
            if (distanceToTargetLine > distance)
                continue;

            var sideLength = distance - distanceToTargetLine;
            ranges.Add((station.X - sideLength, station.X + sideLength));
        }
        var index = 0;
        while (index < ranges.Count)
        {
            if (ranges[index].Item1 < 0 && ranges[index].Item2 < 0)
                ranges[index] = (0, 0);

            if (ranges[index].Item1 > mapMax && ranges[index].Item2 > mapMax)
                ranges[index] = (0, 0);

            if (ranges[index].Item1 < 0 && ranges[index].Item2 > mapMax)
                ranges[index] = (0, mapMax);

            if (ranges[index].Item1 < 0)
                ranges[index] = (0, ranges[index].Item2);

            if (ranges[index].Item2 > mapMax)
                ranges[index] = (ranges[index].Item1, mapMax);

            index++;
        }
        var x = ranges.OrderBy(r => r.Item1).ToList();

        for (int i = 1; i < x.Count; i++)
        {
            var previous = x[i - 1];
            var current = x[i];

            if (previous.Item2 >= current.Item1)
            {
                x.Remove(previous);
                x[x.FindIndex(v => v == current)] = (previous.Item1, Math.Max(previous.Item2, current.Item2));
                i--;
            }
        }
        if (x.Count != 1)
        {
            return x[0].Item2 + 1;
        }

        return null;
    }
}
