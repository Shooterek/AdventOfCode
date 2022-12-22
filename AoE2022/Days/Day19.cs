using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day19 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        var blueprints = this.Input.Select(line => new Blueprint(NumberPattern.Matches(line).Select(m => int.Parse(m.Captures.First().Value)).ToList())).ToList();
        return blueprints.Select((b, index) => MaxGeodes(b, 24, 6) * (index + 1)).Sum();
    }

    protected override object SecondTask()
    {
        var blueprints = this.Input.Select(line => new Blueprint(NumberPattern.Matches(line).Select(m => int.Parse(m.Captures.First().Value)).ToList())).ToList();
        return blueprints.Take(3).Select((b, index) => MaxGeodes(b, 32, 12)).Aggregate((curr, next) => curr * next);
    }

    private int MaxGeodes(Blueprint bp, int iterations, int minOreTime)
    {
        var max = 0;
        BruteForce(1, 0, 0, 0, 0, 0, 0, 0, iterations);
        Console.WriteLine(max);
        return max;

        void BruteForce(int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots, int ore, int clay, int obsidian, int geo, int timeLeft)
        {
            if (timeLeft == 0)
            {
                if (geo > max)
                    max = geo;
                return;
            }

            if (obsidian >= bp.GeodeRobotObsidianCost && ore >= bp.GeodeRobotOreCost)
            {
                BruteForce(oreRobots,
                           clayRobots,
                           obsidianRobots,
                           geodeRobots + 1,
                           ore - bp.GeodeRobotOreCost + oreRobots,
                           clay + clayRobots,
                           obsidian - bp.GeodeRobotObsidianCost + obsidianRobots,
                           geo + geodeRobots,
                           timeLeft - 1);
            }
            else if (clay >= bp.ObsidianRobotClayCost && ore >= bp.ObsidianRobotOreCost)
                {
                    BruteForce(oreRobots,
                               clayRobots,
                               obsidianRobots + 1,
                               geodeRobots,
                               ore - bp.ObsidianRobotOreCost + oreRobots,
                               clay - bp.ObsidianRobotClayCost + clayRobots,
                               obsidian + obsidianRobots,
                               geo + geodeRobots,
                               timeLeft - 1);

                // removable for part 2
                BruteForce(oreRobots,
                           clayRobots,
                           obsidianRobots,
                           geodeRobots,
                           ore + oreRobots,
                           clay + clayRobots,
                           obsidian + obsidianRobots,
                           geo + geodeRobots,
                           timeLeft - 1);
                }
            else
            {
                
                if (ore >= bp.OreRobotCost && oreRobots < bp.MaxOreCost && timeLeft > minOreTime)
                {
                    BruteForce(oreRobots + 1,
                    clayRobots,
                    obsidianRobots,
                    geodeRobots,
                    ore - bp.OreRobotCost + oreRobots,
                    clay + clayRobots,
                    obsidian + obsidianRobots,
                    geo + geodeRobots,
                    timeLeft - 1);
                }
                if (ore >= bp.ClayRobotCost && timeLeft > minOreTime)
                {
                    BruteForce(oreRobots,
                               clayRobots + 1,
                               obsidianRobots,
                               geodeRobots,
                               ore - bp.ClayRobotCost + oreRobots,
                               clay + clayRobots,
                               obsidian + obsidianRobots,
                               geo + geodeRobots,
                               timeLeft - 1);
                }
                BruteForce(oreRobots,
                           clayRobots,
                           obsidianRobots,
                           geodeRobots,
                           ore + oreRobots,
                           clay + clayRobots,
                           obsidian + obsidianRobots,
                           geo + geodeRobots,
                           timeLeft - 1);
            }
        }
    }

    private class Blueprint
    {
        public Blueprint(List<int> costs)
        {
            this.OreRobotCost = costs[1];
            this.ClayRobotCost = costs[2];
            this.ObsidianRobotOreCost = costs[3];
            this.ObsidianRobotClayCost = costs[4];
            this.GeodeRobotOreCost = costs[5];
            this.GeodeRobotObsidianCost = costs[6];

            this.MaxOreCost = new List<int> { this.ClayRobotCost, this.ObsidianRobotOreCost, this.GeodeRobotOreCost }.Max();
        }

        public int OreRobotCost { get; set; }

        public int ClayRobotCost { get; set; }

        public int ObsidianRobotOreCost { get; set; }

        public int ObsidianRobotClayCost { get; set; }

        public int GeodeRobotOreCost { get; set; }

        public int GeodeRobotObsidianCost { get; set; }

        public int MaxOreCost { get; set; }
    }
}
