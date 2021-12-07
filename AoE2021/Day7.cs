using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day7 : Day
    {
        public Day7() : base("day7")
        {
        }

        protected override object FirstTask()
        {
            var numbers = this._inputLoader.LoadIntListFromOneLine();
            var minPath = long.MaxValue;
            var minNumber = long.MinValue;

            for (int i = 0; i < numbers.Max(); i++)
            {
                long pathLength = numbers.Sum(x => Math.Abs(x - i));
                if (pathLength < minPath)
                {
                    minPath = pathLength;
                    minNumber = i;
                }
            }

            return minPath;
        }

        protected override object SecondTask()
        {
            var numbers = this._inputLoader.LoadIntListFromOneLine();
            var minPath = long.MaxValue;
            var minNumber = long.MinValue;

            for (int i = 0; i < numbers.Max(); i++)
            {
                long pathLength = numbers.Sum(x =>
                {
                    return Enumerable.Range(1, Math.Abs(x - i)).Sum();
                });
                if (pathLength < minPath)
                {
                    minPath = pathLength;
                    minNumber = i;
                }
            }

            return minPath;
        }
    }
}
