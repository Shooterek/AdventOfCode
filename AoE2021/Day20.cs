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

			var incrY = sizeY + 36;
			var incrX = sizeX + 36;

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

			for (int z = 0; z < 2; z++)
			{
				var tempArr = new char[incrY][];
				for (int i = 0; i < incrY; i++)
				{
					tempArr[i] = new char[incrX];
					for (int j = 0; j < incrX; j++)
					{
						tempArr[i][j] = '.';
					}
				}
				for (int a = 0; a < incrY - (z + 1) * 3; a++)
				{
					for (int b = 0; b < incrX - (z + 1) * 3; b++)
					{
						var num = map[a].Skip(b).Take(3).ToList();
						num.AddRange(map[a + 1].Skip(b).Take(3));
						num.AddRange(map[a + 2].Skip(b).Take(3));

						var integer = GetIntValue(num);
						tempArr[a][b] = instr[integer];
					}
				}

				for (int a = 0; a < incrY; a++)
				{
					for (int b = 0; b < incrX; b++)
					{
						map[a][b] = tempArr[a][b];
					}
				}
			}

			return map.Sum(x => x.Count(r => r == '#'));
		}

		private int GetIntValue(List<char> num)
		{
			num.Reverse();
			return (int)num.Select((x, index) => x == '.' ? 0 : Math.Pow(2, index)).Sum();
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var instr = Regex.Replace(input[0], @"\s+", "");

			var initMap = input[1].Split("\r").Select(x => Regex.Replace(x, @"\s+", "")).ToArray();

			var sizeY = initMap.Count();
			var sizeX = initMap[0].Length;

			var map = new char[sizeY, sizeX];
			for (int i = 0; i < sizeY; i++)
			{
				for (int j = 0; j < sizeX; j++)
				{
					map[i, j] = initMap[i][j];
				}
			}
			var oddChar = instr[0];
			var evenChar = instr[511];
			map = Resize(map, '.');
			for (int i = 0; i < map.GetLength(0); i++)
			{
				for (int j = 0; j < map.GetLength(1); j++)
				{
					Console.Write(map[i, j]);
				}
				Console.WriteLine();
			}
			return "";
			//for (int z = 0; z < iterations; z++)
			//{
			//	var tempArr = new char[incrY][];
			//	for (int i = 0; i < incrY; i++)
			//	{
			//		tempArr[i] = new char[incrX];
			//		for (int j = 0; j < incrX; j++)
			//		{
			//			tempArr[i][j] = '.';
			//		}
			//	}
			//	for (int a = 0; a < incrY - (z + 1) * 3; a++)
			//	{
			//		for (int b = 0; b < incrX - (z + 1) * 3; b++)
			//		{
			//			var num = map[a].Skip(b).Take(3).ToList();
			//			num.AddRange(map[a + 1].Skip(b).Take(3));
			//			num.AddRange(map[a + 2].Skip(b).Take(3));

			//			var integer = GetIntValue(num);
			//			tempArr[a][b] = instr[integer];
			//		}
			//	}

			//	for (int a = 0; a < incrY; a++)
			//	{
			//		for (int b = 0; b < incrX; b++)
			//		{
			//			map[a][b] = tempArr[a][b];
			//		}
			//	}
			//}

			//return map.Sum(x => x.Count(r => r == '#'));
		}

		private char[,] Resize(char[,] map, char defChar)
		{
			var newMap = new char[map.GetLength(0) + 4, map.GetLength(1) + 4];

			for (int i = 0; i < newMap.GetLength(0); i++)
			{
				for (int j = 0; j < newMap.GetLength(1); j++)
				{
					if (j >= 2 && j < map.GetLength(1) + 2 && i >= 2 && i < map.GetLength(0) + 2)
					{
						newMap[i, j] = map[i - 2, j - 2];
					}
					else
					{
						newMap[i, j] = defChar;
					}
				}
			}

			return newMap;
		}
	}
}
