using AoE2023.Utils;
using System.Text.RegularExpressions;

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

        var step = 0;
        var currentSquares = GetNeighbours(start, maxX, maxY, 'S').ToList();
        var visitedSquares = new HashSet<(int x, int y)>();
        visitedSquares.Add(start);
        while (currentSquares.Count > 0)
        {
            var nextIter = new List<(int x, int y)>();
            foreach (var square in currentSquares)
            {
                if (!visitedSquares.Add(square))
                {
                    Console.WriteLine(step);
                    continue;
                }
                if (square.x == start.x && square.y == start.y)
                {
                    currentSquares.Clear();
                    break;
                }
                nextIter.AddRange(GetNeighbours(square, maxX, maxY, map[square.y][square.x]));
            }
            step++;

            currentSquares.Clear();
            currentSquares.AddRange(nextIter.Where(sq => !visitedSquares.Contains(sq)));
            nextIter.Clear();
        }

        return step;
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
        var currentSquares = GetNeighbours(start, maxX, maxY, 'S').ToList();
        var visitedSquares = new HashSet<(int x, int y)>();
        visitedSquares.Add(start);
        while (currentSquares.Count > 0)
        {
            var nextIter = new List<(int x, int y)>();
            foreach (var square in currentSquares)
            {
                if (!visitedSquares.Add(square))
                {
                    Console.WriteLine(step);
                    continue;
                }
                if (square.x == start.x && square.y == start.y)
                {
                    currentSquares.Clear();
                    break;
                }
                nextIter.AddRange(GetNeighbours(square, maxX, maxY, map[square.y][square.x]));
            }
            step++;

            currentSquares.Clear();
            currentSquares.AddRange(nextIter.Where(sq => !visitedSquares.Contains(sq)));
            nextIter.Clear();
        }

        var wallSquares = new HashSet<(int x, int y)>();
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

        wallSquares = wallSquares.Except(visitedSquares).ToHashSet();

        var nextSquares = wallSquares.ToHashSet();
        while (nextSquares.Any()) {
            var next = nextSquares.SelectMany(sq => GetNeighbours(sq, maxX, maxY, 'S', false)).Distinct().ToArray();
            nextSquares.Clear();
            foreach (var sq in next) {
                if (!visitedSquares.Contains(sq) && wallSquares.Add(sq)) {
                    nextSquares.Add(sq);
                }
            }
        }

        var allSquares = GenerateCoordinates(maxY, maxX).Except(wallSquares.Concat(visitedSquares));

        var result = allSquares.Where(sq => {
            var wallCountLeft = Enumerable.Range(0, sq.x).Select(x => (x, sq.y)).Intersect(visitedSquares).Count();
            var wallCountRight = Enumerable.Range(sq.x, maxX - sq.x).Select(x => (x, sq.y)).Intersect(visitedSquares).Count();
            var wallCountTop = Enumerable.Range(0, sq.y).Select(y => (sq.x, y)).Intersect(visitedSquares).Count();
            var wallCountDown = Enumerable.Range(sq.y, maxY - sq.y).Select(y => (sq.x, sq.y)).Intersect(visitedSquares).Count();
            return (wallCountLeft % 2 == 1 || wallCountRight % 2 == 1) || (wallCountTop % 2 == 1 || wallCountDown % 2 == 1);
        });

        return result.Count();
    }

    private IEnumerable<(int x, int y)> GetNeighbours((int x, int y) current, int maxX, int maxY, char symbol, bool validate = true)
    {
        var directions = new List<(int x, int y)>() {
            (1, 0),
            (-1, 0),
            (0, current.y + 1),
            (0, current.y - 1),
        };

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
}
