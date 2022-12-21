using AoE2022.Utils;

public class Day21 : StringListDay
{
    protected override object FirstTask()
    {
        var instructions = GetInstructions();

        return instructions["root"].GetValue(instructions);
    }

    protected override object SecondTask()
    {
        var instructions = GetInstructions();
    }

    private Dictionary<string, Action> GetInstructions()
    {
        var instructions = new Dictionary<string, Action>();
        foreach (var line in this.Input)
        {
            var split = line.Split(": ");
            Action act;
            if (char.IsDigit(split[1][0]))
            {
                act = new YellNumber(long.Parse(split[1]));
            }
            else
            {
                var opSplit = split[1].Split(" ");
                act = new OperationAction(opSplit[0], opSplit[2], opSplit[1][0]);
            }

            instructions.Add(split[0], act);
        }

        return instructions;
    }

    private abstract record Action()
    {
        public abstract long GetValue(Dictionary<string, Action> instructions);
    }

    private record YellNumber(long Number) : Action
    {
        public override long GetValue(Dictionary<string, Action> instructions) => this.Number;
    }

    private record OperationAction(string Left, string Right, char Operation) : Action
    {
        public override long GetValue(Dictionary<string, Action> instructions)
        {
            var leftValue = instructions[this.Left].GetValue(instructions);
            if (instructions[this.Left] is OperationAction)
                instructions[this.Left] = new YellNumber(leftValue);
            var rightValue = instructions[this.Right].GetValue(instructions);
            if (instructions[this.Right] is OperationAction)
                instructions[this.Right] = new YellNumber(rightValue);

            return this.Operation switch
            {
                '+' => leftValue + rightValue,
                '-' => leftValue - rightValue,
                '/' => leftValue / rightValue,
                '*' => leftValue * rightValue,
                '=' => leftValue == rightValue ? 1 : 0,
            };
        }
    }
}
