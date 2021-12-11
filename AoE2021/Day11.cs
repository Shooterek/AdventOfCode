using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day11 : Day
    {
        public Day11() : base("day11")
        {
        }

        protected override object FirstTask()
		{
			var grid = this._inputLoader.LoadStringListInput()
				.Select(x => x.ToCharArray().Select(x => (val: (int)char.GetNumericValue(x), flashed: false)).ToArray()).ToArray();

			var flashedCount = 0;

			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine(i);
				IncreaseAllByOne();

				while (ShouldAnyOctopusFlash())
				{
					FlashAll();
				}

				ZeroAllFlashed();
				PrintGrid();
			}

			return flashedCount;

			void IncreaseAllByOne()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						grid[i][j].val += 1;
					}
				}
			}

			bool ShouldAnyOctopusFlash()
			{
				return grid.Any(x => x.Any(x => x.val > 9 && !x.flashed));
			}

			void FlashAll()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						if (grid[i][j].val > 9 && !grid[i][j].flashed)
						{
							grid[i][j].flashed = true;
							IncreaseAllNeighbours(i, j);
						}
					}
				}
			}

			void IncreaseAllNeighbours(int y, int x)
			{
				var gridLength = grid.Length;
				grid[y][x].val += 1;
				if (y < gridLength - 1)
				{
					grid[y + 1][x].val += 1;
					if (x < grid[y].Length - 1)
						grid[y + 1][x + 1].val += 1;
					if (x > 0)
						grid[y + 1][x - 1].val += 1;
				}
				if (y > 0)
				{
					grid[y - 1][x].val += 1;
					if (x < grid[y].Length - 1)
						grid[y - 1][x + 1].val += 1;
					if (x > 0)
						grid[y - 1][x - 1].val += 1;
				}
				if (x > 0)
				{
					grid[y][x - 1].val += 1;
				}
				if (x < gridLength - 1)
				{
					grid[y][x + 1].val += 1;
				}
			}

			void ZeroAllFlashed()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						if (grid[i][j].flashed)
						{
							grid[i][j].val = 0;
							grid[i][j].flashed = false;
							flashedCount++;
						}
					}
				}
			}

			void PrintGrid()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						Console.Write(grid[i][j].val + ", ");
					}
					Console.WriteLine();
				}
			}
        }

		protected override object SecondTask()
		{
			var grid = this._inputLoader.LoadStringListInput()
				.Select(x => x.ToCharArray().Select(x => (val: (int)char.GetNumericValue(x), flashed: false)).ToArray()).ToArray();
			var iteration = 1;

			while (true)
			{
				IncreaseAllByOne();
				Console.WriteLine(iteration);

				while (ShouldAnyOctopusFlash())
				{
					FlashAll();
				}

				ZeroAllFlashed();

				if (grid.All(x => x.All(x => x.val == 0)))
					break;
				iteration++;
			}

			return iteration;

			void IncreaseAllByOne()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						grid[i][j].val += 1;
					}
				}
			}

			bool ShouldAnyOctopusFlash()
			{
				return grid.Any(x => x.Any(x => x.val > 9 && !x.flashed));
			}

			void FlashAll()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						if (grid[i][j].val > 9 && !grid[i][j].flashed)
						{
							grid[i][j].flashed = true;
							IncreaseAllNeighbours(i, j);
						}
					}
				}
			}

			void IncreaseAllNeighbours(int y, int x)
			{
				var gridLength = grid.Length;
				grid[y][x].val += 1;
				if (y < gridLength - 1)
				{
					grid[y + 1][x].val += 1;
					if (x < grid[y].Length - 1)
						grid[y + 1][x + 1].val += 1;
					if (x > 0)
						grid[y + 1][x - 1].val += 1;
				}
				if (y > 0)
				{
					grid[y - 1][x].val += 1;
					if (x < grid[y].Length - 1)
						grid[y - 1][x + 1].val += 1;
					if (x > 0)
						grid[y - 1][x - 1].val += 1;
				}
				if (x > 0)
				{
					grid[y][x - 1].val += 1;
				}
				if (x < gridLength - 1)
				{
					grid[y][x + 1].val += 1;
				}
			}

			void ZeroAllFlashed()
			{
				for (int i = 0; i < grid.Length; i++)
				{
					for (int j = 0; j < grid[i].Length; j++)
					{
						if (grid[i][j].flashed)
						{
							grid[i][j].val = 0;
							grid[i][j].flashed = false;
						}
					}
				}
			}
		}
    }        
}
