using AoE2023.Utils;

namespace AoE2023;

public class Day18 : StringListDay
{
    protected override object FirstTask()
    {
        var instructions = this.Input.Select(line =>
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            return new PaintInstruction(parts[0].First(), int.Parse(parts[1]));
        }).ToArray();

        var points = new HashSet<Coordinates2>();
        var start = new Coordinates2(0, 0);
        points.Add(start);
        foreach (var instr in instructions)
        {
            for (int i = 0; i < instr.Length; i++)
            {
                var change = instr.Direction switch
                {
                    'R' => (1, 0),
                    'L' => (-1, 0),
                    'U' => (0, -1),
                    'D' => (0, 1),
                };

                start = start with { X = start.X + change.Item1, Y = start.Y + change.Item2 };
                points.Add(start);
            }
        }

        var xOffset = points.MinBy(sq => sq.X).X;
        var yOffset = points.MinBy(sq => sq.Y).Y;

        points = points.Select(p => p with { X = p.X - xOffset, Y = p.Y - yOffset }).ToHashSet();

        var maxX = points.MaxBy(sq => sq.X).X + 1;
        var maxY = points.MaxBy(sq => sq.Y).Y + 1;

        var wallSquares = new HashSet<Coordinates2>();
        foreach (var x in Enumerable.Range(0, (int)maxX))
        {
            wallSquares.Add(new(x, 0));
            wallSquares.Add(new(x, maxY - 1));
        }
        foreach (var y in Enumerable.Range(0, (int)maxY))
        {
            wallSquares.Add(new(0, y));
            wallSquares.Add(new(maxX - 1, y));
        }

        wallSquares = wallSquares.Except(points).ToHashSet();
        var nextSquares = wallSquares.ToHashSet();
        while (nextSquares.Any())
        {
            var next = nextSquares.SelectMany(sq => GetNeighbours(sq, maxX, maxY)).Distinct().ToArray();
            nextSquares.Clear();
            foreach (var sq in next)
            {
                if (!points.Contains(sq) && wallSquares.Add(sq))
                {
                    nextSquares.Add(sq);
                }
            }
        }

