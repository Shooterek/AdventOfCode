using AoE2022.Utils;

public class Day9 : StringListDay
{
    protected override object FirstTask()
    {
        var head = new Point(0, 0);
        var tail = new Point(0, 0);
        var visitedPoints = new List<Point>();

        foreach (var instr in this.Input)
        {
            (var diffX, var difY) = instr[0] switch
            {
                'U' => (0, 1),
                'D' => (0, -1),
                'L' => (-1, 0),
                'R' => (1, 0),
                _ => throw new Exception(),
            };
            var length = int.Parse(instr[1..]);

            while (length > 0) {
                head = head with { X = head.X + diffX, Y = head.Y + difY };
                (var x, var y) = ((head.X - tail.X), (head.Y - tail.Y)) switch {
                    (2, _) => (head.X - 1, head.Y),
                    (-2, _) => (head.X + 1, head.Y),
                    (_, 2) => (head.X, head.Y - 1),
                    (_, -2) => (head.X, head.Y + 1),
                    _ => (tail.X, tail.Y),
                };
                tail = new Point(x, y);
                visitedPoints.Add(tail);
                length--;
            }
        }

        return visitedPoints.Distinct().Count();
    }

    protected override object SecondTask()
    {
        var rope = Enumerable.Range(0, 10).Select(_ => new Point(0, 0)).ToList();
        var visitedPoints = new List<Point>();

        foreach (var instr in this.Input) {
            (var diffX, var difY) = instr[0] switch
            {
                'U' => (0, 1),
                'D' => (0, -1),
                'L' => (-1, 0),
                'R' => (1, 0),
                _ => throw new Exception(),
            };
            var length = int.Parse(instr[1..]);

            while (length > 0) {
                rope[0] = rope[0] with { X = rope[0].X + diffX, Y = rope[0].Y + difY };
                for (int i = 1; i < rope.Count; i++)
                {
                    var head = rope[i - 1];
                    var tail = rope[i];
                    (var x, var y) = ((head.X - tail.X), (head.Y - tail.Y)) switch
                    {
                        (2, 2) => (head.X - 1, head.Y - 1),
                        (-2, -2) => (head.X + 1, head.Y + 1),
                        (2, -2) => (head.X - 1, head.Y + 1),
                        (-2, 2) => (head.X + 1, head.Y - 1),
                        (2, _) => (head.X - 1, head.Y),
                        (-2, _) => (head.X + 1, head.Y),
                        (_, 2) => (head.X, head.Y - 1),
                        (_, -2) => (head.X, head.Y + 1),
                        _ => (tail.X, tail.Y),
                    };
                    tail = new(x, y);
                    rope[i] = tail;
                }
                visitedPoints.Add(rope.Last());
                length--;
            }
        }

        return visitedPoints.Distinct().Count();
    }

    private record Point(int X, int Y);
}
