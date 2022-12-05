using AoE2022.Utils;

public class Day5 : StringBatchesDay
{
    protected override object FirstTask()
    {
        var startingPos = this.Input.First().Split("\r ");
        var commands = this.Input.Skip(1).First().Split("\r ");

        var queues = new Queue<char>[10];

        var index = 1;
        while (index < startingPos.MaxBy(c => c.Length).Length) {
            var queueIndex = (int)Char.GetNumericValue(startingPos.Last()[index]) - 1;
            queues[queueIndex] = new Queue<char>();
            for (int i = startingPos.Length - 2; i >= 0; i--) {
                var letter = startingPos[i][index];
                if (letter == ' ')
                    continue;

                queues[queueIndex].Enqueue(letter);
            }
            index += 4;
        }

        return null;
    }

    protected override object SecondTask()
    {
        return null;
    }
}
