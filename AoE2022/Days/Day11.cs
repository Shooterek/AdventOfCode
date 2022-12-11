using AoE2022.Utils;
using System.Text.RegularExpressions;

public class Day11 : StringBatchesDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        var monkeys = new Dictionary<int, Monkey>();

        foreach (var batch in this.Input) {
            var lines = batch.Split("\r ");
            var monkeyNumber = int.Parse(NumberPattern.Match(lines[0]).Captures[0].Value);
            var items = NumberPattern.Matches(lines[1]).Select(c => int.Parse(c.Captures[0].Value));
            var operationValue = NumberPattern.Match(lines[2]) is Match { Success: true } m ? int.Parse(NumberPattern.Match(lines[2]).Captures[0].Value) : null as int?;
            var operation = lines[2].Contains("*") ? Operation.Multiply : Operation.Add;
            var divisibleBy = int.Parse(NumberPattern.Match(lines[3]).Captures[0].Value);
            var trueMonkey = int.Parse(NumberPattern.Match(lines[4]).Captures[0].Value);
            var falseMonkey = int.Parse(NumberPattern.Match(lines[5]).Captures[0].Value);
            monkeys[monkeyNumber] = new Monkey(items.ToList(), divisibleBy, trueMonkey, falseMonkey, operation, operationValue);
        }

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < monkeys.Count(); j++)
            {
                foreach ((var target, var item) in monkeys[j].InspectItems()) {
                    monkeys[target].Items.Add(item);
                }

                monkeys[j] = monkeys[j] with { Items = new() };
            }
        }

        return monkeys.Select(m => m.Value).OrderByDescending(m => m.InspectedItems).Take(2).Aggregate(1, (current, next) => current * next.InspectedItems);
    }

    protected override object SecondTask()
    {
        var monkeys = new Dictionary<int, Monkey>();

        foreach (var batch in this.Input) {
            var lines = batch.Split("\r ");
            var monkeyNumber = int.Parse(NumberPattern.Match(lines[0]).Captures[0].Value);
            var items = NumberPattern.Matches(lines[1]).Select(c => int.Parse(c.Captures[0].Value));
            var operationValue = NumberPattern.Match(lines[2]) is Match { Success: true } m ? int.Parse(NumberPattern.Match(lines[2]).Captures[0].Value) : null as int?;
            var operation = lines[2].Contains("*") ? Operation.Multiply : Operation.Add;
            var divisibleBy = int.Parse(NumberPattern.Match(lines[3]).Captures[0].Value);
            var trueMonkey = int.Parse(NumberPattern.Match(lines[4]).Captures[0].Value);
            var falseMonkey = int.Parse(NumberPattern.Match(lines[5]).Captures[0].Value);
            monkeys[monkeyNumber] = new Monkey(items.ToList(), divisibleBy, trueMonkey, falseMonkey, operation, operationValue, false);
        }

        foreach (var monkey in monkeys) {
            monkeys[monkey.Key] = monkey.Value with { Items = new() };
        }

        monkeys[0] = monkeys[0] with { Items = new List<int>() {79} };

        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < monkeys.Count(); j++)
            {
                var moves = monkeys[j].InspectItems();
                foreach ((var target, var item) in moves) {
                    monkeys[target].Items.Add(item);
                }
                monkeys[j] = monkeys[j] with { Items = new() };
            }

            var x = monkeys.SelectMany(m => m.Value.Items);
            Console.WriteLine(x);
        }

        return monkeys.Select(m => m.Value).OrderByDescending(m => m.InspectedItems).Take(2).Aggregate(1f, (current, next) => current * next.InspectedItems);
    }

    private record Monkey(List<int> Items, int DivisibleBy, int MonkeyTrue, int MonkeyFalse, Operation Operation, int? OperationValue, bool Worry = true) {
        public int InspectedItems { get; private set; }
        public List<(int target, int item)> InspectItems()
        {
            this.InspectedItems += this.Items.Count;
            return this.Items.Select(i =>
            {
                return (Operation, OperationValue) switch
                {
                    (Operation.Add, null) => 2 * i,
                    (Operation.Add, var val) => i + val.Value,
                    (Operation.Multiply, null) => i * i,
                    (Operation.Multiply, var val) => i * val.Value,
                    _ => throw new Exception(),
                };
            })
            .Select(i => Worry ? i / 3 : i)
            .Select(i => (i % DivisibleBy == 0 ? MonkeyTrue : MonkeyFalse, i))
            .ToList();
        }
    }

    private enum Operation {
        Add,
        Multiply,
    }
}
