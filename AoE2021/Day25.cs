using System;
using System.Collections.Generic;
using System.Linq;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day25 : Day
	{
		public Day25() : base("day25")
		{
		}

		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray()).ToArray();
			var ySize = input.Length;
			var xSize = input[0].Length;

			var counter = 0;
			var wasMove = true;
			while (wasMove)
			{
				wasMove = false;
				var temp = new char[ySize][];

				for (int i = 0; i < ySize; i++)
				{
					temp[i] = new char[xSize];
					for (int j = 0; j < xSize; j++)
					{
						temp[i][j] = input[i][j];
					}
				}

				for (int y = 0; y < ySize; y++)
				{
					for (int x = 0; x < xSize; x++)
					{
						var character = input[y][x];
						if (character == '>')
						{
							(var newXPos, var newYPos) = GetNewPos(x, y);
							if (input[newYPos][newXPos] != '>' && input[newYPos][newXPos] != 'v')
							{
								temp[y][x] = '.';
								temp[newYPos][newXPos] = input[y][x];
								wasMove = true;
							}
						}
					}
				}

				for (int y = 0; y < ySize; y++)
				{
					for (int x = 0; x < xSize; x++)
					{
						var character = input[y][x];
						if (character == 'v')
						{
							(var newXPos, var newYPos) = GetNewPos(x, y);
							if (temp[newYPos][newXPos] != '>' && temp[newYPos][newXPos] != 'v' && (input[newYPos][newXPos] == '.' || input[newYPos][newXPos] == '>'))
							{
								temp[y][x] = '.';
								temp[newYPos][newXPos] = input[y][x];
								wasMove = true;
							}
						}
					}
				}

				input = temp;

				counter++;
			}

			return counter;

			(int newX, int newY) GetNewPos(int x, int y)
			{
				if (input[y][x] == 'v')
					return (x, y + 1 >= ySize ? 0 : y + 1);

				else if (input[y][x] == '>')
					return (x + 1 >= xSize ? 0 : x + 1, y);
				else 
					return (x, y);
			}
		}

		protected override object SecondTask()
		{
			return "";
		}
	}
}
