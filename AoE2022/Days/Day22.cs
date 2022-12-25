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
                pos = (newX, newY);
            }

            if (!char.IsDigit(instruction.Last()))
            {
                direction += instruction.Last() == 'R' ? 1 : -1;
                direction = (direction + 4) % 4;
            }
        }

        return (pos.y + 1) * 1000 + (pos.x + 1) * 4 + direction;
    }

    protected override object SecondTask()
    {
        var map = this.Input[0]
            .Split("\r ")
            .Select(l => l.Select(c => c).ToList()).ToList();

        var maxWidth = map[0].Count - 1;
        var maxHeight = map.Count - 1;
        int direction = 0;
        var pos = (x: map[0].IndexOf(Empty), y: 0);

        var instructions = Regex.Split(this.Input[1], @"(?<=[RL])");

        foreach (var instruction in instructions)
        {
            if (instruction == "36R")
            {
                Console.WriteLine();
            }
            var steps = int.Parse(NumberPattern.Match(instruction).Captures.First().Value);
            for (int i = 0; i < steps; i++)
            {
                var dir = GetDir(direction);
                (var oldX, var oldY) = pos;
                var newX = pos.x + dir.Item1;
                var newY = pos.y + dir.Item2;

                if (newY < 0)
                {
                    if (newX >= 50 && newX < 100)
                    {
                        newY = 100 + newX;
                        newX = 0;
                        direction = 0;
                    }
                    if (newX >= 100 && newX < 150)
                    {
                        newY = 199;
                        newX = newX - 100;
                    }
                }
                else if (newY >= 0 && newY < 50)
                {
                    if (newX > maxWidth)
                    {
                        newY = maxHeight - 50 - newY;
                        newX = 99;
                        direction = 2;
                    }
                    else if (newX < 50)
                    {
                        newY = maxHeight - 50 - newY;
                        newX = 0;
                        direction = 0;
                    }
                }
                else if (newY >= 50 && newY < 100)
                {
                    if (newX > 99 && direction == 0)
                    {
                        newX = 50 + newY;
                        newY = 49;
                        direction = 3;
                    }
                    else if (newX > 99 && direction == 1)
                    {
                        newY = newX - 50;
                        newX = 99;
                        direction = 2;
                    }
                    else if (newX < 50 && direction == 2)
                    {
                        newX = newY - 50;
                        newY = 100;
                        direction = 1;
                    }
                    else if (newX < 50 && direction == 3)
                    {
                        newY = 50 + newX;
                        newX = 50;
                        direction = 0;
                    }
                }
                else if (newY >= 100 && newY < 150)
                {
                    if (newX >= 100)
                    {
                        newY = 0 + 149 - newY;
                        newX = maxWidth;
                        direction = 0;
                    }
                    else if (newX < 0)
                    {
                        newY = 0 + 149 - newY;
                        newX = 50;
                        direction = 2;
                    }
                }
                else if (newY >= 150 && newY < 200)
                {
                    if (newX < 0)
                    {
                        newX = newY - 100;
                        newY = 0;
                        direction = 1;
                    }
                    else if (newX >= 50 && newX < 100 && direction == 0)
                    {
                        newX = newY - 100;
                        newY = 149;
                        direction = 3;
                    }
                    else if (newX >= 50 && newX < 100 && direction == 1)
                    {
                        newY = 100 + newX;
                        newX = 49;
                        direction = 2;
                    }
                }
                else if (newY >= 200)
                {
                    newY = 0;
                    newX = 100 + newX;
                }

                if (map[newY][newX] == Wall) {
                    Console.WriteLine($"Wall at X:{newX}, Y: {newY}");
                    break;
                }
                pos = (newX, newY);
                Console.WriteLine($"{pos}: {direction} | {instruction}");

                if (map[newY][newX] == None)
                    throw new Exception();
            }

            if (!char.IsDigit(instruction.Last()))
            {
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
}
