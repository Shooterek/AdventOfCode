using System.Diagnostics;
using AoE2022.Utils;

public class Day12 : StringListDay
{
    private List<(int, int)> ind = new(){
        (-1, 0),
        (1, 0),
        (0, 1),
        (0, -1),
    };
    protected override object FirstTask()
    {
        var map = this.Input.Select((line, y) => line.Select((c, x) => new Point(x, y, GetValue(c), c)).ToArray()).ToArray();
        var start = map.SelectMany(p => p).First(p => p.Letter == 'S');
        var end = map.SelectMany(p => p).First(p => p.Letter == 'E');

        var newlyReached = new List<Point>() { start };
        var count = -1;
        while(!map.SelectMany(p => p).First(p => p.Letter == 'E').Reached)
        {
            foreach (var p in newlyReached)
            {
                map[p.Y][p.X].Reached = true;
            }
            count++;
            newlyReached = newlyReached.SelectMany(r => GetNeighbours(r, map)).Distinct().ToList();
        }
        return count;
    }

    protected override object SecondTask()
    {
        var map = this.Input.Select((line, y) => line.Select((c, x) => new Point(x, y, GetValue(c), c)).ToArray()).ToArray();
        var start = map.SelectMany(p => p).First(p => p.Letter == 'S');
        var end = map.SelectMany(p => p).First(p => p.Letter == 'E');

        var newlyReached = map.SelectMany(p => p).Where(p => p.Letter == 'a');
        var count = -1;
        while(!map.SelectMany(p => p).Where(p => p.Letter == 'E').Any(p => p.Reached))
        {
            foreach (var p in newlyReached)
            {
                map[p.Y][p.X].Reached = true;
            }
            count++;
            newlyReached = newlyReached.SelectMany(r => GetNeighbours(r, map)).Distinct().ToList();
        }
        return count;
    }

    private record Point(int X, int Y, int Height, char Letter) {
        public bool Reached { get; set; } = false;
    }

    private int GetValue(char c) {
        var val = c switch {
            'S' => 'a',
            'E' => 'z',
            _ => c,
        };
        return Convert.ToInt32(val);
    }

    private List<Point> GetNeighbours(Point p, Point[][] map) {
        var stack = new List<Point>();
        foreach (var i in this.ind) {
            if (p.X + i.Item1 >= map[0].Length || p.X + i.Item1 < 0 || p.Y + i.Item2 >= map.Length || p.Y + i.Item2 < 0)
            continue;

            var point = map[p.Y + i.Item2][p.X + i.Item1];
            if (point.Height <= p.Height + 1 && !point.Reached)
                stack.Add(point);
        }

        return stack;
    }
}