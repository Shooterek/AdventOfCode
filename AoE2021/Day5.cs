using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day5 : Day
    {
        public Day5(string inputPath) : base(inputPath)
        {
        }

        public override string FirstTask()
        {
            var input = _inputLoader.LoadStringListInput(this._inputPath);
            var x = new Dictionary<(int, int), int>();
            foreach (var inputLine in input)
            {
                var startEndSplit = inputLine.Split(" -> ").Select(x => x.Split(',')).ToArray();
                var x1 = int.Parse(startEndSplit[0][0]);
                var y1= int.Parse(startEndSplit[0][1]);

                var x2 = int.Parse(startEndSplit[1][0]);
                var y2 = int.Parse(startEndSplit[1][1]);

                if (x1 != x2 && y1 != y2)
                    continue;

                var stepCount = Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2)) + 1;
                var yDir = (y2 - y1) switch
                {
                    0 => 0,
                    < 0 => -1,
                    _ => 1,
                }; 
                var xDir = (x2 - x1) switch
                {
                    0 => 0,
                    < 0 => -1,
                    _ => 1,
                };

                for (int i = x1, j = y1; stepCount > 0; i += xDir, j += yDir, stepCount--)
                {
                    if (x.TryGetValue((i, j), out _))
                    {
                        x[(i, j)] += 1;
                        continue;
                    }

                    x.Add((i, j), 1);
                }
            }


            return x.Count(pair => pair.Value > 1).ToString();
        }

        public override string SecondTask()
        {
            var input = _inputLoader.LoadStringListInput(this._inputPath);
            var x = new Dictionary<(int, int), int>();
            foreach (var inputLine in input)
            {
                var startEndSplit = inputLine.Split(" -> ").Select(x => x.Split(',')).ToArray();
                var x1 = int.Parse(startEndSplit[0][0]);
                var y1 = int.Parse(startEndSplit[0][1]);

                var x2 = int.Parse(startEndSplit[1][0]);
                var y2 = int.Parse(startEndSplit[1][1]);

                var stepCount = Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2)) + 1;
                var yDir = (y2 - y1) switch
                {
                    0 => 0,
                    < 0 => -1,
                    _ => 1,
                };
                var xDir = (x2 - x1) switch
                {
                    0 => 0,
                    < 0 => -1,
                    _ => 1,
                };

                for (int i = x1, j = y1; stepCount > 0; i += xDir, j += yDir, stepCount--)
                {
                    if (x.TryGetValue((i, j), out _))
                    {
                        x[(i, j)] += 1;
                        continue;
                    }

                    x.Add((i, j), 1);
                }
            }


            return x.Count(pair => pair.Value > 1).ToString();
        }
    }
}
