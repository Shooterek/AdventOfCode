using AoE2022.Utils;
public class Day17 : EntireStringDay
{
    protected override object FirstTask()
    {
        var maxWidth = 7;
        var points = new HashSet<Point>();
        var heighestPoint = new Dictionary<int, int>();
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => heighestPoint[v] = 0);
        var rocksFallen = 0;
        var jetIndex = 0;
        while (rocksFallen < 2023)
        {
            var maxHeight = heighestPoint.Values.OrderDescending().FirstOrDefault(-1);
            var shape = GetShape(rocksFallen++, maxHeight);
            while (true)
            {
                var jetInstr = this.Input[jetIndex++ % this.Input.Length] == '<' ? -1 : 1;
                var movedShape = shape.Select(s => s.ChangePos(jetInstr, 0)).ToList();
                shape = movedShape.Any(p => p.X < 0 || p.X > 6 || points.Contains(p)) ? shape : movedShape;
                movedShape = shape.Select(s => s.ChangePos(0, -1)).ToList();
                if (movedShape.Any(p => p.Y < 0 || points.Contains(p))) {
                    shape.ForEach(p => points.Add(p));
                    shape.GroupBy(p => p.X, p => p.Y)
                        .Select(kv => (kv.Key, kv.Max()))
                        .ToList()
                        .ForEach(kv => heighestPoint[kv.Key] = Math.Max(heighestPoint[kv.Key], kv.Item2));
                    break;
                }
                shape = movedShape;
            }
        }

        return points.MaxBy(p => p.Y).Y - 1;
    }

    protected override object SecondTask()
    {
        var maxWidth = 7;
        var points = new HashSet<Point>();
        var heighestPoint = new Dictionary<int, int>();
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => heighestPoint[v] = 0);
        var rocksFallen = 0;
        var jetIndex = 0;
        var c = new HashSet<(int, int)>();
        while (rocksFallen < 2023)
        {
            var maxHeight = heighestPoint.Values.OrderDescending().FirstOrDefault(-1);
            jetIndex %= this.Input.Length;
            if (rocksFallen % 25 == 0) {
                Console.WriteLine(maxHeight);
            }
            // if (!c.Add((jetIndex, rocksFallen % 5))) {
            //     Console.WriteLine(rocksFallen);
            // }
            var shape = GetShape(rocksFallen++, maxHeight);
            while (true)
            {
                var jetInstr = this.Input[jetIndex++ % this.Input.Length] == '<' ? -1 : 1;
                var movedShape = shape.Select(s => s.ChangePos(jetInstr, 0)).ToList();
                shape = movedShape.Any(p => p.X < 0 || p.X > 6 || points.Contains(p)) ? shape : movedShape;
                movedShape = shape.Select(s => s.ChangePos(0, -1)).ToList();
                if (movedShape.Any(p => p.Y < 0 || points.Contains(p))) {
                    shape.ForEach(p => points.Add(p));
                    shape.GroupBy(p => p.X, p => p.Y)
                        .Select(kv => (kv.Key, kv.Max()))
                        .ToList()
                        .ForEach(kv => heighestPoint[kv.Key] = Math.Max(heighestPoint[kv.Key], kv.Item2));
                    break;
                }
                shape = movedShape;
            }
        }

        return points.MaxBy(p => p.Y).Y - 1;
    }

    private record Point(int X, int Y) {
        public Point ChangePos(int xDif, int yDif)
            => this with { X = this.X + xDif, Y = this.Y + yDif };
    }

    private List<Point> GetShape(int number, int currentMaxHeight)
    {
        var lowestPoint = currentMaxHeight + 4;
        return (number % 5) switch
        {
            0 => Points(Enumerable.Range(2, 4).Select(i => (i, lowestPoint)).ToArray()),
            1 => Points(
                (3, lowestPoint + 2),
                (2, lowestPoint + 1), (3, lowestPoint + 1), (4, lowestPoint + 1),
                (3, lowestPoint)),
            2 => Points(
                (4, lowestPoint + 2),
                (4, lowestPoint + 1),
                (2, lowestPoint), (3, lowestPoint), (4, lowestPoint)),
            3 => Points(
                (2, lowestPoint + 3),
                (2, lowestPoint + 2),
                (2, lowestPoint + 1),
                (2, lowestPoint)),
            4 => Points(
                (2, lowestPoint + 1), (3, lowestPoint + 1),
                (2, lowestPoint), (3, lowestPoint)),
            _ => throw new Exception(),
        };

        List<Point> Points(params (int, int)[] points)
            => points.Select(p => new Point(p.Item1, p.Item2)).ToList();
    }
}
