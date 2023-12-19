using AoE2023.Utils;
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
    }

    protected override object SecondTask()
    {
        return null;
    }

    private (Dictionary<string, Func<Workflow, string?>> ratings, Workflow[] workflows) ParseInput()
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

            var r = (Workflow w) =>
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
                return new Workflow(parts[0], parts[1], parts[2], parts[3]);
            }).ToArray();

        return (workFlows, ratings);
    }

    private Func<Workflow, string?> GetFunc(string[] parts, char op, string ret)
    {
        return (Workflow src) =>
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

public record Workflow(int X, int M, int A, int S);