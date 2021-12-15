using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoE2021
{
    public class Day14 : Day
    {
        public Day14() : base("day14")
        {
        }

        protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var instr = input[0].ToList();

			var instructions = input[1].Split("\r").Select(x =>
			{
				var split = x.Split(" -> ");
				return (instr: split[0].Trim(), result: split[1].First());
			}).ToDictionary(x => x.instr, x => x.result);

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < instr.Count - 1; j += 2)
				{
					var z = $"{instr[j]}{instr[j + 1]}";
					var charToInsert = instructions[z];
					instr.Insert(1 + j, charToInsert);
				}
			}

			var orderedAmounts = instr.GroupBy(x => x).Select(x => x.Count()).OrderByDescending(y => y);

			return orderedAmounts.First() - orderedAmounts.Last();
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var instr = input[0];
			var rules = input[1].Split("\r").Select(x => x.Split(" -> ")).ToDictionary(x => x[0].Trim(), x => x[1]);

			var pairs = new Dictionary<string, long>();
			for (var i = 0; i < instr.Length - 1; i++)
			{
				var z = $"{instr[i]}{instr[i + 1]}";
				pairs[z] = pairs.GetValueOrDefault(z) + 1;
			}

			for (var i = 0; i < 40; i++)
			{
				var newPairs = new Dictionary<string, long>();
				foreach (var pair in pairs)
				{
					var charToinsert = rules[pair.Key];
					var p1 = $"{pair.Key[0]}{charToinsert}";
					var p2 = $"{charToinsert}{pair.Key[1]}";
					newPairs[p1] = newPairs.GetValueOrDefault(p1) + pair.Value;
					newPairs[p2] = newPairs.GetValueOrDefault(p2) + pair.Value;
				}
				pairs = newPairs;
			}

			var letterCount = new Dictionary<char, long>();
			foreach (var pair in pairs)
			{
				letterCount[pair.Key[0]] = letterCount.GetValueOrDefault(pair.Key[0]) + pair.Value;
				letterCount[pair.Key[1]] = letterCount.GetValueOrDefault(pair.Key[1]) + pair.Value;
			}

			letterCount[instr[0]]++;
			letterCount[instr.Last()]++;

			return (letterCount.Values.Max() - letterCount.Values.Min()) / 2;
		}
    }
}
