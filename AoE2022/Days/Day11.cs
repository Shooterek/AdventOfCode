using AoE2022.Utils;
using System.Text.RegularExpressions;

public class Day11 : StringBatchesDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        var monkeys = new Dictionary<long, Monkey>();

        foreach (var batch in this.Input) {
            var lines = batch.Split("\r ");
            var monkeyNumber = long.Parse(NumberPattern.Match(lines[0]).Captures[0].Value);
            var items = NumberPattern.Matches(lines[1]).Select(c => long.Parse(c.Captures[0].Value));
            var operationValue = NumberPattern.Match(lines[2]) is Match { Success: true } m ? long.Parse(NumberPattern.Match(lines[2]).Captures[0].Value) : null as long?;
            var operation = lines[2].Contains("*") ? Operation.Multiply : Operation.Add;
            var divisibleBy = long.Parse(NumberPattern.Match(lines[3]).Captures[0].Value);
            var trueMonkey = long.Parse(NumberPattern.Match(lines[4]).Captures[0].Value);
            var falseMonkey = long.Parse(NumberPattern.Match(lines[5]).Captures[0].Value);
            monkeys[monkeyNumber] = new Monkey(items.ToList(), divisibleBy, trueMonkey, falseMonkey, operation, operationValue);
        }

        for (long i = 0; i < 20; i++)
        {
            for (long j = 0; j < monkeys.Count(); j++)
            {
                foreach ((var target, var item) in monkeys[j].InspectItems()) {
                    monkeys[target].Items.Add(item);
                }

                monkeys[j] = monkeys[j] with { Items = new() };
            }
        }

        return monkeys.Select(m => m.Value).OrderByDescending(m => m.InspectedItems).Take(2).Aggregate(1L, (current, next) => current * next.InspectedItems);
    }

    protected override object SecondTask()
    {
        var monkeys = new Dictionary<long, Monkey>();

        foreach (var batch in this.Input) {
            var lines = batch.Split("\r ");
            var monkeyNumber = long.Parse(NumberPattern.Match(lines[0]).Captures[0].Value);
            var items = NumberPattern.Matches(lines[1]).Select(c => long.Parse(c.Captures[0].Value));
            var operationValue = NumberPattern.Match(lines[2]) is Match { Success: true } m ? long.Parse(NumberPattern.Match(lines[2]).Captures[0].Value) : null as long?;
            var operation = lines[2].Contains("*") ? Operation.Multiply : Operation.Add;
            var divisibleBy = long.Parse(NumberPattern.Match(lines[3]).Captures[0].Value);
            var trueMonkey = long.Parse(NumberPattern.Match(lines[4]).Captures[0].Value);
            var falseMonkey = long.Parse(NumberPattern.Match(lines[5]).Captures[0].Value);
            monkeys[monkeyNumber] = new Monkey(items.ToList(), divisibleBy, trueMonkey, falseMonkey, operation, operationValue, false);
        }
        
        for (long i = 0; i < 10000; i++)
        {
            for (long j = 0; j < monkeys.Count(); j++)
            {
                var moves = monkeys[j].InspectItems();
                foreach ((var target, var item) in moves) {
                    monkeys[target].Items.Add(item % 9699690);
                }
                monkeys[j] = monkeys[j] with { Items = new() };
            }
        }

        return monkeys.Select(m => m.Value).OrderByDescending(m => m.InspectedItems).Take(2).Aggregate(1L, (current, next) => current * next.InspectedItems);
    }

    private record Monkey(List<long> Items, long DivisibleBy, long MonkeyTrue, long MonkeyFalse, Operation Operation, long? OperationValue, bool Worry = true) {
        public long InspectedItems { get; private set; }
        public List<(long target, long item)> InspectItems()
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