        return maxX * maxY - wallSquares.Count();
    }

    protected override object SecondTask()
    {
        var instructions = this.Input.Select(line =>
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            return new PaintInstruction(parts[2][7], long.Parse(parts[2][2..7], System.Globalization.NumberStyles.HexNumber));
        }).ToArray();

        var points = new HashSet<Coordinates2>();
        var start = new Coordinates2(0, 0);
        points.Add(start);
        foreach (var instr in instructions)
        {
            var change = instr.Direction switch
            {
                '0' => (1, 0),
                '2' => (-1, 0),
                '3' => (0, -1),
                '1' => (0, 1),
            };

            start = start with { X = start.X + change.Item1 * instr.Length, Y = start.Y + change.Item2 * instr.Length };
            points.Add(start);
        }


        var xOrder = points.Select(p => p.X).Order().Distinct().ToList();
        var yOrder = points.Select(p => p.Y).Order().Distinct().ToList();

        IEnumerable<long> n = xOrder.SelectMany(p => new long[] { p - 1, p + 1 }).Where(p => p > xOrder[0] && p < xOrder.Last()).ToArray();
        xOrder.AddRange(n);

        n = yOrder.SelectMany(p => new long[] { p - 1, p + 1 }).Where(p => p > yOrder[0] && p < yOrder.Last()).ToArray();
        yOrder.AddRange(n);

        xOrder = [.. xOrder.Distinct().Order()];
        yOrder = [.. yOrder.Distinct().Order()];

        foreach (var instr in instructions)
        {
            var change = instr.Direction switch
            {
                '0' => (1, 0),
                '2' => (-1, 0),
                '3' => (0, -1),
                '1' => (0, 1),
            };
        }

        var pointList = points.ToArray();
        var pointsPrim = new HashSet<Coordinates2>();
        for (int i = 0; i < points.Count; i++)
        {
            var currentX = xOrder.FindIndex(c => pointList[i].X == c);
            var currentY = yOrder.FindIndex(c => pointList[i].Y == c);
            var nextX = xOrder.FindIndex(c => pointList[(i + 1) % (points.Count - 1)].X == c);
            var nextY = yOrder.FindIndex(c => pointList[(i + 1) % (points.Count - 1)].Y == c);

            var xRange = Enumerable.Range(Math.Min(currentX, nextX), Math.Abs(currentX - nextX) + 1);
            var yRange = Enumerable.Range(Math.Min(currentY, nextY), Math.Abs(currentY - nextY) + 1);
            foreach (var x in xRange)
            {
                foreach (var y in yRange)
                {
                    pointsPrim.Add(new(x, y));
                }
            }
        }

        var xOffset = pointsPrim.MinBy(sq => sq.X).X;
        var yOffset = pointsPrim.MinBy(sq => sq.Y).Y;

        pointsPrim = pointsPrim.Select(p => p with { X = p.X - xOffset, Y = p.Y - yOffset }).ToHashSet();

        var maxX = xOrder.Count + 1;
        var maxY = yOrder.Count + 1;

        var wallSquares = new HashSet<Coordinates2>();
        foreach (var x in Enumerable.Range(0, maxX))
        {
            wallSquares.Add(new(x, 0));
            wallSquares.Add(new(x, maxY - 1));
        }
        foreach (var y in Enumerable.Range(0, maxY))
        {
            wallSquares.Add(new(0, y));
            wallSquares.Add(new(maxX - 1, y));
        }

        wallSquares = wallSquares.Except(pointsPrim).ToHashSet();
        var nextSquares = wallSquares.ToHashSet();
        while (nextSquares.Any())
        {
            var next = nextSquares.SelectMany(sq => GetNeighbours(sq, maxX, maxY)).Distinct().ToArray();
            nextSquares.Clear();
            foreach (var sq in next)
            {
                if (!pointsPrim.Contains(sq) && wallSquares.Add(sq))
                {
                    nextSquares.Add(sq);
                }
            }
        }

        long allSquares = (points.MaxBy(p => p.X).X + 1) * (points.MaxBy(p => p.Y).Y + 1);

        var temp = new List<long>();
        DisplayJaggedArray(xOrder.Count, yOrder.Count, wallSquares.ToHashSet());

        for (int x = 0; x < xOrder.Count; x++) {
            for (int y = 0; y < yOrder.Count; y++) {
                if (wallSquares.Contains(new(x, y))) {
                    var xCount = (xOrder[x]) - (x > 0 ? xOrder[x - 1] + 1 : 0);
                    var yCount = (yOrder[y]) - (y > 0 ? yOrder[y - 1] + 1 : 0);
                    temp.Add(yCount);
                    temp.Add(xCount);
                    allSquares -= xCount * yCount + 1;
                }
            }
        }

        var t = temp.Sum();
        Console.WriteLine(t);
        return allSquares- t;
    }

    private IEnumerable<Coordinates2> GetNeighbours(Coordinates2 current, long maxX, long maxY)
    {
        return new Coordinates2[] { new(0, -1), new(0, 1), new(1, 0), new(-1, 0) }
            .Select(pd => new Coordinates2(current.X + pd.X, current.Y + pd.Y))
            .Where(c => c.X >= 0 && c.X < maxX && c.Y >= 0 && c.Y < maxY);
    }

    static void DisplayJaggedArray(int maxX, int maxY, HashSet<Coordinates2> set)
    {
        for (int i = 0; i < maxY; i++)
        {
            for (int j = 0; j < maxX; j++)
            {
                char displayChar = set.Contains(new(j, i)) ? '#' : '.';
                Console.Write(displayChar + " ");
            }
            Console.WriteLine();
        }
    }
}

public record PaintInstruction(char Direction, long Length);

public record Coordinates2(long X, long Y);