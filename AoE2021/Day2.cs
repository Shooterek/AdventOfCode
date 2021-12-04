using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoE2021
{
    public class Day2 : Day
    {
        public Day2(string inputPath) : base(inputPath)
        {

        }
        public override string FirstTask()
        {
            var input = _inputLoader.LoadStringListInput(this._inputPath);
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

            return (horizontal * depth).ToString();
        }

        public override string SecondTask()
        {
            var input = _inputLoader.LoadStringListInput(this._inputPath);
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

            return (horizontal * depth).ToString();
        }
    }
}
