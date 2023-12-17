using AoE2023.Utils;

namespace AoE2023;

public class Day17 : StringListDay
{
    private List<(int x, int y, char mask)> c = new(){
        (1, 0, 'R'),
        (-1, 0, 'L'),
        (0, 1, 'D'),
        (0, -1, 'U'),
    };

    protected override object FirstTask()
    {
        var grid = this.Input.Select(line => line.Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var gridHeight = grid.Length;
        var gridLength = grid[0].Length;
        var end = new Coordinates(grid[0].Length - 1, grid.Length - 1);
        var current = new List<Square>(){
            new(new(0, 0), "", 0, new(0, 0)),
        };
        var cache = new Dictionary<Square, int?>();
        while (current.Count > 0)
        {
            var nextSquares = current.SelectMany(c =>
            {
                return this.c.Select(x =>
                {
                    string direction;
                    var currentDirection = c.Direction.FirstOrDefault();
                    direction = currentDirection != x.mask ? $"{x.mask}" : c.Direction + x.mask;
                    var newX = x.x + c.C.X;
                    var newY = x.y + c.C.Y;

                    if (newX == c.Previous.X && newY == c.Previous.Y)
                        return null;

                    if (newX >= 0 && newX < gridLength && newY >= 0 && newY < gridHeight && direction.Length < 4)
                        return new Square(new(newX, newY), direction, c.Cost + grid[newY][newX], c.C);

                    return null;
                }).OfType<Square>();
            })
                .Where(sq =>
                {
                    if (cache.GetValueOrDefault(sq) is { } cachedValue && cachedValue <= sq.Cost)
                        return false;

                    cache[sq] = sq.Cost;
                    return true;
                })
                .ToList();
            current = nextSquares;
        }

        return cache.Where(sq => sq.Key.C == end).MinBy(kv => kv.Value).Value;
    }

    protected override object SecondTask()
    {        var grid = this.Input.Select(line => line.Select(c => (int)char.GetNumericValue(c)).ToArray()).ToArray();
        var gridHeight = grid.Length;
        var gridLength = grid[0].Length;
        var end = new Coordinates(grid[0].Length - 1, grid.Length - 1);
        var current = new List<Square>(){
            new(new(0, 0), "", 0, new(0, 0)),
        };
        var cache = new Dictionary<Square, int?>();
        while (current.Count > 0)
        {
            var nextSquares = current.SelectMany(c =>
            {
                return this.c.Select(x =>
                {
                    string direction;
                    var currentDirection = c.Direction.FirstOrDefault();
                    if (currentDirection != default && currentDirection != x.mask && c.Direction.Length < 4) {
                        return null;
                    }
                    direction = currentDirection != x.mask ? $"{x.mask}" : c.Direction + x.mask;
                    var newX = x.x + c.C.X;
                    var newY = x.y + c.C.Y;

                    if (newX == c.Previous.X && newY == c.Previous.Y)
                        return null;

                    if (newX >= 0 && newX < gridLength && newY >= 0 && newY < gridHeight && direction.Length < 11)
                        return new Square(new(newX, newY), direction, c.Cost + grid[newY][newX], c.C);

                    return null;
                }).OfType<Square>();
            })
                .Where(sq =>
                {
                    if (cache.GetValueOrDefault(sq) is { } cachedValue && cachedValue <= sq.Cost)
                        return false;

                    cache[sq] = sq.Cost;
                    return true;
                })
                .ToList();
            current = nextSquares;
        }

        return cache.Where(sq => sq.Key.Direction.Length >= 4 && sq.Key.C == end).MinBy(kv => kv.Value).Value;
    }
}

public class Square : IEquatable<Square>
{
    public Coordinates C { get; }
    public string Direction { get; }
    public int Cost { get; }

    public Coordinates Previous { get; }

    public Square(Coordinates c, string direction, int cost, Coordinates previous)
    {
        C = c;
        Direction = direction;
        Cost = cost;
        Previous = previous;
    }

    public bool Equals(Square other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(C, other.C) && Direction == other.Direction;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Square);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(C, Direction);
    }

    public static bool operator ==(Square left, Square right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Square left, Square right)
    {
        return !Equals(left, right);
    }
}

public class Masks
{
    public const int Up = 3;
    public const int Right = 30;
    public const int Down = 300;
    public const int Left = 3000;
}
