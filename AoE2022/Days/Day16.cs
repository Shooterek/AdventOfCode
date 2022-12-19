using AoE2022.Utils;
using System.Text.RegularExpressions;

public class Day16 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    private static Regex ValvePattern = new Regex(@"[A-Z]{2}");

    protected override object FirstTask()
    {
        var map = new Dictionary<string, Node>();
        foreach (var line in this.Input)
        {
            var rate = int.Parse(NumberPattern.Match(line).Captures.First().Value);
            var valves = ValvePattern.Matches(line).Select(m => m.Captures.First().Value).ToArray();
            map.Add(valves[0], new(rate, valves[1..].ToList()));

        }
        var nodesWithFlowRate = map.Where(kv => kv.Value.Flow > 0 || kv.Key == "AA").ToList();
        var map2 = new Dictionary<string, List<(int, string)>>();

        foreach (var flowNode in nodesWithFlowRate)
        {
            map2[flowNode.Key] = new();
            var otherNodes = nodesWithFlowRate.Where(n => n.Key != flowNode.Key && n.Key != "AA").Select(n => n.Key).ToList();
            foreach (var other in otherNodes)
            {
                var pathLength = FindPaths(flowNode, map, otherNodes, other);
                if (pathLength is int path)
                    map2[flowNode.Key].Add((path, other));
            }
        }

        return FindBestScore(30, nodesWithFlowRate.Count - 1, map, map2);
    }

    protected override object SecondTask()
    {
        throw new NotImplementedException();
    }

    private int FindBestScore(int time, int maxCount, Dictionary<string, Node> map, Dictionary<string, List<(int, string)>> map2)
    {
        var scores = new List<(int, List<NodePath>)>();
        FindAllPaths("AA", time, new(), false);
        return scores.MaxBy(s => s.Item1).Item1;

        void FindAllPaths(string node, int remainingSeconds, List<NodePath> path, bool open)
        {
            if (open)
            {
                path.Add(new(remainingSeconds, node));
            }
            if (remainingSeconds < 1 || path.Count == maxCount)
            {
                var val = path.Where(s => s.Seconds > 0).Aggregate(0, (score, node) =>
                {
                    var n = map[node.Node];
                    return score + node.Seconds * n.Flow;
                });
                scores.Add((val, path));
                return;
            }

            foreach (var n in map2[node])
            {
                if (!path.Any(p => p.Node == n.Item2))
                {
                    FindAllPaths(n.Item2, remainingSeconds - n.Item1, new(path), true);
                }
            }
        }
    }

    private int? FindPaths(KeyValuePair<string, Node> flowNode, Dictionary<string, Node> allNodes, List<string> flowNodes, string nodeToFind)
    {
        var searchNodes = new List<string>() { flowNode.Key };
        var searchedNodes = new HashSet<string>();
        var length = 1;
        while (searchNodes.Count > 0)
        {
            if (length == 30)
                return null;

            if (searchedNodes.Contains(nodeToFind))
                return length;

            searchNodes = searchNodes.SelectMany(node => allNodes[node].Tunnels).Where(n => searchedNodes.Add(n)).ToList();
            length++;
        }

        return null;
    }

    private record Node(int Flow, List<string> Tunnels);

    private class NodePath
    {
        public NodePath(int Seconds, string Node)
        {
            this.Seconds = Seconds;
            this.Node = Node;
        }

        public int Seconds { get; set; }

        public string Node { get; set; }
    }
}
