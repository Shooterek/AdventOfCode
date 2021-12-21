using System;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day21 : Day
	{
		public Day21() : base("day21")
		{
		}

		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringListInput();
			var p1StartPos = int.Parse(Regex.Matches(input[0], @"\d+")[1].Value);
			var p2StartPos = int.Parse(Regex.Matches(input[1], @"\d+")[1].Value);

			var p1Score = 0;
			var p2Score = 0;
			var dieCounter = 0;
			var currVal = 6;
			while (p1Score < 1000 && p2Score < 1000)
			{
				p1StartPos += currVal--;
				p1StartPos = p1StartPos > 10 ? p1StartPos % 10 : p1StartPos;
				p1Score += p1StartPos;
				dieCounter += 3;
				currVal = currVal < 0 ? 9 : currVal;

				if (p1Score >= 1000)
					break;

				p2StartPos += currVal--;
				p2StartPos = p2StartPos > 10 ? p2StartPos % 10 : p2StartPos;
				p2Score += p2StartPos;
				currVal = currVal < 0 ? 9 : currVal;
				dieCounter += 3;

				Console.WriteLine($"P1: {p1Score}, P2: {p2Score}");
			}

			return Math.Min(p1Score, p2Score) * dieCounter;
		}

		protected override object SecondTask()
		{
			throw new NotImplementedException();
		}
	}
}
