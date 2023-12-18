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

        var points = new HashSet<Coordinates>();
        var start = new Coordinates(0, 0);
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

        var wallSquares = new HashSet<Coordinates>();
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
            var hex = int.Parse(parts[2][2..7], System.Globalization.NumberStyles.HexNumber);
            return new PaintInstruction(parts[2][7], hex);
        }).ToArray();

        var points = new HashSet<Coordinates>();
        var start = new Coordinates(0, 0);
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

        var pointsPrim = new HashSet<Coordinates>();
        start = new Coordinates(0, 0);
        pointsPrim.Add(start);
        foreach (var instr in instructions)
        {
            var change = instr.Direction switch
            {
                '0' => (1, 0),
                '2' => (-1, 0),
                '3' => (0, -1),
                '1' => (0, 1),
            };

            start = start with { X = start.X + change.Item1, Y = start.Y + change.Item2 };
            pointsPrim.Add(start);
        }

        var xOffset = pointsPrim.MinBy(sq => sq.X).X;
        var yOffset = pointsPrim.MinBy(sq => sq.Y).Y;

        pointsPrim = pointsPrim.Select(p => p with { X = p.X - xOffset, Y = p.Y - yOffset }).ToHashSet();

        var maxX = pointsPrim.MaxBy(sq => sq.X).X + 1;
        var maxY = pointsPrim.MaxBy(sq => sq.Y).Y + 1;

        var wallSquares = new HashSet<Coordinates>();
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

        var xOrder = points.Select(p => p.X).Order().ToArray();
        var yOrder = points.Select(p => p.Y).Order().ToArray();
        long allSquares = (points.MaxBy(p => p.X).X + 1) * (points.MaxBy(p => p.Y).Y + 1);
        foreach (var wall in wallSquares) {
            allSquares -= ((long)Math.Abs(xOrder[wall.X + 1] - xOrder[wall.X])) * ((long)Math.Abs(yOrder[wall.Y + 1] - yOrder[wall.Y]));
        }

        return allSquares;
    }

    private IEnumerable<Coordinates> GetNeighbours(Coordinates current, int maxX, int maxY)
    {
        return new Coordinates[] { new(0, -1), new(0, 1), new(1, 0), new(-1, 0) }
            .Select(pd => new Coordinates(current.X + pd.X, current.Y + pd.Y))
            .Where(c => c.X >= 0 && c.X < maxX && c.Y >= 0 && c.Y < maxY);
    }
}

public record PaintInstruction(char Direction, int Length);
