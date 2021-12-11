using System.Linq;

namespace AoE2021.Utils
{
	public class OctopusGrid
	{
		public OctopusGrid(int[][] grid)
		{
			this.Grid = grid.Select(x => x.Select(y => (val: y, flashed: false)).ToArray()).ToArray();
		}

		public (int val, bool flashed)[][] Grid { get; }

		public void IncreaseAllByOne()
		{
			for (int i = 0; i < this.Grid.Length; i++)
			{
				for (int j = 0; j < this.Grid[i].Length; j++)
				{
					this.Grid[i][j].val += 1;
				}
			}
		}

		public bool ShouldAnyOctopusFlash()
		{
			return this.Grid.Any(x => x.Any(x => x.val > 9 && !x.flashed));
		}

		public int FlashAll()
		{
			int count = 0;
			for (int i = 0; i < this.Grid.Length; i++)
			{
				for (int j = 0; j < this.Grid[i].Length; j++)
				{
					if (this.Grid[i][j].val > 9 && !this.Grid[i][j].flashed)
					{
						this.Grid[i][j].flashed = true;
						IncreaseAllNeighbours(i, j);
						count++;
					}
				}
			}

			return count;
		}

		public void IncreaseAllNeighbours(int y, int x)
		{
			var gridLength = this.Grid.Length;
			this.Grid[y][x].val += 1;
			if (y < gridLength - 1)
			{
				this.Grid[y + 1][x].val += 1;
				if (x < this.Grid[y].Length - 1)
					this.Grid[y + 1][x + 1].val += 1;
				if (x > 0)
					this.Grid[y + 1][x - 1].val += 1;
			}
			if (y > 0)
			{
				this.Grid[y - 1][x].val += 1;
				if (x < this.Grid[y].Length - 1)
					this.Grid[y - 1][x + 1].val += 1;
				if (x > 0)
					this.Grid[y - 1][x - 1].val += 1;
			}
			if (x > 0)
			{
				this.Grid[y][x - 1].val += 1;
			}
			if (x < gridLength - 1)
			{
				this.Grid[y][x + 1].val += 1;
			}
		}

		public void ZeroAllFlashed()
		{
			for (int i = 0; i < this.Grid.Length; i++)
			{
				for (int j = 0; j < this.Grid[i].Length; j++)
				{
					if (this.Grid[i][j].flashed)
					{
						this.Grid[i][j].val = 0;
						this.Grid[i][j].flashed = false;
					}
				}
			}
		}

		public bool DidAllFlashTogether()
			=> this.Grid.All(x => x.All(x => x.val == 0));
	}
}
