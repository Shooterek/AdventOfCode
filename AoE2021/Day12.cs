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
			return correctPaths.Count;

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

			PossiblePaths(new List<string> { "start" }, caves["start"], true);
			return correctPaths.Count;

			void PossiblePaths(List<string> previousCaves, Cave currCave, bool canContainAnotherDuplicate)
			{
				if (previousCaves.Contains("end"))
				{
					correctPaths.Add(string.Join(",", previousCaves));
					return;
				}
				foreach (var cave in currCave.Neighbours.Select(x => (cave: x, isDuplicate: previousCaves.Contains(x.Id))).Where(x => x.cave.Id != "start" && (!x.cave.IsSmall || (canContainAnotherDuplicate || !x.isDuplicate))))
				{
					PossiblePaths(previousCaves.Append(cave.cave.Id).ToList(), cave.cave, !cave.cave.IsSmall ? canContainAnotherDuplicate : (!cave.isDuplicate && canContainAnotherDuplicate));
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
