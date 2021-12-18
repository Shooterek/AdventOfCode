using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day18 : Day
    {
		public Day18() : base("day18")
        {
        }

        protected override object FirstTask()
		{
			var lines = this._inputLoader.LoadStringListInput();

			string x = null;
			foreach (var line in lines)
			{
				if (x == null)
					x = line;
				else
					x = Add(x, line);

				while (true)
				{
					var newText = x;
					var indexOfNumberToSplit = int.MaxValue;
					string? numberToSplit = null;
					var indexOfPairToExplode = int.MaxValue;
					string? pairToExplode = null;
					var numbers = Regex.Matches(x, @"\d+");
					if (numbers.Any(q => q.Length > 1))
					{
						numberToSplit = numbers.First(q => q.Length > 1).Value;
						indexOfNumberToSplit = x.IndexOf(numbers.First(q => q.Length > 1).Value);
					}

					var matches = Regex.Matches(x, @"\[\d+,\d+\]");
					foreach (Match match in matches)
					{
						var depthOfMatch = GetDepthToIndex(x, match.Index);
						if (depthOfMatch >= 4)
						{
							indexOfPairToExplode = match.Index;
							pairToExplode = match.Value;
							break;
						}
					}

					if (pairToExplode != null)
					{
						newText = Explode(x, pairToExplode, indexOfPairToExplode);
					}
					else if (numberToSplit != null)
					{
						newText = Split(x, numberToSplit);
					}
					if (newText.Length == x.Length)
						break;

					x = newText;
				}
			}

			return CalculateMagnitude(x);
		}

		protected override object SecondTask()
		{
			var lines = this._inputLoader.LoadStringListInput();
			var dict = new Dictionary<string, string>();
			string x = null;
			foreach (var line in lines)
			{
				dict[line] = Reduce(line);
			}

			var maxMagn = long.MinValue;
			foreach (var line in lines)
			{
				foreach (var line2 in lines.Where(r => r != line))
				{
					var magn = CalculateMagnitude(Reduce(Add(dict[line], line2)));
					if (magn > maxMagn)
						maxMagn = magn;
				}
			}

			return maxMagn;
		}

		private string Reduce(string x)
		{
			while (true)
			{
				var newText = x;
				var indexOfNumberToSplit = int.MaxValue;
				string? numberToSplit = null;
				var indexOfPairToExplode = int.MaxValue;
				string? pairToExplode = null;
				var numbers = Regex.Matches(x, @"\d+");
				if (numbers.Any(q => q.Length > 1))
				{
					numberToSplit = numbers.First(q => q.Length > 1).Value;
					indexOfNumberToSplit = x.IndexOf(numbers.First(q => q.Length > 1).Value);
				}

				var matches = Regex.Matches(x, @"\[\d+,\d+\]");
				foreach (Match match in matches)
				{
					var depthOfMatch = GetDepthToIndex(x, match.Index);
					if (depthOfMatch >= 4)
					{
						indexOfPairToExplode = match.Index;
						pairToExplode = match.Value;
						break;
					}
				}

				if (pairToExplode != null)
				{
					newText = Explode(x, pairToExplode, indexOfPairToExplode);
				}
				else if (numberToSplit != null)
				{
					newText = Split(x, numberToSplit);
				}
				if (newText.Length == x.Length)
					break;

				x = newText;
			}

			return x;
		}

		private long CalculateMagnitude(string x)
		{
			while (x.Count(r => r == '[' || r == ']') > 0)
			{
				var matches = Regex.Matches(x, @"\[\d+,\d+\]");
				foreach (Match match in matches)
				{
					var left = int.Parse(match.Value[1..^1].Split(',')[0]);
					var right = int.Parse(match.Value[1..^1].Split(',')[1]);

					x = x.Replace(match.Value, (left * 3 + right * 2).ToString());
				}
			}

			return int.Parse(x);
		}

		private string Add(string x, string line)
			=> $"[{x},{line}]";

		private string Split(string x, string numberToSplit)
		{
			var number = int.Parse(numberToSplit);
			var regex = new Regex(Regex.Escape(numberToSplit));
			return regex.Replace(x, $"[{number / 2},{Math.Ceiling((decimal)number / 2)}]", 1);
		}

		private int GetDepthToIndex(string x, int index)
		{
			var sub = x[..index];
			var leftCount = sub.Count(a => a == '[');
			var rightCount = sub.Count(a => a == ']');

			return leftCount - rightCount;
		}

		private string Explode(string x, string pairToExplode, int indexOfPairToExplode)
		{
			var numbers = Regex.Matches(x, @"\d+");
			var leftNumberToIncrese = numbers.LastOrDefault(x => x.Index < indexOfPairToExplode);
			var wasLeftNumberLengthIncreased = false;

			if (leftNumberToIncrese != null)
			{
				var newNumber = int.Parse(leftNumberToIncrese.Value) + int.Parse(pairToExplode.Split(',')[0][1..]);
				x = $"{x[..leftNumberToIncrese.Index]}{newNumber}{x[(leftNumberToIncrese.Index + leftNumberToIncrese.Value.Length)..]}";
				wasLeftNumberLengthIncreased = newNumber.ToString().Length != leftNumberToIncrese.Value.Length;
			}

			numbers = Regex.Matches(x, @"\d+");
			var rightNumberToIncrese = numbers.FirstOrDefault(x => x.Index >= indexOfPairToExplode + pairToExplode.Length + (wasLeftNumberLengthIncreased ? 1 : 0));
			if (rightNumberToIncrese != null)
			{
				var newNumber = int.Parse(rightNumberToIncrese.Value) + int.Parse(pairToExplode.Split(',')[1][..^1]);
				x = $"{x[..rightNumberToIncrese.Index]}{newNumber}{x[(rightNumberToIncrese.Index + rightNumberToIncrese.Value.Length)..]}";
			}

			var regex = new Regex(Regex.Escape(pairToExplode));
			var newText = regex.Replace(x, "0", 1, indexOfPairToExplode);

			return newText;
		}
	}
}
