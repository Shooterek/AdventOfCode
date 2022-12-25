using AoE2022.Utils;

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

        var bestPath = FindBestPath(map2, xLength, yLength, start, end, 0);

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

        var goal = FindBestPath(map2, xLength, yLength, start, end, 0);
        var back = FindBestPath(map2, xLength, yLength, end, start, goal);
        var goal2 = FindBestPath(map2, xLength, yLength, start, end, back);

        return goal2;
    }

    private int FindBestPath(char[][] map, int xLength, int yLength, Point start, Point end, int startIteration)
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
        for (int i = 0; i < 24; i++)
        {
            FindPath(start, startIteration + i);
            visited.Clear();
        }
        return shortestPath;

        void FindPath(Point current, int iteration)
        {
            if (iteration >= 3500 || iteration > shortestPath)
                return;
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
                if (MoveAvailable(nextPoint, iteration + 1))
                {
                    FindPath(nextPoint, iteration + 1);
                }
            }
        }

        bool MoveAvailable(Point m, int i)
        {
            if (m.X < 0 || m.Y < 0 || m.X >= xLength || m.Y >= yLength)
                return false;

            var line = map[m.Y];
            var leftIndex = (m.X + i) % xLength;
            var rightIndex = Mod(m.X - (i % xLength), xLength);
            var xAvailable = line[leftIndex] != '<' && line[rightIndex] != '>';

            if (!xAvailable)
                return false;

            var col = map.Select(line => line.ElementAt(m.X)).ToArray();
            var botIndex = (m.Y + i) % yLength;
            var topIndex = Mod(m.Y - (i % yLength), yLength);
            var yAvailable = col[topIndex] != 'v' && col[botIndex] != '^';

            return yAvailable;
        }
    }

    private record Point(int X, int Y)
    {
        public Point Add(Point other) => this with { X = this.X + other.X, Y = this.Y + other.Y };
    }

    private int Mod(int x1, int x2)
        => (x1 + x2) % x2;
}
