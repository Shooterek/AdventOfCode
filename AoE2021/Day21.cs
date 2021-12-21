using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day21 : Day
	{
		private Dictionary<int, long> dict;

		public Day21() : base("day21")
		{
			this.dict = new()
			{
				[3] = 1,
				[4] = 3,
				[5] = 6,
				[6] = 7,
				[7] = 6,
				[8] = 3,
				[9] = 1,
			};
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
			}

			return Math.Min(p1Score, p2Score) * dieCounter;
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringListInput();
			var p1StartPos = int.Parse(Regex.Matches(input[0], @"\d+")[1].Value);
			var p2StartPos = int.Parse(Regex.Matches(input[1], @"\d+")[1].Value);

			(var firstWins, var secondWins) = MakeTurn(p1StartPos, p2StartPos, 0, 0, 0, false);

			return $"{firstWins}, {secondWins}";
		}

		public (long firstWins, long secondWins) MakeTurn(int firstPos, int secondPos, int firstPlayerScore, int secondPlayerScore, int addedValue, bool isFirstPlayersTurn)
		{
			if (isFirstPlayersTurn)
			{
				firstPos += addedValue;
				firstPos = firstPos > 10 ? firstPos % 10 : firstPos;

				firstPlayerScore += firstPos;

				if (firstPlayerScore >= 21)
					return (1, 0);
			}
			else
			{
				secondPos += addedValue;
				secondPos = secondPos > 10 ? secondPos % 10 : secondPos;

				secondPlayerScore += secondPos;

				if (secondPlayerScore >= 21)
					return (0, 1);
			}

			long firstScore = 0;
			long secondScore = 0;

			for (int i = 3; i < 10; i++)
			{
				(var f1, var f2) = MakeTurn(firstPos, secondPos, firstPlayerScore, secondPlayerScore, i, !isFirstPlayersTurn);
				firstScore += this.dict[i] * f1;
				secondScore += this.dict[i] * f2;
			}

			return (firstScore, secondScore);
		}
	}
}
