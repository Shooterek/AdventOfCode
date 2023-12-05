using AoE2023.Utils;

namespace AoE2023;

public class Day5 : StringBatchesDay
{
    protected override object FirstTask()
    {
        var seeds = this.Input.First()
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
        var transformers = this.Input.Skip(1).Select(batch => {
            return batch.Split("\r").Skip(1).Select(line => {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return new Transformer(numbers[0], numbers[1], numbers[2]);
            }).ToArray();
        }).ToArray();

        var locations = seeds.Select(seed => {
            var val = seed;
            foreach (var transformerList in transformers) {
                var transformerToUse = transformerList.FirstOrDefault(t => val >= t.Source && val <= t.Source + t.Range - 1);
                if (transformerToUse == null) {
                    continue;
                }
                var offset = val - transformerToUse.Source;
                val = transformerToUse.Destination + offset;
            }
            return val;
        });

        return locations.Min();
    }

    protected override object SecondTask()
    {
        var seeds = this.Input.First()
            .Split(":")[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .Chunk(2)
            .Select(chunk => new CRange(chunk[0], chunk[1]))
            .ToArray();

        var transformers = this.Input.Skip(1).Select(batch => {
            return batch.Split("\r").Skip(1).Select(line => {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return new Transformer(numbers[0], numbers[1], numbers[2]);
            }).ToArray();
        }).ToArray();

        return null;
    }
}

file record Transformer(long Destination, long Source, long Range);

file record CRange(long Start, long Count);