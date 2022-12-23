using AoE2022.Utils;

public class Day23 : StringListDay
{
    private Point NW = new(-1, -1);
    private Point N = new(0, -1);
    private Point NE = new(1, -1);
    private Point W = new(-1, 0);
    private Point E = new(1, 0);
    private Point SW = new(-1, 1);
    private Point S = new(0, 1);
    private Point SE = new(1, 1);
    private readonly List<Point> All;

    public Day23()
    {
        this.All = new List<Point> { NW, N, NE, W, E, SW, S, SE };
    }

    protected override object FirstTask()
    {
        var map = ParseInput();

        for (int i = 0; i < 10; i++)
        {
            var plannedMoves = new Dictionary<Point, List<Point>>();
            foreach (var elf in map)
            {
                if (this.All.All(m => !map.Contains(m.Add(elf))))
                    continue;
                
                var plannedMove = TryFindNextPosition(map, elf, i);
                if (plannedMove == null)
                    continue;

                if (!plannedMoves.TryAdd(plannedMove, new List<Point> { elf }))
                    plannedMoves[plannedMove].Add(elf);
            }

            foreach (var plannedMove in plannedMoves.Where(gr => gr.Value.Count == 1))
            {
                map.Remove(plannedMove.Value.First());
                map.Add(plannedMove.Key);
            }
        }

        var maxY = map.MaxBy(p => p.Y).Y;
        var minY = map.MinBy(p => p.Y).Y;
        var maxX = map.MaxBy(p => p.X).X;
        var minX = map.MinBy(p => p.X).X;

        return (maxX - minX + 1) * (maxY - minY + 1) - map.Count();
    }

    protected override object SecondTask()
    {
        var map = ParseInput();
        
        var i = 0;
        while (true)
        {
            var plannedMoves = new Dictionary<Point, List<Point>>();
            foreach (var elf in map)
            {
                if (this.All.All(m => !map.Contains(m.Add(elf))))
                    continue;
                
                var plannedMove = TryFindNextPosition(map, elf, i);
                if (plannedMove == null)
                    continue;

                if (!plannedMoves.TryAdd(plannedMove, new List<Point> { elf }))
                    plannedMoves[plannedMove].Add(elf);
            }

            i++;
            if (plannedMoves.All(m => m.Value.Count > 1))
                return i;
            foreach (var plannedMove in plannedMoves.Where(gr => gr.Value.Count == 1))
            {
                map.Remove(plannedMove.Value.First());
                map.Add(plannedMove.Key);
            }
        }
    }

    private HashSet<Point> ParseInput()
    {
        var map = this.Input.SelectMany((line, yIndex) => line.Select((c, xIndex) => c == '#' ? new Point(xIndex, yIndex) : null)).OfType<Point>();
        return new(map);
    }

    private record Point(int X, int Y)
    {
        public Point Add(Point other) => this with { X = this.X + other.X, Y = this.Y + other.Y };
    }

    private Point? TryFindNextPosition(HashSet<Point> map, Point current, int i)
    {
        Point? next = null;
        var counter = 0;
        while (next == null && counter < 4)
        {
            var moves = GetMoves(i + counter);
            if (moves.All(c => !map.Contains(current.Add(c))))
                next = current.Add(moves[1]);

            counter++;
        }

        return next;
    }

    private List<Point> GetMoves(int dir)
    {
        return (dir % 4) switch
        {
            0 => new List<Point> { NW, N, NE },
            1 => new List<Point> { SW, S, SE },
            2 => new List<Point> { NW, W, SW },
            3 => new List<Point> { NE, E, SE },
        };
    }
}
