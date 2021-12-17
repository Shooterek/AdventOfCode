using System;
using System.Collections.Generic;
using System.Linq;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day17 : Day
    {
		public Day17() : base("day17")
        {
        }

        protected override object FirstTask()
		{
			var input = this._inputLoader.LoadEntireString().Replace("target area: ", "");
			var coords = input.Split(", ");
			var xCords = coords[0].Split("=")[1];
			var xMin = int.Parse(xCords.Split("..")[0]);
			var xMax = int.Parse(xCords.Split("..")[1]);

			var yCords = coords[1].Split("=")[1];
			var yMin = int.Parse(yCords.Split("..")[0]);
			var yMax = int.Parse(yCords.Split("..")[1]);

			var val = Math.Abs(yMin) > (Math.Abs(yMax)) ? yMin : yMax;

			if (val < 0)
			{
				val = Math.Abs(val);
				return ((decimal)(0 + val - 1) / 2) * val;
			}

			return ((decimal)(0 + val) / 2) * (val + 1);
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadEntireString().Replace("target area: ", "");
			var coords = input.Split(", ");
			var xCords = coords[0].Split("=")[1];
			var xMin = int.Parse(xCords.Split("..")[0]);
			var xMax = int.Parse(xCords.Split("..")[1]);

			var yCords = coords[1].Split("=")[1];
			var yMin = int.Parse(yCords.Split("..")[0]);
			var yMax = int.Parse(yCords.Split("..")[1]);

			var possibleX = FindPossibleX(xMin, xMax).Distinct().ToList();
			var possibleY = FindPossibleY(yMin, yMax).ToList();

			var results = new List<(int, int)>();

			foreach (var posY in possibleY)
			{
				foreach (var posX in possibleX)
				{
					if (posX.steps == posY.steps)
						results.Add((posY.speed, posX.speed));
				}
			}

			return results.Distinct().Count();
		}

		private List<(int steps, int speed)> FindPossibleY(int yMin, int yMax)
		{
			var results = new List<(int, int)>();
			var stepCount = 1;
			while (stepCount < 500)
			{
				var speed = -350;
				while (speed < 350)
				{
					var y = CalculateYPosition(speed, stepCount);
					if (y <= yMax && y >= yMin)
						results.Add((stepCount, speed));
					speed++;
				}
				stepCount++;
			}

			return results;
		}

		private int CalculateYPosition(int speed, int stepCount)
		{
			var pos = 0;
			for (int i = 0; i < stepCount; i++)
			{
				pos += speed;
				speed--;
			}

			return pos;
		}

		private List<(int steps, int speed)> FindPossibleX(int xMin, int xMax)
		{
			var results = new List<(int, int)>();
			var stepCount = 1;
			while (stepCount < 500)
			{
				var speed = -350;
				while (speed < 350)
				{
					var x = CalculateXPosition(speed, stepCount);
					if (x <= xMax && x >= xMin)
						results.Add((stepCount, speed));
					speed++;
				}
				stepCount++;
			}

			return results;
		}

		private int CalculateXPosition(int speed, int stepCount)
		{
			var pos = 0;
			for (int i = 0; i < stepCount; i++)
			{
				pos += speed;
				var speedChange = speed switch
				{
					< 0 => 1,
					0 => 0,
					> 0 => -1
				};
				speed += speedChange;
			}

			return pos;
		}
	}
}
