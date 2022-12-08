using AoE2022.Utils;
using MoreLinq;

public class Day8 : StringListDay
{
    protected override object FirstTask()
    {
        var numbers = this.Input.Select(l => l.Select(n => (int)Char.GetNumericValue(n)).ToArray()).ToArray();
        var upToDown = UpToDown(numbers).ToList();
        var counter = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = 0; j < numbers[i].Length; j++)
            {
                var currentTree = numbers[i][j];
                if (numbers[i][0..j].All(v => v < currentTree)
                    || numbers[i][(j + 1)..].All(v => v < currentTree)
                    || upToDown[j][0..i].All(v => v < currentTree)
                    || upToDown[j][(i + 1)..].All(v => v < currentTree))
                    {
                        counter++;
                    }
            }
        }

        return counter;
    }

    protected override object SecondTask()
    {
        var numbers = this.Input.Select(l => l.Select(n => (int)Char.GetNumericValue(n)).ToArray()).ToArray();
        var upToDown = UpToDown(numbers).ToList();
        var max = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = 0; j < numbers[i].Length; j++)
            {
                var currentTree = numbers[i][j];
                var left = numbers[i][0..j].Reverse().TakeUntil(v => v >= currentTree).Count();
                var right = numbers[i][(j + 1)..].TakeUntil(v => v >= currentTree).Count();
                var up = upToDown[j][0..i].Reverse().TakeUntil(v => v >= currentTree).Count();
                var down = upToDown[j][(i + 1)..].TakeUntil(v => v >= currentTree).Count();

                var score = left * right * up * down;
                if (score > max)
                    max = score;
            }
        }

        return max;
    }

    private IEnumerable<int[]> UpToDown(int[][] source) {
        for (int i = 0; i < source[0].Length; i++)
        {
            var r = new List<int>();
            for (int j = 0; j < source.Length; j++)
            {
                r.Add(source[j][i]);
            }

            yield return r.ToArray();
        }
    }
}
