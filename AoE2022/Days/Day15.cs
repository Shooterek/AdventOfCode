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
        foreach ((var start, var end) in x) {
            result += end - start;
        }

        return result;
    }

    protected override object SecondTask()
    {
        var min = 0;
        var max = 20;
        //var max = 4_000_000;
        var sensorRanges = new List<SensorRange>();
        var ranges = new List<(int, int)>();
        foreach (var line in this.Input)
        {
            var n = NumberPattern.Matches(line).Select(m => m.Captures[0].Value).Select(int.Parse).ToList();
            var station = new Point(n[0], n[1]);
            var distance = ManhattanDistance(n[0], n[1], n[2], n[3]);
            sensorRanges.Add(new(n[0] - distance, n[0] + distance, n[1] - distance, n[1] + distance, n[0], n[1], distance));
        }

        var ys = sensorRanges.SelectMany(r => new int[] {r.Y1, r.Y2}).Order().ToList();
        var xs = sensorRanges.SelectMany(r => new int[] {r.X1, r.X2}).Order().ToList();
        var map = new bool[ys.Count][];
        for (int i = 0; i < map.Length; i++)
        {
            map[i] = new bool[map.Length];
        }

        for (int i = 0; i < ys.Count; i++)
        {
            for (int j = 0; j < xs.Count; j++)
            {
                var val1 = ys[i];
                var val2 = xs[j];

                var res = sensorRanges.FirstOrDefault(r => {
                    return ManhattanDistance(r.X3, r.Y3, val1, val2) <= r.Dist;
                });
                map[i][j] = sensorRanges.Any(r => {
                    return ManhattanDistance(r.X3, r.Y3, val1, val2) <= r.Dist;
                });
            }
        }

        
        for (int i = 0; i < ys.Count; i++)
        {
            for (int j = 0; j < xs.Count; j++)
            {
                if (map[i][j] == false && ys[i] >= 0 && ys[i] <= max && xs[j] >= 0 && xs[j] <= max)
                    Console.WriteLine($"{ys[i]} {xs[j]}");
                    // Console.WriteLine($"{(double)ys[i] * 4_000_000 + xs[j]}");
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
}
