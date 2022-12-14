using AoE2022.Utils;

public class Day14 : StringListDay
{
    protected override object FirstTask()
    {
        var rocks = BuildMap();

        var minLeft = rocks.MinBy(r => r.X).X;
        var maxRight = rocks.MaxBy(r => r.X).X;

        var counter = 0;
        var outside = false;
        while (!outside) {
            var grain = new Point(500, 0);
            var rested = false;
            while (!rested && !outside) {
                if (!rocks.Contains(new Point(grain.X, grain.Y + 1))) {
                    grain = new Point(grain.X, grain.Y + 1);
                }
                else if (!rocks.Contains(new Point(grain.X - 1, grain.Y + 1))) {
                    grain = new Point(grain.X - 1, grain.Y + 1);
                }
                else if (!rocks.Contains(new Point(grain.X + 1, grain.Y + 1))) {
                    grain = new Point(grain.X + 1, grain.Y + 1);
                }
                else {
                    rested = true;
                }

                if (grain.X < minLeft || grain.Y > maxRight) {
                    outside = true;
                }
            }
            if (!outside) {
                counter++;
                rocks.Add(grain);
            }
        }

        return counter;
    }

    protected override object SecondTask()
    {
        var rocks = BuildMap();
        var maxHeight = 1 + rocks.MaxBy(r => r.Y).Y;

        var counter = 0;
        var outside = false;
        while (!outside) {
            var grain = new Point(500, 0);
            var rested = false;
            while (!rested && !outside)
            {
                var nextPosition = CheckSpace(rocks, grain, 0, 1)
                    ?? CheckSpace(rocks, grain, -1, 1)
                    ?? CheckSpace(rocks, grain, 1, 1);

                if (nextPosition == null || nextPosition.Y == maxHeight) {
                    rested = true;
                }
                grain = nextPosition ?? grain;
            }
            if (rocks.Contains(grain)) {
                outside = true;
            }
            else {
                counter++;
                rocks.Add(grain);
            }
        }

        return counter;
    }

    private HashSet<Point> BuildMap()
    {
        var rocks = new HashSet<Point>();
        foreach (var line in this.Input) {
            var coords = line.Split("->");

            for (int i = 0; i < coords.Length - 1; i++)
            {
                var start = coords[i].Split(",").Select(int.Parse).ToArray();
                var end = coords[i + 1].Split(",").Select(int.Parse).ToArray();

                if (start[0] - end[0] != 0) {
                    var min = Math.Min(start[0], end[0]);
                    var max = Math.Max(start[0], end[0]);

                    for (int j = min; j <= max; j++)
                    {
                        rocks.Add(new(j, start[1]));
                    }
                }
                else {
                    var min = Math.Min(start[1], end[1]);
                    var max = Math.Max(start[1], end[1]);

                    for (int j = min; j <= max; j++)
                    {
                        rocks.Add(new(start[0], j));
                    }
                }
            }
        }

        return rocks;
    }

    private Point? CheckSpace(HashSet<Point> map, Point @from, int xDiff, int yDiff) {
        var point = @from with { X = from.X + xDiff, Y = from.Y + yDiff };
        return map.Contains(point) ? null : point;
    }

    private record Point(int X, int Y);
}
