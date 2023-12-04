using AoE2023.Utils;

namespace AoE2023;

public class Day4 : StringListDay {
    protected override object FirstTask() {
        return this.Input.Select(line => {
            var numbers = line.Split(":")[1].Trim();
            var numberLists = numbers.Split("|").Select(n => n.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray();
            var commonPart = numberLists[0].Intersect(numberLists[1]);
            return commonPart.Count() is >= 1 ? Math.Pow(2, commonPart.Count() - 1) : 0;
        })
        .Sum();
    }

    protected override object SecondTask() {
        var lists = this.Input.Select((line, index) => {
            var numbers = line.Split(":")[1].Trim();
            var numberLists = numbers.Split("|").Select(n => n.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)).ToArray();
            return (numberLists.First().ToArray(), numberLists.Skip(1).First().ToArray(), index);
        }).ToArray();

        var copies = new Dictionary<int, int>();
        for (int i = 0; i < lists.Length; i++) {
            copies.Add(i, 1);
        }

        foreach (var card in lists) {
            var score = card.Item1.Intersect(card.Item2).Count();
            for (int i = 1; i <= score; i++) {
                copies[card.index + i] += copies[card.index];
            }
        }

        return copies.Sum(kv => kv.Value);
    }
}