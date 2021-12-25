using System.Collections.Generic;
using System.Linq;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day24 : Day
	{
		private List<long> addX;
		private List<long> divZ;
		private List<long> addY;
		private Dictionary<(int, long), List<string>> cache;
		private List<long> maxZAtStep;

		public Day24() : base("day24")
		{
		}

		protected override object FirstTask()
		{
			this.cache = new();
			this.maxZAtStep = new();
			this.addX = new();
			this.addY = new();
			this.divZ = new();
			var lines = this._inputLoader.LoadStringListInput();

			for (int i = 0; i < 14; i++)
			{
				this.divZ.Add(int.Parse(lines[(18 * i) + 4].Split()[2]));
				this.addX.Add(int.Parse(lines[(18 * i) + 5].Split()[2]));
				this.addY.Add(int.Parse(lines[(18 * i) + 15].Split()[2]));
			}
			for (int i = 0; i < this.divZ.Count; i++)
			{
				this.maxZAtStep.Add(this.divZ.Skip(i).Aggregate(1L, (a, b) => a * b));
			}

			var ids = RunGroupsRecursivly(0, 0);
			return ids.Max();
		}

		protected override object SecondTask()
		{
			this.cache = new();
			this.maxZAtStep = new();
			this.addX = new();
			this.addY = new();
			this.divZ = new();
			var lines = this._inputLoader.LoadStringListInput();

			for (int i = 0; i < 14; i++)
			{
				this.divZ.Add(int.Parse(lines[(18 * i) + 4].Split()[2]));
				this.addX.Add(int.Parse(lines[(18 * i) + 5].Split()[2]));
				this.addY.Add(int.Parse(lines[(18 * i) + 15].Split()[2]));
			}

			this.cache = new();
			this.maxZAtStep = new();
			for (int i = 0; i < this.divZ.Count; i++)
			{
				this.maxZAtStep.Add(this.divZ.Skip(i).Aggregate(1L, (a, b) => a * b));
			}

			var ids = RunGroupsRecursivly(0, 0);
			return ids.Min();
		}

		private List<string> RunGroupsRecursivly(int groupNum, long prevZ)
		{
			if (this.cache.ContainsKey((groupNum, prevZ)))
				return this.cache[(groupNum, prevZ)];

			if (groupNum >= 14)
			{
				if (prevZ == 0)
					return new List<string>() { "" };

				return Enumerable.Empty<string>().ToList();
			}
			if (prevZ > this.maxZAtStep[groupNum])
				return Enumerable.Empty<string>().ToList();

			long nextX = this.addX[groupNum] + prevZ % 26;
			long nextZ;
			List<string> newValues = new();
			if (nextX > 0 && nextX < 10)
			{
				nextZ = RunSingleGroup(groupNum, prevZ, nextX);
				newValues.AddRange(RunGroupsRecursivly(groupNum + 1, nextZ).Select(x => $"{nextX}{x}").ToList());
			}
			else
			{
				foreach (int i in Enumerable.Range(1, 9))
				{
					nextZ = RunSingleGroup(groupNum, prevZ, i);
					newValues.AddRange(RunGroupsRecursivly(groupNum + 1, nextZ).Select(x => $"{i}{x}").ToList());
				}
			}

			this.cache[(groupNum, prevZ)] = newValues;
			return newValues;
		}

		private long RunSingleGroup(int groupNum, long z, long input)
		{
			long x = this.addX[groupNum] + z % 26;
			z /= this.divZ[groupNum];
			if (x != input)
			{
				z *= 26;
				z += input + this.addY[groupNum];
			}

			return z;
		}
	}
}
