using AoE2021.Utils;
using System.Text.RegularExpressions;

namespace AoE2021
{
    public class Day2 : Day
    {
        public Day2() : base("day2")
        {

        }
        protected override object FirstTask()
        {
            var input = _inputLoader.LoadStringListInput();
            var regex = "(.+)\\s(\\d+)";

            long horizontal = 0;
            long depth = 0;
            foreach (var x in input)
            {
                var match = Regex.Match(x, regex);
                var action = match.Groups[1].Value;
                var val = int.Parse(match.Groups[2].Value);
                switch (action)
                {
                    case "forward":
                        {
                            horizontal += val;
                            break;
                        }
                    case "up":
                        {
                            depth -= val;
                            break;
                        }
                    case "down":
                        {
                            depth += val;
                            break;
                        }
                }
            }

            return horizontal * depth;
        }

        protected override object SecondTask()
        {
            var input = _inputLoader.LoadStringListInput();
            var regex = "(.+)\\s(\\d+)";

            long horizontal = 0;
            long aim = 0;
            long depth = 0;
            foreach (var x in input)
            {
                var match = Regex.Match(x, regex);
                var action = match.Groups[1].Value;
                var val = int.Parse(match.Groups[2].Value);
                switch (action)
                {
                    case "forward":
                        {
                            horizontal += val;
                            depth += aim * val;
                            break;
                        }
                    case "up":
                        {
                            aim -= val;
                            break;
                        }
                    case "down":
                        {
                            aim += val;
                            break;
                        }
                }
            }

            return horizontal * depth;
        }
    }
}
