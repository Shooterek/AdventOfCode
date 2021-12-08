using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day8 : Day
    {
        public Day8() : base("day8")
        {
        }

        protected override object FirstTask()
        {
            var lines = this._inputLoader.LoadStringListInput();
            var numberLengths = new List<int>() {2, 3, 4, 7};
            var numbers = lines.Select(line => line.Split("|")[1])
                .Select(secondPart => secondPart.Split(" ").Count(n => numberLengths.Contains(n.Length)))
                .Sum();

            return numbers;
        }

        protected override object SecondTask()
        {
            var lines = this._inputLoader.LoadStringListInput();
            var finalSum = lines.Aggregate(0, (value, line) =>
            {
                var input = line.Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var result = line.Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var dict = new List<(string, int)>();

                var digit1 = input.Single(x => x.Length == 2);
                var digit4 = input.Single(x => x.Length == 4);
                var diff1and4 = digit4.Except(digit1);
                var digit7 = input.Single(x => x.Length == 3);
                var diff1and7 = digit7.Except(digit1);
                var digit8 = input.Single(x => x.Length == 7);
                var digit6 = input.Single(x => x.Length == 6 && x.Intersect(digit1).Count() == 1);
                var digit0 = input.Single(x => x.Length == 6 && x.Intersect(diff1and4).Count() == 1);
                var digit9 = input.Single(x => x.Length == 6 && x != digit0 && x != digit6);
                var digit5 = input.Single(x => x.Length == 5 && x.Intersect(digit1).Count() == 1 && digit9.Intersect(x).Count() == 5);
                var digit2 = input.Single(x => x.Length == 5 && x.Intersect(digit5).Count() == 3);
                var intsersect2and5 = digit2.Intersect(digit5).Append(digit1[1]).Append(digit1[0]);
                var digit3 = input.Single(x => x.Length == 5 && x.Intersect(intsersect2and5).Count() == 5);
            
                dict.Add((digit0, 0));
                dict.Add((digit1, 1));
                dict.Add((digit2, 2));
                dict.Add((digit3, 3));
                dict.Add((digit4, 4));
                dict.Add((digit5, 5));
                dict.Add((digit6, 6));
                dict.Add((digit7, 7));
                dict.Add((digit8, 8));
                dict.Add((digit9, 9));

                return value + dict.First(x => x.Item1.Length == result[0].Length && x.Item1.Intersect(result[0]).Count() == x.Item1.Length).Item2 * 1000
                       + dict.First(x => x.Item1.Length == result[1].Length && x.Item1.Intersect(result[1]).Count() == x.Item1.Length).Item2 * 100
                       + dict.First(x => x.Item1.Length == result[2].Length && x.Item1.Intersect(result[2]).Count() == x.Item1.Length).Item2 * 10
                       + dict.First(x => x.Item1.Length == result[3].Length && x.Item1.Intersect(result[3]).Count() == x.Item1.Length).Item2;
            });
            return finalSum;
        }
        
    }
}
