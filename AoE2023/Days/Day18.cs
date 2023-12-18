using AoE2023.Utils;
using Microsoft.Diagnostics.Runtime.Utilities;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day18 : StringListDay
{
    protected override object FirstTask()
    {
        var instructions = this.Input.Select(line =>
        {
            var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            return new PaintInstruction(parts[0].First(), int.Parse(parts[1]), parts[2]);
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

        points = points.Select(p => p with { X = p.X - xOffset, Y = p.Y -yOffset }).ToHashSet();

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
        return null;
    }


    static List<Coordinates> GenerateCoordinates(int rows, int columns)
    {
        var coordinates = new List<Coordinates>();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                coordinates.Add(new(x, y));
            }
        }

        return coordinates;
    }

    private IEnumerable<Coordinates> GetNeighbours(Coordinates current, int maxX, int maxY)
    {
        return new Coordinates[] { new(0, -1), new(0, 1), new(1, 0), new(-1, 0) }
            .Select(pd => new Coordinates(current.X + pd.X, current.Y + pd.Y))
            .Where(c => c.X >= 0 && c.X < maxX && c.Y >= 0 && c.Y < maxY);
    }
}

public record PaintInstruction(char Direction, int Length, string Color);