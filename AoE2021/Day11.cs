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
			var input = this._inputLoader.LoadStringListInput()
				.Select(x => x.ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray()).ToArray();
			var grid = new OctopusGrid(input);
			var flashedCount = 0;

			for (int i = 0; i < 100; i++)
			{
				grid.IncreaseAllByOne();

				while (grid.ShouldAnyOctopusFlash())
					flashedCount += grid.FlashAll();

				grid.ZeroAllFlashed();
			}

			return flashedCount;
        }

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringListInput()
				.Select(x => x.ToCharArray().Select(x => (int)char.GetNumericValue(x)).ToArray()).ToArray();
			var grid = new OctopusGrid(input);
			var iteration = 1;

			while (true)
			{
				grid.IncreaseAllByOne();

				while (grid.ShouldAnyOctopusFlash())
					grid.FlashAll();

				grid.ZeroAllFlashed();

				if (grid.DidAllFlashTogether())
					break;
				iteration++;
			}

			return iteration;
		}
    }        
}
