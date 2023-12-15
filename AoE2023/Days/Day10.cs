using AoE2023.Utils;
using System.Collections.Immutable;

namespace AoE2023;

public class Day10 : StringListDay
{
    private readonly Dictionary<char, List<(int x, int y)>> symbolMap = new()
    {
        ['L'] = new() { (0, -1), (1, 0) },
        ['J'] = new() { (0, -1), (-1, 0) },
        ['F'] = new() { (0, 1), (1, 0) },
        ['7'] = new() { (0, 1), (-1, 0) },
        ['-'] = new() { (1, 0), (-1, 0) },
        ['|'] = new() { (0, -1), (0, 1) },
        ['S'] = new() { (0, -1), (0, 1), (1, 0), (-1, 0) },
        ['.'] = new(),
    };

    private char[][] map;

    protected override object FirstTask()
    {
        this.map = this.Input.Select(line => line.ToCharArray()).ToArray();

        var start = (x: 0, y: 0);
        var maxY = map.Length;
        var maxX = map[0].Length;
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (map[y][x] == 'S')
                    start = (x, y);
            }
        }

        var visitedSquares = new HashSet<(int x, int y)>();
        var square = start;
        while (true)
        {
            visitedSquares.Add(square);
            square = GetNeighbours(square, maxX, maxY, map[square.y][square.x]).FirstOrDefault(x => !visitedSquares.Contains(x), (-1, -1));
            if (square.x == -1)
            {
                break;
            }
        }

        return Math.Ceiling((float)visitedSquares.Count / 2);
    }

    protected override object SecondTask()
    {
        this.map = this.Input.Select(line => line.ToCharArray()).ToArray();

        var start = (x: 0, y: 0);
        var maxY = map.Length;
        var maxX = map[0].Length;
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                if (map[y][x] == 'S')
                    start = (x, y);
            }
        }

        var step = 0;
        var visitedSquares = new HashSet<(int x, int y)>();
        var square = start;
        while (true)
        {
            visitedSquares.Add(square);
            square = GetNeighbours(square, maxX, maxY, map[square.y][square.x]).FirstOrDefault(x => !visitedSquares.Contains(x), (-1, -1));
            if (square.x == -1)
            {
                break;
            }
            step++;
        }

        maxX *= 2;
        maxY *= 2;
        var borderSquares = visitedSquares.Select(sq => sq with { x = sq.x * 2, y = sq.y * 2 }).ToList();
        var midpoints = new List<(int, int)>();
        for (int i = 0; i < visitedSquares.Count - 1; i++)
        {
            var current = borderSquares[i];
            var next = borderSquares[i + 1];

            midpoints.Add(GetMidpoint(current, next));
        }

        var c = borderSquares[borderSquares.Count - 1];
        var n = borderSquares[0];

        midpoints.Add(GetMidpoint(c, n));
        borderSquares.AddRange(midpoints);

        var wallSquares = new HashSet<(int x, int y)>();
        var borderSquaresSet = borderSquares.ToHashSet();
        foreach (var x in Enumerable.Range(0, maxX))
        {
            wallSquares.Add((x, 0));
            wallSquares.Add((x, maxY - 1));
        }
        foreach (var y in Enumerable.Range(0, maxY))
        {
            wallSquares.Add((0, y));
            wallSquares.Add((maxX - 1, y));
        }

        wallSquares = wallSquares.Except(borderSquares).ToHashSet();

        var nextSquares = wallSquares.ToHashSet();
        while (nextSquares.Any())
        {
            var next = nextSquares.SelectMany(sq => GetNeighbours(sq, maxX, maxY, 'S', false)).Distinct().ToArray();
            nextSquares.Clear();
            foreach (var sq in next)
            {
                if (!borderSquaresSet.Contains(sq) && wallSquares.Add(sq))
                {
                    nextSquares.Add(sq);
                }
            }
        }

        var allSquares = GenerateCoordinates(maxY, maxX).Except(borderSquares).Except(wallSquares).Where(sq => sq.x % 2 == 0 && sq.y % 2 == 0).ToList();

        return allSquares.Count;
    }

    private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) current, int maxX, int maxY, char symbol, bool validate = true)
    {
        var possibleDirections = this.symbolMap[symbol];

        return possibleDirections
            .Select(pd => (x: current.x + pd.x, y: current.y + pd.y))
            .Where(c => c.x >= 0 && c.x < maxX && c.y >= 0 && c.y < maxY)
            .Where(c => !validate || GetNeighbours(c, maxX, maxY, this.map[c.y][c.x], false).Contains(current));
    }

    static List<(int x, int y)> GenerateCoordinates(int rows, int columns)
    {
        List<(int x, int y)> coordinates = new List<(int x, int y)>();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                coordinates.Add((x, y));
            }
        }

        return coordinates;
    }

    static (int x, int y) GetMidpoint((int X, int Y) coord1, (int X, int Y) coord2)
    {
        int midX = (coord1.X + coord2.X) / 2;
        int midY = (coord1.Y + coord2.Y) / 2;

        return new(midX, midY);
    }
}
