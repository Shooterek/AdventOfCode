using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day20 : Day
	{
		public Day20() : base("day20")
		{
		}

		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var instr = Regex.Replace(input[0], @"\s+", "");

			var initMap = input[1].Split("\r").Select(x => Regex.Replace(x, @"\s+", "")).ToArray();

			var sizeY = initMap.Count();
			var sizeX = initMap[0].Length;

			var incrY = sizeY + 25;
			var incrX = sizeX + 25;

			var map = new char[incrY][];
			for (int i = 0; i < incrY; i++)
			{

				map[i] = new char[incrX];
				for (int j = 0; j < incrX; j++)
				{
					map[i][j] = '.';
				}
			}

			for (int i = 10; i < sizeY + 10; i++)
			{
				for (int j = 10; j < sizeX + 10; j++)
				{
					map[i][j] = initMap[i - 10][j - 10];
				}
			}

			return "";
		}

		protected override object SecondTask()
		{
			return "";
		}
	}
}
