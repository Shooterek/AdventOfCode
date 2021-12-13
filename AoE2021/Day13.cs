using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day13 : Day
    {
        public Day13() : base("day13")
        {
        }

        protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var coordinates = input[0].Split("\r").Select(x =>
			{
				var temp = x.Replace(" ", "").Split(",");
				return (x: int.Parse(temp[0]), y: int.Parse(temp[1]));
			}).ToArray();

			var foldInstructions = input[1].Split("\r").Select(x => x.Replace("fold along ", "").TrimStart()).Select(x =>
			{
				var temp = x.Split("=");
				return (axis: temp[0], val: int.Parse(temp[1]));
			});

			var instr = foldInstructions.First();
			for (int i = 0; i < coordinates.Length; i++)
			{
				var val = coordinates[i];
				if (instr.axis == "x" && val.x > instr.val)
				{
					var newX = instr.val - (val.x - instr.val);
					val.x = newX;
					coordinates[i] = val;
				}
				if (instr.axis == "y" && val.y > instr.val)
				{
					var newY = instr.val - (val.y - instr.val);
					val.y = newY;
					coordinates[i] = val;
				}
			}

			return coordinates.Distinct().Count();
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var coordinates = input[0].Split("\r").Select(x =>
			{
				var temp = x.Replace(" ", "").Split(",");
				return (x: int.Parse(temp[0]), y: int.Parse(temp[1]));
			}).ToArray();

			var foldInstructions = input[1].Split("\r").Select(x => x.Replace("fold along ", "").TrimStart()).Select(x =>
			{
				var temp = x.Split("=");
				return (axis: temp[0], val: int.Parse(temp[1]));
			});

			foreach (var instr in foldInstructions)
			{
				for (int i = 0; i < coordinates.Length; i++)
				{
					var val = coordinates[i];
					if (instr.axis == "x" && val.x > instr.val)
					{
						var newX = instr.val - (val.x - instr.val);
						val.x = newX;
						coordinates[i] = val;
					}
					if (instr.axis == "y" && val.y > instr.val)
					{
						var newY = instr.val - (val.y - instr.val);
						val.y = newY;
						coordinates[i] = val;
					}
				}
				coordinates = coordinates.Distinct().ToArray();
			}

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					Console.Write(coordinates.Contains((j, i)) ? "#" : " ");
				}
				Console.WriteLine();
			}

			return "";
		}
    }
}
