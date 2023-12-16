using AoE2023.Utils;
using FluentAssertions;

namespace AoE2023;

public class Day16 : StringListDay
{
    protected override object FirstTask()
    {
        var grid = this.Input.Select(line => line.ToCharArray()).ToArray();
        var gridHeight = grid.Length;
        var gridLength = grid[0].Length;
        var visitedSquares = new HashSet<Beam>();

        Beam start = new(0, 0, new(1, 0));
        var currentBeams = new List<Beam>() {
            start,
        };
        visitedSquares.Add(start);

        while (currentBeams.Count > 0)
        {
            var nextBeams = currentBeams.SelectMany(b => GetOutputBeams(b, grid[b.Y][b.X])).ToArray();

            currentBeams = nextBeams
                .Where(b => b.X >= 0 && b.X < gridLength && b.Y >= 0 && b.Y < gridHeight).ToArray()
                .Where(b => visitedSquares.Add(b))
                .ToList();
        }

        Test();

        return visitedSquares.Select(b => (b.X, b.Y)).Distinct().Count();
    }

    protected override object SecondTask()
    {
        var grid = this.Input.Select(line => line.ToCharArray()).ToArray();
        var gridHeight = grid.Length;
        var gridLength = grid[0].Length;
        var modifier = gridHeight > gridLength ? gridHeight : gridLength;

        var cache = new Dictionary<Beam, List<int>>();
        Beam start = new(0, 0, new(1, 0));

        return GetBeams(start).Distinct().Count();

        IEnumerable<int> GetBeams(Beam start)
        {
            if (cache.GetValueOrDefault(start) is {} list)
                return list;

            var visitedSquares = new HashSet<Beam>();
            var currentBeams = new List<Beam>()
            {
                start,
            };
            visitedSquares.Add(start);

            while (currentBeams.Count > 0)
            {
                var nextBeams = currentBeams.SelectMany(b => GetOutputBeams(b, grid[b.Y][b.X])).ToArray();

                currentBeams = nextBeams
                    .Where(b => b.X >= 0 && b.X < gridLength && b.Y >= 0 && b.Y < gridHeight).ToArray()
                    .Where(visitedSquares.Add)
                    .ToList();
            }

            var squares = visitedSquares.Select(b => b.Y * modifier + b.X).ToList();
            cache.Add(start, squares);
            return squares;
        }
    }

    private void Test()
    {
        var b1 = GetOutputBeams(new(0, 0, new(0, -1)), '-');
        var b2 = GetOutputBeams(new(0, 0, new(0, 1)), '-');
        var b3 = GetOutputBeams(new(0, 0, new(-1, 0)), '-');
        var b4 = GetOutputBeams(new(0, 0, new(1, 0)), '-');

        b1.Should().BeEquivalentTo([new Beam(-1, 0, new(-1, 0)), new Beam(1, 0, new(1, 0))]);
        b2.Should().BeEquivalentTo([new Beam(-1, 0, new(-1, 0)), new Beam(1, 0, new(1, 0))]);
        b3.Should().BeEquivalentTo([new Beam(-1, 0, new(-1, 0))]);
        b4.Should().BeEquivalentTo([new Beam(1, 0, new(1, 0))]);

        var b5 = GetOutputBeams(new(0, 0, new(0, 1)), '|');
        var b6 = GetOutputBeams(new(0, 0, new(0, -1)), '|');
        var b7 = GetOutputBeams(new(0, 0, new(-1, 0)), '|');
        var b8 = GetOutputBeams(new(0, 0, new(1, 0)), '|');

        b5.Should().BeEquivalentTo([new Beam(0, 1, new(0, 1))]);
        b6.Should().BeEquivalentTo([new Beam(0, -1, new(0, -1))]);
        b7.Should().BeEquivalentTo([new Beam(0, -1, new(0, -1)), new Beam(0, 1, new(0, 1))]);
        b8.Should().BeEquivalentTo([new Beam(0, -1, new(0, -1)), new Beam(0, 1, new(0, 1))]);

        var m1 = GetOutputBeams(new(0, 0, new(1, 0)), '\\');
        var m2 = GetOutputBeams(new(0, 0, new(-1, 0)), '\\');
        var m3 = GetOutputBeams(new(0, 0, new(0, -1)), '\\');
        var m4 = GetOutputBeams(new(0, 0, new(0, 1)), '\\');

        m1.Should().BeEquivalentTo([new Beam(0, 1, new(0, 1))]);
        m2.Should().BeEquivalentTo([new Beam(0, -1, new(0, -1))]);
        m3.Should().BeEquivalentTo([new Beam(-1, 0, new(-1, 0))]);
        m4.Should().BeEquivalentTo([new Beam(1, 0, new(1, 0))]);

        var m5 = GetOutputBeams(new(0, 0, new(1, 0)), '/');
        var m6 = GetOutputBeams(new(0, 0, new(-1, 0)), '/');
        var m7 = GetOutputBeams(new(0, 0, new(0, -1)), '/');
        var m8 = GetOutputBeams(new(0, 0, new(0, 1)), '/');

        m5.Should().BeEquivalentTo([new Beam(0, -1, new(0, -1))]);
        m6.Should().BeEquivalentTo([new Beam(0, 1, new(0, 1))]);
        m7.Should().BeEquivalentTo([new Beam(1, 0, new(1, 0))]);
        m8.Should().BeEquivalentTo([new Beam(-1, 0, new(-1, 0))]);
    }

    private Beam[] GetOutputBeams(Beam src, char instruction)
    {
        if (instruction == '-')
        {
            if (src.Direction.X != 0)
            {
                return [src with { X = src.X + src.Direction.X }];
            }
            if (src.Direction.Y != 0)
            {
                return [
                    src with { X = src.X + 1, Direction = new(1, 0) },
                    src with { X = src.X - 1, Direction = new(-1, 0) },
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
                    src with { Y = src.Y + 1, Direction = new(0, 1) },
                    src with { Y = src.Y - 1, Direction = new(0, -1) },
                ];
            }
        }

        if (instruction == '\\')
        {
            return [src with { X = src.X + src.Direction.Y, Y = src.Y + src.Direction.X, Direction = new(src.Direction.Y, src.Direction.X) }];
        }

        if (instruction == '/')
        {
            return [src with { X = src.X - src.Direction.Y, Y = src.Y - src.Direction.X, Direction = new(-src.Direction.Y, -src.Direction.X) }];
        }

        return [src with { X = src.X + src.Direction.X, Y = src.Y + src.Direction.Y }];
    }
}

public record Beam(int X, int Y, Direction Direction);

public record Direction(int X, int Y);
