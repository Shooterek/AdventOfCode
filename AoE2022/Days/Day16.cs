using AoE2022.Utils;
using System.Text.RegularExpressions;

public class Day16 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    private static Regex ValvePattern = new Regex(@"[A-Z]{2}");
    protected override object FirstTask()
    {
        var map = new Dictionary<string, Node>();
        foreach (var line in this.Input) {
            var rate = int.Parse(NumberPattern.Match(line).Captures.First().Value);
            var valves = ValvePattern.Matches(line).Select(m => m.Captures.First().Value).ToArray();
            map.Add(valves[0], new(rate, valves[1..].ToList()));

        }
        var nodesWithFlowRate = map.Where(kv => kv.Value.Flow > 0).ToList();
        return 0;
    }

    protected override object SecondTask()
    {
        throw new NotImplementedException();
    }

    private record Node(int Flow, List<string> Tunnels);
}
