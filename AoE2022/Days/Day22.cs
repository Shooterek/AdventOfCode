using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day22 : StringBatchesDay
{
    private const char Empty = '.';
    private const char Wall = '#';
    private const char None = ' ';

    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        var map = this.Input[0]
            .Split("\r ")
            .Select(l => l.Select(c => c).ToList()).ToList();

        var maxWidth = map[0].Count - 1;
        var maxHeight = map.Count - 1;
        int direction = 0;
        int lastMoveDir = 0;
        var pos = (x: map[0].IndexOf(Empty), y: 0);

        var instructions = Regex.Split(this.Input[1], @"(?<=[RL])");

        var ind = 0;
        foreach (var instruction in instructions)
        {
            ind++;
            var steps = int.Parse(NumberPattern.Match(instruction).Captures.First().Value);
            var dir = GetDir(direction);
            for (int i = 0; i < steps; i++)
            {
                (var oldX, var oldY) = pos;
                var newX = pos.x + dir.Item1;
                var newY = pos.y + dir.Item2;

                newX = (newX + maxWidth + 1) % (maxWidth + 1);
                newY = (newY + maxHeight + 1) % (maxHeight + 1);

                while (map[newY][newX] == None)
                {
                    newX = newX + dir.Item1;
                    newY = newY + dir.Item2;

                    newX = (newX + maxWidth + 1) % (maxWidth + 1);
                    newY = (newY + maxHeight + 1) % (maxHeight + 1);
                }
                if (map[newY][newX] == Wall)
                {
                    pos = (oldX, oldY);
                    break;
                }
                else
                {
                    pos = (newX, newY);
                    lastMoveDir = direction;
                }
            }

            if (!char.IsDigit(instruction.Last())) {
                direction += instruction.Last() == 'R' ? 1 : -1;
                direction = (direction + 4) % 4;
            }
        }

        return (pos.y + 1) * 1000 + (pos.x + 1) * 4 + direction;
    }

    private (int, int) GetDir(int direction)
    {
        return direction switch
        {
            0 => (1, 0),
            1 => (0, 1),
            2 => (-1, 0),
            3 => (0, -1),
        };
    }

    protected override object SecondTask()
    {
        return 0;
    }
}
