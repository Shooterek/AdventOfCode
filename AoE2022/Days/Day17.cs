using AoE2022.Utils;

public class Day17 : EntireStringDay
{
    protected override object FirstTask()
    {
        return GetHeight(2022);
    }

    protected override object SecondTask()
    {
        var maxWidth = 7;
        var points = new HashSet<Point>();
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => points.Add(new(v, 0)));
        var highest = new Dictionary<int, int>();
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => highest[v] = 0);
        var rocksFallen = 0;
        var jetIndex = 0;
        var x = new HashSet<(int, int)>();
        var ct = new List<(int?, (int, int))>();
        while (rocksFallen < 10000)
        {
            var maxHeight = highest.Values.OrderDescending().First();
            jetIndex %= this.Input.Length;
            var c = (jetIndex, rocksFallen % 5);
            if (!x.Add(c))
            {
                ct.Add((rocksFallen, c));
            }

            for (int i = 0; i < ct.Count - 1; i++)
            {
                var curr = ct[i];
                var next = ct[i + 1];
                if (next.Item1 - curr.Item1 != 1)
                {
                    ct[i] = ct[i] with { Item1 = null };
                }
            }

            ct = ct.Where(c => c.Item1 != null).ToList();
            if (ct.Count == 50)
            {
                var firstIndex = x.ToList().IndexOf(ct.First().Item2);
                var cycleLength = ct.First().Item1 - firstIndex;
                var rocksFallenWithinCycles = 1000000000000 - firstIndex;
                var cycleCount = rocksFallenWithinCycles / cycleLength;
                var heightEarnedWithinOneCycle = GetHeight(ct.First().Item1.Value) - GetHeight(firstIndex);

                return heightEarnedWithinOneCycle * cycleCount + GetHeight(firstIndex + (int)(rocksFallenWithinCycles % cycleCount));
            }
            var shape = GetShape(rocksFallen++, maxHeight);
            while (true)
            {
                var jetInstr = this.Input[jetIndex++ % this.Input.Length] == '<' ? -1 : 1;
                var movedShape = shape.Select(s => s.ChangePos(jetInstr, 0)).ToList();
                shape = movedShape.Any(p => p.X < 0 || p.X > 6 || points.Contains(p)) ? shape : movedShape;
                movedShape = shape.Select(s => s.ChangePos(0, -1)).ToList();
                if (movedShape.Any(p => points.Contains(p)))
                {
                    shape.ForEach(p => points.Add(p));
                    shape.GroupBy(p => p.X, p => p.Y)
                        .Select(kv => (kv.Key, kv.Max()))
                        .ToList()
                        .ForEach(kv => highest[kv.Key] = Math.Max(highest[kv.Key], kv.Item2));
                    break;
                }
                shape = movedShape;
            }
        }

        return highest.Values.OrderDescending().First();
    }

    private record Point(int X, int Y)
    {
        public Point ChangePos(int xDif, int yDif)
            => this with { X = this.X + xDif, Y = this.Y + yDif };
    }

    private int GetHeight(int fallenRocks)
    {
        var maxWidth = 7;
        var points = new HashSet<Point>();
        var highest = new Dictionary<int, int>();
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => points.Add(new(v, 0)));
        Enumerable.Range(0, maxWidth).ToList().ForEach(v => highest[v] = 0);
        var rocksFallen = 0;
        var jetIndex = 0;
        while (rocksFallen < fallenRocks)
        {
            var maxHeight = highest.Values.OrderDescending().First();
            jetIndex %= this.Input.Length;
            var shape = GetShape(rocksFallen++, maxHeight);
            while (true)
            {
                var jetInstr = this.Input[jetIndex++ % this.Input.Length] == '<' ? -1 : 1;
                var movedShape = shape.Select(s => s.ChangePos(jetInstr, 0)).ToList();
                shape = movedShape.Any(p => p.X < 0 || p.X > 6 || points.Contains(p)) ? shape : movedShape;
                movedShape = shape.Select(s => s.ChangePos(0, -1)).ToList();
                if (movedShape.Any(p => points.Contains(p)))
                {
                    shape.ForEach(p => points.Add(p));
                    shape.GroupBy(p => p.X, p => p.Y)
                        .Select(kv => (kv.Key, kv.Max()))
                        .ToList()
                        .ForEach(kv => highest[kv.Key] = Math.Max(highest[kv.Key], kv.Item2));
                    break;
                }
                shape = movedShape;
            }
        }

        return highest.Values.OrderDescending().First();
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
