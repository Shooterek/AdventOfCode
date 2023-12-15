using AoE2023.Utils;
using MoreLinq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day15 : EntireStringDay
{
    protected override object FirstTask()
        => GetValue(this.Input);

    protected override object SecondTask()
    {
        var lenses = this.Input.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(l =>
            {
                var parts = l.Split('-', '=').ToArray();
                var label = parts[0];
                var operation = l.Contains("=") ? '=' : '-';
                int? focalLength = operation == '=' ? int.Parse(parts[1]) : null;
                return new Lens(label, GetValue(label), operation, focalLength);
            }).ToArray();

        var boxesDictionary = new Dictionary<int, LinkedList<Lens>>();
        var boxesLookup = new Dictionary<(string, int), Lens>();

        lenses.Select(l => l.Box).Distinct().ForEach(l => {
            boxesDictionary.Add(l, new());
        });

        foreach (var lens in lenses) {
            var box = lens.Box;
            var lookupLens = boxesLookup.GetValueOrDefault((lens.Label, lens.Box));
            if (lens.Operation == '-' && lookupLens is {} remove) {
                boxesDictionary[lens.Box].Remove(remove);
                boxesLookup.Remove((remove.Label, remove.Box));
            }
            else if (lookupLens is {} replace && lens.Operation == '=') {
                var x = boxesDictionary[lens.Box].Find(replace);
                boxesDictionary[lens.Box].AddBefore(x, lens);
                boxesDictionary[lens.Box].Remove(x);
                
                boxesLookup.Remove((replace.Label, replace.Box));
                boxesLookup.Add((lens.Label, lens.Box), lens);
            }
            else if (lens.Operation == '=') {
                boxesDictionary[lens.Box].AddLast(lens);
                boxesLookup.Add((lens.Label, lens.Box), lens);
            }
        }

        var focal = boxesDictionary.SelectMany(box => box.Value.Select((lens, index) => (box.Key + 1) * lens.FocalLenth * (index + 1)));
        return focal.Sum();
    }

    private int GetValue(string input)
    {
        var sum = 0;
        var currentValue = 0;
        foreach (var c in input)
        {
            if (c == ',')
            {
                sum += currentValue;
                currentValue = 0;
                continue;
            }
            currentValue = (currentValue + c) * 17 % 256;
        }

        return sum + currentValue;
    }
}

file record Lens(string Label, int Box, char Operation, int? FocalLenth);
