using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day7 : StringListDay
{
    protected override object FirstTask()
    {
        var cards = this.Input.Select(line =>
        {
            var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var score = GetScore(split[0]);
            return (card: split[0], number: long.Parse(split[1]), score);
        });

        var x = cards.Order(new CardComparer(27));

        return x.Select((c, index) => c.number * (index + 1)).Sum();
    }

    protected override object SecondTask()
    {
        var cards = this.Input.Select(line =>
        {
            var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var maxScore = split[0].Select(character =>
            {
                var newString = split[0].Replace('J', character);
                return GetScore(newString);
            })
                .Max();
            return (card: split[0], number: long.Parse(split[1]), maxScore);
        });

        var x = cards.Order(new CardComparer(1));

        return x.Select((c, index) => c.number * (index + 1)).Sum();
    }

    private int GetScore(string input)
    {
        var cardGroups = input.GroupBy(c => c).ToArray();
        var groupCounts = cardGroups.Select(cg => cg.Count()).OrderDescending().ToArray();
        return groupCounts switch
        {
            [var a] => 10,
            [var a, var b] when a == 4 => 9,
            [_, _] => 8,
            [var a, var b, _] when a == 3 => 7,
            [_, _, _] => 6,
            [_, _, _, _] => 5,
            _ => 4
        };
    }
}

file class CardComparer : IComparer<(string card, long number, int score)>
{
    private readonly int jValue;

    public CardComparer(int jValue)
    {
        this.jValue = jValue;

        this.valueMap = new()
        {
            ['A'] = 30,
            ['K'] = 29,
            ['Q'] = 28,
            ['J'] = this.jValue,
            ['T'] = 25,
            ['9'] = 24,
            ['8'] = 23,
            ['7'] = 22,
            ['6'] = 21,
            ['5'] = 10,
            ['4'] = 8,
            ['3'] = 6,
            ['2'] = 2,
        };
    }

    private readonly Dictionary<char, int> valueMap;

    public int Compare((string card, long number, int score) x1, (string card, long number, int score) x2)
    {
        if (x1.score != x2.score)
            return x1.score > x2.score ? 1 : -1;

        for (int i = 0; i < x1.card.Length; i++)
        {
            var c1 = this.valueMap[x1.card[i]];
            var c2 = this.valueMap[x2.card[i]];

            if (c1 != c2)
            {
                return c1 > c2 ? 1 : -1;
            }
        }

        return 0;
    }
}