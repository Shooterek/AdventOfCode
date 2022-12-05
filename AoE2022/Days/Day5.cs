using AoE2022.Utils;

public class Day5 : StringBatchesDay
{
    protected override object FirstTask()
    {
        var startingPos = this.Input.First().Split("\r ");
        var commands = this.Input.Skip(1).First().Split("\r ");

        var stacks = new Stack<char>[10];

        var index = 1;
        while (index < startingPos.MaxBy(c => c.Length).Length) {
            var stackIndex = (int)Char.GetNumericValue(startingPos.Last()[index]) - 1;
            stacks[stackIndex] = new Stack<char>();
            for (int i = startingPos.Length - 2; i >= 0; i--) {
                var letter = startingPos[i][index];
                if (letter == ' ')
                    continue;

                stacks[stackIndex].Push(letter);
            }
            index += 4;
        }

        foreach (var command in commands) {
            var parts = command.Split(' ');
            var numbers = new string[] { parts[1], parts[3], parts[5] }.Select(int.Parse).ToArray();
            var count = numbers[0];
            var from = numbers[1] - 1;
            var to = numbers[2] - 1;

            for (int i = 0; i < count; i++)
            {
                stacks[to].Push(stacks[from].Pop());
            }
        }

        return string.Join("", stacks.OfType<Stack<char>>().Select(q => q.Pop()));
    }

    protected override object SecondTask()
    {        var startingPos = this.Input.First().Split("\r ");
        var commands = this.Input.Skip(1).First().Split("\r ");

        var stacks = new Stack<char>[10];

        var index = 1;
        while (index < startingPos.MaxBy(c => c.Length).Length) {
            var stackIndex = (int)Char.GetNumericValue(startingPos.Last()[index]) - 1;
            stacks[stackIndex] = new Stack<char>();
            for (int i = startingPos.Length - 2; i >= 0; i--) {
                var letter = startingPos[i][index];
                if (letter == ' ')
                    continue;

                stacks[stackIndex].Push(letter);
            }
            index += 4;
        }

        foreach (var command in commands) {
            var parts = command.Split(' ');
            var numbers = new string[] { parts[1], parts[3], parts[5] }.Select(int.Parse).ToArray();
            var count = numbers[0];
            var from = numbers[1] - 1;
            var to = numbers[2] - 1;

            string move = "";
            for (int i = 0; i < count; i++)
            {
                move += stacks[from].Pop();
            }

            foreach (var letter in move.Reverse()) {
                stacks[to].Push(letter);
            }
        }

        return string.Join("", stacks.OfType<Stack<char>>().Select(q => q.Pop()));
    }
}
