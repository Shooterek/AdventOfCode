using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day12 : Day
    {
        public Day12() : base("day12")
        {
        }

        protected override object FirstTask()
		{
			var caveInput = this._inputLoader.LoadStringListInput();
			var caves = new Dictionary<string, Cave>();
			var correctPaths = new List<string>();
			foreach (var path in caveInput.Select(x => x.Split('-')))
			{
				if (!caves.TryGetValue(path[0], out var firstCave))
				{
					firstCave = new Cave(path[0]);
					caves[path[0]] = firstCave;
				}
				if (!caves.TryGetValue(path[1], out var secondCave))
				{
					secondCave = new Cave(path[1]);
					caves[path[1]] = secondCave;
				}
				firstCave.AddNeighbour(secondCave);
				secondCave.AddNeighbour(firstCave);
			}

			PossiblePaths("start", caves["start"]);
			return correctPaths.Count();

			void PossiblePaths(string previousCaves, Cave currCave)
			{
				if (previousCaves.Contains("end"))
				{
					correctPaths.Add(previousCaves);
					return;
				}
				foreach (var cave in currCave.Neighbours.Where(x => !x.IsSmall || !previousCaves.Contains(x.Id)))
				{
					PossiblePaths($"{previousCaves}-{cave.Id}", cave);
				}
			}
		}

		protected override object SecondTask()
		{
			var caveInput = this._inputLoader.LoadStringListInput();
			var caves = new Dictionary<string, Cave>();
			var counter = 0;
			var correctPaths = new List<string>();
			foreach (var path in caveInput.Select(x => x.Split('-')))
			{
				if (!caves.TryGetValue(path[0], out var firstCave))
				{
					firstCave = new Cave(path[0]);
					caves[path[0]] = firstCave;
				}
				if (!caves.TryGetValue(path[1], out var secondCave))
				{
					secondCave = new Cave(path[1]);
					caves[path[1]] = secondCave;
				}
				firstCave.AddNeighbour(secondCave);
				secondCave.AddNeighbour(firstCave);
			}

			PossiblePaths(new List<string> { "start" }, caves["start"]);
			return counter;

			void PossiblePaths(List<string> previousCaves, Cave currCave)
			{
				if (previousCaves.Contains("end"))
				{
					counter++;
					correctPaths.Add(string.Join(",", previousCaves));
					return;
				}
				foreach (var cave in currCave.Neighbours.Where(x => x.Id != "start" && (!x.IsSmall || (previousCaves.Count(r => r == x.Id) < 2 && previousCaves.Where(r => r.ToUpper() != r).GroupBy(r => r).Select(r => r.Count()).Count(r => r > 1) < 2))))
				{
					PossiblePaths(previousCaves.Append(cave.Id).ToList(), cave);
				}
			}
		}
    }

	public class Cave
	{
		public Cave(string Id)
		{
			this.Id = Id;
			this.IsSmall = this.Id != this.Id.ToUpper();
		}

		public string Id { get; }

		public bool IsSmall { get; }

		public List<Cave> Neighbours { get; } = new List<Cave>();

		public void AddNeighbour(Cave cave) => this.Neighbours.Add(cave);
	}
}
