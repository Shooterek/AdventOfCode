using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day16 : StringListDay
{
    protected override object FirstTask()
    {
        var grid = this.Input.Select(line => line.ToCharArray()).ToArray();
        var gridHeight = grid.Length;
        var gridLength = grid[0].Length;
        var visitedSquares = new HashSet<Beam>();

        var currentBeams = new List<Beam>() {
            new (1, 0, new(-1, 0)),
        };

        while (currentBeams.Any())
        {

            var nextBeams = currentBeams.SelectMany(b => GetOutputBeams(b, grid)).ToArray();

            currentBeams = nextBeams
                .Where(b => b.X >= 0 && b.X < gridLength && b.Y >= 0 && b.Y < gridHeight).ToArray()
                .Where(b => visitedSquares.Add(b))
                .ToList();
        }

        Print2DMap(gridLength, gridHeight, visitedSquares.ToList());

        return visitedSquares.Count();
    }

    protected override object SecondTask()
    {
        return null;
    }

    private Beam[] GetOutputBeams(Beam src, char[][] grid)
    {
        var instruction = grid[src.Y][src.X];

        if (instruction == '-')
        {
            if (src.Direction.X != 0)
            {
                return [src with { X = src.X + src.Direction.X }];
            }
            if (src.Direction.Y != 0)
            {
                return [
                    src with { X = src.X + src.Direction.Y, Y = src.Direction.Y, Direction = new(src.Direction.Y, 0) },
                    src with { X = src.X - src.Direction.Y, Y = src.Direction.Y, Direction = new(-src.Direction.Y, 0) },
                ];
            }
        }

        if (instruction == '|')
        {
            if (src.Direction.Y != 0)
            {
                return [src with { Y = src.Y + src.Direction.Y }];
            }
            if (src.Direction.X != 0)
            {
                return [
                    src with { Y = src.Y + src.Direction.X, Direction = new(0, src.Direction.X) },
                    src with { Y = src.Y - src.Direction.X, Direction = new(0, -src.Direction.X) },
                ];
            }
        }

        if (instruction == '\\')
        {
            return [src with { X = src.X - src.Direction.Y, Y = src.Y + src.Direction.X, Direction = new(-src.Direction.Y, -src.Direction.X) }];
        }

        if (instruction == '/')
        {
            return [src with { X = src.X + src.Direction.Y, Y = src.Y - src.Direction.X, Direction = new(src.Direction.Y, src.Direction.X) }];
        }

        return [src with { X = src.X + src.Direction.X, Y = src.Y + src.Direction.Y }];
    }

    static void Print2DMap(int maxX, int maxY, List<Beam> coordinates)
    {
        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                if (coordinates.Any(b => b.X == x && b.Y == y))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }
}

public record Beam(int X, int Y, Direction Direction);

public record Direction(int X, int Y);
