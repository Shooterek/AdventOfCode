using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day22 : Day
	{
		public Day22() : base("day22")
		{
		}

		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringListInput();
			var dict = new Dictionary<(int x, int y, int z), bool>();
			
			foreach (var line in input)
			{
				var split = line.Split(" ");
				var shouldBeOn = split[0] == "on";

				var axsSplit = split[1].Split(",");

				var xMin = int.Parse(axsSplit[0].Split("=")[1].Split("..")[0]);
				var xMax = int.Parse(axsSplit[0].Split("=")[1].Split("..")[1]);

				var yMin = int.Parse(axsSplit[1].Split("=")[1].Split("..")[0]);
				var yMax = int.Parse(axsSplit[1].Split("=")[1].Split("..")[1]);

				var zMin = int.Parse(axsSplit[2].Split("=")[1].Split("..")[0]);
				var zMax = int.Parse(axsSplit[2].Split("=")[1].Split("..")[1]);

				for (int x = xMin < -50 ? -50 : xMin; x <= (xMax > 50 ? 50 : xMax); x++)
				{
					for (int y = yMin < -50 ? -50 : yMin; y <= (yMax > 50 ? 50 : yMax); y++)
					{
						for (int z = zMin < -50 ? -50 : zMin; z <= (zMax > 50 ? 50 : zMax); z++)
						{
							dict[(x, y, z)] = shouldBeOn;
						}
					}
				}
			}

			return dict.Count(x => x.Value);
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringListInput();
			var dict = new Dictionary<(int x, int y, int z), bool>();

			foreach (var line in input)
			{
				Console.WriteLine(line);
				var split = line.Split(" ");
				var shouldBeOn = split[0] == "on";

				var axsSplit = split[1].Split(",");

				var xMin = int.Parse(axsSplit[0].Split("=")[1].Split("..")[0]);
				var xMax = int.Parse(axsSplit[0].Split("=")[1].Split("..")[1]);

				var yMin = int.Parse(axsSplit[1].Split("=")[1].Split("..")[0]);
				var yMax = int.Parse(axsSplit[1].Split("=")[1].Split("..")[1]);

				var zMin = int.Parse(axsSplit[2].Split("=")[1].Split("..")[0]);
				var zMax = int.Parse(axsSplit[2].Split("=")[1].Split("..")[1]);

				for (int x = xMin; x <= xMax; x++)
				{
					for (int y = yMin; y <= yMax; y++)
					{
						for (int z = zMin; z <= zMax; z++)
						{
							dict[(x, y, z)] = shouldBeOn;
						}
					}
				}
			}

			return dict.Count(x => x.Value);
		}
	}
}
