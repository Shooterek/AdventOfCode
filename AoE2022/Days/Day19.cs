using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day19 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    protected override object FirstTask()
    {
        var blueprints = this.Input.Select(line => new Blueprint(NumberPattern.Matches(line).Select(m => int.Parse(m.Captures.First().Value)).ToList())).ToList();
        return blueprints.Select((b, index) => MaxGeodes(b) * (index + 1)).Sum();
    }

    private int MaxGeodes(Blueprint bp)
    {
        var max = 0;
        BruteForce(1, 0, 0, 0, 0, 0, 0, 0, 24);
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
            else
            {
                if (clay >= bp.ObsidianRobotClayCost && ore >= bp.ObsidianRobotOreCost)
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
                }
                if (ore >= bp.OreRobotCost && ore < 25)
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
                if (ore >= bp.ClayRobotCost && clay < 25)
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

    protected override object SecondTask()
    {
        throw new NotImplementedException();
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
        }

        public int OreRobotCost { get; set; }

        public int ClayRobotCost { get; set; }

        public int ObsidianRobotOreCost { get; set; }

        public int ObsidianRobotClayCost { get; set; }

        public int GeodeRobotOreCost { get; set; }

        public int GeodeRobotObsidianCost { get; set; }
    }
}
