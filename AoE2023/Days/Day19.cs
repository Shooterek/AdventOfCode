using AoE2023.Utils;
using BenchmarkDotNet.Environments;
using FluentAssertions;
using MoreLinq;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day19 : StringBatchesDay
{
    private readonly Regex regex = new Regex(@"{.+}");
    protected override object FirstTask()
    {
        (var workflows, var ratings) = ParseInput();

        var sum = 0;
        foreach (var rating in ratings)
        {
            var result = "in";

            while (result != "A" && result != "R")
            {
                result = workflows[result](rating);
            }

            if (result == "A")
            {
                sum += rating.A + rating.X + rating.S + rating.M;
            }
        }

        return sum;

        (Dictionary<string, Func<Rating, string?>> ratings, Rating[] workflows) ParseInput()
        {
            var workFlows = this.Input[0].Split("\r", StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var match = this.regex.Match(line);
                var name = line[0..match.Index].Trim();

                var val = match.Value.Replace("{", "").Replace("}", "");
                var expressions = val.Split(",").Select(exp =>
                {
                    if (!exp.Contains(":"))
                        return (_) => exp;

                    var exprParts = exp.Split(":");
                    var actParts = exprParts[0].Split('<', '>');
                    var op = exprParts[0].First(c => c == '<' || c == '>');
                    return GetFunc(actParts, op, exprParts[1]);
                }).ToArray();

                var r = (Rating w) =>
                {
                    foreach (var exp in expressions)
                    {
                        if (exp(w) is { } result)
                            return result;
                    }

                    throw new Exception();
                };

                return (name, r);
            }).ToDictionary(v => v.name, v => v.r);

            var ratings = this.Input[1].Split("\r", StringSplitOptions.RemoveEmptyEntries)
                .Select(w =>
                {
                    var parts = w.Split(",").Select(p => int.Parse(string.Join("", p.Split("=")[1].Where(c => char.IsDigit(c))))).ToArray();
                    return new Rating(parts[0], parts[1], parts[2], parts[3]);
                }).ToArray();

            return (workFlows, ratings);
        }

        Func<Rating, string?> GetFunc(string[] parts, char op, string ret)
        {
            return (Rating src) =>
            {
                var valToCheck = parts[0] switch
                {
                    "a" => src.A,
                    "x" => src.X,
                    "s" => src.S,
                    "m" => src.M,
                };

                var check = int.Parse(parts[1]);
                if (op == '>' && valToCheck > check)
                    return ret;

                if (op == '<' && valToCheck < check)
                    return ret;

                return null;
            };
        }
    }

    protected override object SecondTask()
    {
        var sum = 0L;
        var workflows = ParseInput();
        var startingRange = new RatingRanges(new(1, 4000), new(1, 4000), new(1, 4000), new(1, 4000));

        var a = new WorkflowGroup("A", [new WorkflowOperation(0, 'a', null, "A")]);
        var r = new WorkflowGroup("R", [new WorkflowOperation(0, 'r', null, "R")]);
        workflows.Add("A", a);
        workflows.Add("R", r);

        var start = workflows["in"];
        return start.GetCombinations(workflows, startingRange);

        Dictionary<string, WorkflowGroup> ParseInput()
        {
            return this.Input[0].Split("\r", StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var match = this.regex.Match(line);
                var name = line[0..match.Index].Trim();

                var val = match.Value.Replace("{", "").Replace("}", "");
                var expressions = val.Split(",").Select(exp =>
                {
                    if (!exp.Contains(":"))
                        return new WorkflowOperation(0, 'x', null, exp);

                    var exprParts = exp.Split(":");
                    var actParts = exprParts[0].Split('<', '>');
                    var op = exprParts[0].First(c => c == '<' || c == '>');
                    return new WorkflowOperation(int.Parse(actParts[1]), actParts[0][0], exprParts[0].First(c => c == '<' || c == '>'), exprParts[1]);
                }).ToArray();

                return (name, gr: new WorkflowGroup(name, expressions));
            }).ToDictionary(v => v.name, v => v.gr);
        }
    }
}

public record Rating(int X, int M, int A, int S);

public record RatingRanges(RatingRange X, RatingRange M, RatingRange A, RatingRange S);

public record RatingRange(int Start, int End) {
    public long Length => this.End - this.Start + 1;

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}";
    }
}


public record WorkflowGroup(string Name, WorkflowOperation[] Operations)
{
    public long GetCombinations(Dictionary<string, WorkflowGroup> workflows, RatingRanges rangeToCheck)
    {
        var sum = 0L;
        foreach (var w in this.Operations) {
            (var score, rangeToCheck) = w.GetAcceptedCombinations(workflows, rangeToCheck);
            sum += score;

            if (rangeToCheck == null)
                break;
        }

        return sum;
    }
}

public static class Temp {
    public static List<RatingRanges> ranges = new();
}

public record WorkflowOperation(int ValueCheck, char Property, char? Operation, string ReturnType)
{
    public (long, RatingRanges?) GetAcceptedCombinations(Dictionary<string, WorkflowGroup> workflows, RatingRanges range)
    {
        var sum = 0L;
        if (this.Operation is null)
        {
            if (this.ReturnType == "A")
            {
                return (range.X.Length * range.M.Length * range.A.Length * range.S.Length, null);
            }

            if (this.ReturnType == "R")
                return (0, null);

            return (workflows[this.ReturnType].GetCombinations(workflows, range), null);
        }

        var rangeToCompare = this.Property switch
        {
            'a' => range.A,
            'x' => range.X,
            's' => range.S,
            'm' => range.M,
        };

        RatingRange? currentRange = null;
        RatingRange? remainingRange = null;
        if (this.Operation == '>')
        {
            if (rangeToCompare.End <= this.ValueCheck)
            {
                currentRange = null;
                remainingRange = rangeToCompare;
            }
            else if (rangeToCompare.Start > ValueCheck)
            {
                currentRange = rangeToCompare;
            }
            else
            {
                currentRange = new RatingRange(ValueCheck + 1, rangeToCompare.End);
                remainingRange = new RatingRange(rangeToCompare.Start, ValueCheck);
            }
        }
        if (this.Operation == '<')
        {
            if (rangeToCompare.Start >= this.ValueCheck)
            {
                currentRange = null;
                remainingRange = rangeToCompare;
            }
            else if (rangeToCompare.End < ValueCheck)
            {
                currentRange = rangeToCompare;
            }
            else
            {
                currentRange = new RatingRange(rangeToCompare.Start, ValueCheck - 1);
                remainingRange = new RatingRange(ValueCheck, rangeToCompare.End);
            }
        }

        if (currentRange != null) {
            sum += workflows[this.ReturnType].GetCombinations(workflows, Build(range, currentRange, this.Property));
        }

        return (sum, Build(range, remainingRange, this.Property));
    }

    private RatingRanges? Build(RatingRanges src, RatingRange? changed, char operation) {
        if (changed == null)
            return null;

        var xRange = operation == 'x' ? changed : src.X;
        var mRange = operation == 'm' ? changed : src.M;
        var aRange = operation == 'a' ? changed : src.A;
        var sRange = operation == 's' ? changed : src.S;

        return new(xRange, mRange, aRange, sRange);
    }
}