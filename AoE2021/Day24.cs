using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day24 : Day
	{
		private List<(int index, List<string> instructions)> instrGroups = new();
		public Day24() : base("day24")
		{
		}

		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringListInput();

			var x = input.Select((item, index) => item.Contains("inp") ? index : null as int?).OfType<int>().ToList();
			for (int i = 0; i < x.Count; i++)
			{
				if (i < x.Count - 1)
					this.instrGroups.Add((i, input.Skip(x[i] + 1).Take(x[i + 1] - x[i] - 1).ToList()));
				else
					this.instrGroups.Add((i, input.Skip(x[i] + 1).Take(input.Count - x[i] - 1).ToList()));
			}

			var values = new List<long>();
			var cache = new List<(int, int)>();

			Apply("", 0, 0);

			return "";

			void Apply(string number, int index, byte z)
			{
				if (index == this.instrGroups.Count)
				{
					var x = long.Parse(number);
					values.Add(x);
					Console.WriteLine(x);
					return;
				}

				var instr = this.instrGroups.First(x => x.index == index).instructions;
				for (byte i = 9; i > 0; i--)
				{
					var output = GetValue(instr, z, i);

					if (cache.Contains((i, output)))
					{
						return;
					}

					Apply($"{number}{i}", index + 1, output);
					cache.Add((i, output));
				}
			}
		}

		protected override object SecondTask()
		{
			return "";
		}

		private byte GetValue(List<string> instr, int z, int w)
		{
			var x = 0;
			var y = 0;
			foreach (var inst in instr)
			{
				if (inst.Contains("mul"))
				{
					var split = inst.Split(" ");
					var src = split[2];

					var srcValue = src switch
					{
						"z" => z,
						"w" => w,
						"x" => x,
						"y" => y,
						_ => int.Parse(src),
					};

					var dest = inst.Split(" ")[1];
					if (dest == "z")
						z *= srcValue;
					if (dest == "y")
						y *= srcValue;
					if (dest == "x")
						x *= srcValue;
					if (dest == "w")
						w *= srcValue;
				}
				else if (inst.Contains("add"))
				{
					var split = inst.Split(" ");
					var src = split[2];

					var srcValue = src switch
					{
						"z" => z,
						"w" => w,
						"x" => x,
						"y" => y,
						_ => int.Parse(src),
					};

					var dest = split[1];
					if (dest == "z")
						z += srcValue;
					if (dest == "y")
						y += srcValue;
					if (dest == "x")
						x += srcValue;
					if (dest == "w")
						w += srcValue;
				}
				else if (inst.Contains("div"))
				{
					var split = inst.Split(" ");
					var src = split[2];

					var srcValue = src switch
					{
						"z" => z,
						"w" => w,
						"x" => x,
						"y" => y,
						_ => int.Parse(src),
					};

					var dest = split[1];
					if (dest == "z")
						z /= srcValue;
					if (dest == "y")
						y /= srcValue;
					if (dest == "x")
						x /= srcValue;
					if (dest == "w")
						w /= srcValue;
				}
				else if (inst.Contains("mod"))
				{
					var split = inst.Split(" ");
					var src = split[2];

					var srcValue = src switch
					{
						"z" => z,
						"w" => w,
						"x" => x,
						"y" => y,
						_ => int.Parse(src),
					};

					var dest = split[1];
					if (dest == "z")
						z %= srcValue;
					if (dest == "y")
						y %= srcValue;
					if (dest == "x")
						x %= srcValue;
					if (dest == "w")
						w %= srcValue;
				}
				else if (inst.Contains("eql"))
				{
					var split = inst.Split(" ");
					var src = split[2];

					var srcValue = src switch
					{
						"z" => z,
						"w" => w,
						"x" => x,
						"y" => y,
						_ => int.Parse(src),
					};

					var dest = split[1];
					if (dest == "z")
						z = z == srcValue ? 1 : 0;
					if (dest == "y")
						y = y == srcValue ? 1 : 0;
					if (dest == "x")
						x = x == srcValue ? 1 : 0;
					if (dest == "w")
						w = w == srcValue ? 1 : 0;
				}
			}

			return (byte)z;
		}
	}
}
