using AoE2022.Utils;

public class Day21 : StringListDay
{
    private const string RootInstruction = "root";
    private const string HumanInstruction = "humn";
    private const string P2ValueToFind = "x";

    protected override object FirstTask()
    {
        var instructions = GetInstructions();

        return instructions[RootInstruction].GetValue(instructions);
    }

    protected override object SecondTask()
    {
        var instructions = GetInstructions();
        var root = (OperationAction)instructions[RootInstruction];
        instructions[RootInstruction] = new OperationAction(root.Left, root.Right, '=');
        instructions[HumanInstruction] = new YellNumber(P2ValueToFind);

        var val = instructions[RootInstruction].GetStringValue(instructions);
        Console.WriteLine(val);
        return 0;
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
                act = new YellNumber(split[1]);
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

        public abstract string GetStringValue(Dictionary<string, Action> instructions);
    }

    private record YellNumber(string Number) : Action
    {
        public override long GetValue(Dictionary<string, Action> instructions) => long.Parse(this.Number);

        public override string GetStringValue(Dictionary<string, Action> instructions) => this.Number.ToString();
    }

    private record OperationAction(string Left, string Right, char Operation) : Action
    {
        public override long GetValue(Dictionary<string, Action> instructions)
        {
            var leftValue = instructions[this.Left].GetValue(instructions);
            if (instructions[this.Left] is OperationAction)
                instructions[this.Left] = new YellNumber(leftValue.ToString());
            var rightValue = instructions[this.Right].GetValue(instructions);
            if (instructions[this.Right] is OperationAction)
                instructions[this.Right] = new YellNumber(rightValue.ToString());

            return this.Operation switch
            {
                '+' => leftValue + rightValue,
                '-' => leftValue - rightValue,
                '/' => leftValue / rightValue,
                '*' => leftValue * rightValue,
                '=' => leftValue == rightValue ? 1 : 0,
            };
        }

        public override string GetStringValue(Dictionary<string, Action> instructions)
        {
            var left = instructions[this.Left].GetStringValue(instructions);
            var right = instructions[this.Right].GetStringValue(instructions);

            if (!left.Contains(P2ValueToFind))
                left = instructions[this.Left].GetValue(instructions).ToString();

            if (!right.Contains(P2ValueToFind))
                right = instructions[this.Right].GetValue(instructions).ToString();
            return '(' + left + this.Operation + right + ')';
        }
    }
}
