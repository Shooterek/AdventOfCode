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
        var transformers = this.Input.Skip(1).Select(batch =>
        {
            return batch.Split("\r").Skip(1).Select(line =>
            {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return new Transformer(numbers[1], numbers[0], numbers[2]);
            }).ToArray();
        }).ToArray();

        var locations = seeds.Select(seed =>
        {
            var val = seed;
            foreach (var transformerList in transformers)
            {
                var transformerToUse = transformerList.FirstOrDefault(t => val >= t.Start && val <= t.Start + t.Range - 1);
                if (transformerToUse == null)
                {
                    continue;
                }
                var offset = val - transformerToUse.Start;
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
            .Select(chunk => new CRange(chunk[0], chunk[1], -1))
            .ToArray();

        var transformers = this.Input.Skip(1).Select(batch =>
        {
            return batch.Split("\r").Skip(1).Select(line =>
            {
                var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return new Transformer(numbers[1], numbers[0], numbers[2]);
            }).ToArray();
        }).ToArray();

        var locations = seeds.SelectMany(seed =>
        {
            var gen = 0;
            var unprocessed = seeds.ToList().ToHashSet();
            foreach (var transformerList in transformers)
            {
                HashSet<CRange> processed = new();
                List<CRange> ranges = new();
                foreach (var range in unprocessed)
                {
                    ranges = new() {range};
                    foreach (var transformer in transformerList)
                    {
                        var result = transformer.Transform(ranges, gen, out ranges);
                        foreach (var r in result) {
                            processed.Add(r);
                        }
                    }
                    foreach (var x in ranges) {
                        processed.Add(x);
                    }
                }
                unprocessed = processed.ToHashSet();
            }

            return unprocessed;
        }).ToArray();

        locations = locations.OrderBy(l => l.Start).ToArray();
        return locations.First().Start;
    }
}

file record Transformer(long Start, long Destination, long Range)
{
    public long End => this.Start + this.Range - 1;

    public IEnumerable<CRange> Transform(IEnumerable<CRange> rangesToTransform, int gen, out List<CRange> unprocessedRanges)
    {
        unprocessedRanges = new List<CRange>();
        var result = new List<CRange>();
        foreach (var r in rangesToTransform)
        {
            if (r.Start >= this.Start && r.End <= this.End)
            {
                var offset = r.Start - this.Start;
                result.Add(r with { Start = this.Destination + offset, Generation = gen });
            }
            // If range ends within the range of current transformer
            else if (r.End >= this.Start && r.End <= this.End)
            {
                var processedLength = r.End - this.Start;
                var unprocessedRange = new CRange(r.Start, r.Count - processedLength, gen);
                var processedRange = new CRange(this.Destination, Math.Min(r.Count, processedLength), gen);

                result.Add(processedRange);
                if (unprocessedRange.Count > 0)
                {
                    unprocessedRanges.Add(unprocessedRange);
                }
            }
            // If range starts within the range of current transformer
            else if (r.Start >= this.Start && r.Start <= this.End)
            {
                var processedLength = this.End - r.Start + 1;
                var offset = r.Start - this.Start;
                var unprocessedRange = new CRange(this.End + 1, r.Count - processedLength, gen);
                var processedRange = new CRange(this.Destination + offset, processedLength, gen);

                result.Add(processedRange);
                if (unprocessedRange.Count > 0)
                {
                    unprocessedRanges.Add(unprocessedRange);
                }
            }
            else if (r.Start < this.Start && r.End > this.End)
            {
                var processedRange = new CRange(this.Destination, this.Range, gen);
                var left = new CRange(r.Start, this.Start - r.Start, gen);
                var right = new CRange(r.End, r.End - this.End, gen);
                unprocessedRanges.Add(left);
                unprocessedRanges.Add(right);
                result.Add(processedRange);
            }
            else
            {
                unprocessedRanges.Add(r);
            }
        }

        return result;
    }
}

file record CRange(long Start, long Count, int Generation)
{
    public long End => this.Start + this.Count - 1;
}