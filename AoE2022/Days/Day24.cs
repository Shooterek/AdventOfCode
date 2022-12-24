using System.Collections.Concurrent;
using AoE2022.Utils;
using MoreLinq;

public class Day24 : StringListDay
{
    protected override object FirstTask()
    {
        var map = this.Input.Select(line => line.Select(c => c).ToArray()).ToArray();
        var map2 = map[1..^1].Select(line => line[1..^1].ToArray()).ToArray();
        var start = new Point(0, -1);
        var end = new Point(map2[0].Length - 1, map2.Length);
        var x = new Dictionary<Point, List<int>>();
        var xLength = map2[0].Length;
        var yLength = map2.Length;
        var cycleLength = (xLength) * (yLength);

        map2.ForEach((line, yIndex) => line.ForEach((c, xIndex) =>
        {
            x.Add(new(xIndex, yIndex), new List<int>());
        }));

        for (int i = 0; i < cycleLength; i++)
        {
            map2.ForEach((line, yIndex) => line.ForEach((c, xIndex) =>
            {
                var leftIndex = (xIndex + i) % xLength;
                var rightIndex = Mod(xIndex - (i % xLength), xLength);
                var xAvailable = line[leftIndex] != '<' && line[rightIndex] != '>';

                if (!xAvailable)
                    return;

                var col = map2.Select(line => line.ElementAt(xIndex)).ToArray();
                var botIndex = (yIndex + i) % yLength;
                var topIndex = Mod(yIndex - (i % yLength), yLength);
                var yAvailable = col[topIndex] != 'v' && col[botIndex] != '^';

                if (yAvailable)
                    x[new(xIndex, yIndex)].Add(i);
            }));
        }

        var bestPath = FindBestPath(x, start, end, 0);

        return bestPath;
    }

    protected override object SecondTask()
    {
        var map = this.Input.Select(line => line.Select(c => c).ToArray()).ToArray();
        var map2 = map[1..^1].Select(line => line[1..^1].ToArray()).ToArray();
        var start = new Point(0, -1);
        var end = new Point(map2[0].Length - 1, map2.Length);
        var x = new Dictionary<Point, List<int>>();
        var xLength = map2[0].Length;
        var yLength = map2.Length;
        var cycleLength = (xLength) * (yLength);

        map2.ForEach((line, yIndex) => line.ForEach((c, xIndex) =>
        {
            x.Add(new(xIndex, yIndex), new List<int>());
        }));

        for (int i = 0; i < cycleLength; i++)
        {
            map2.ForEach((line, yIndex) => line.ForEach((c, xIndex) =>
            {
                var leftIndex = (xIndex + i) % xLength;
                var rightIndex = Mod(xIndex - (i % xLength), xLength);
                var xAvailable = line[leftIndex] != '<' && line[rightIndex] != '>';

                if (!xAvailable)
                    return;

                var col = map2.Select(line => line.ElementAt(xIndex)).ToArray();
                var botIndex = (yIndex + i) % yLength;
                var topIndex = Mod(yIndex - (i % yLength), yLength);
                var yAvailable = col[topIndex] != 'v' && col[botIndex] != '^';

                if (yAvailable)
                    x[new(xIndex, yIndex)].Add(i);
            }));
        }

        var goal = FindBestPath(x, start, end, 0);
        var back = FindBestPath(x, end, start, goal + 1);
        var goal2 = FindBestPath(x, start, end, back + 1);

        return goal2;
    }

    private int FindBestPath(Dictionary<Point, List<int>> map, Point start, Point end, int startIteration)
    {
        var shortestPath = int.MaxValue;
        var nextMoves = new List<Point>() {
            new(-1, 0),
            new(1, 0),
            new(0, -1),
            new(0, 1),
            new(0, 0),
        };

        var visited = new HashSet<(Point, int)>();

        while (!nextMoves.Select(m => start.Add(m)).Any(m => map.GetValueOrDefault(m)?.Contains(startIteration) == true))
        {
            startIteration++;
        }
        FindPath(start, startIteration);

        void FindPath(Point current, int iteration)
        {
            if (!visited.Add((current, iteration)))
                return;

            if (current == end)
            {
                shortestPath = iteration;
                return;
            }

            foreach (var nextPoint in nextMoves.Select(m => current.Add(m)))
            {
                if (nextPoint == end)
                {
                    FindPath(nextPoint, iteration + 1);
                    return;
                }
                if (map.GetValueOrDefault(nextPoint)?.Contains(iteration) == true)
                {
                    FindPath(nextPoint, iteration + 1);
                }
            }
        }

        return shortestPath - 1;
    }

    private record Point(int X, int Y)
    {
        public Point Add(Point other) => this with { X = this.X + other.X, Y = this.Y + other.Y };
    }

    private int Mod(int x1, int x2)
        => (x1 + x2) % x2;
}
