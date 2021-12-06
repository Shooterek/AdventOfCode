using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day6 : Day
    {
        private Dictionary<int, long> cache;
        public Day6() : base("day6")
        {
        }

        protected override object FirstTask()
        {
            this.cache = new();
            var input = this._inputLoader.LoadStringListInput().First().Split(',').Select(int.Parse).ToList();
            var maxDays = 80;

            long score = 0;
            foreach (var inputNumber in input)
            {
                score += GetDirectChildren(maxDays + 6 - inputNumber);
            }

            return score;
        }

        protected override object SecondTask()
        {
            this.cache = new();
            var input = this._inputLoader.LoadStringListInput().First().Split(',').Select(int.Parse).ToList();
            var maxDays = 256;

            long score = 0;
            foreach (var inputNumber in input)
            {
                score += GetDirectChildren(maxDays + 6 - inputNumber);
            }

            return score;
        }

        private long GetDirectChildren(int v)
        {
            var initVal = v;
            if (this.cache.TryGetValue(v, out var result))
                return result;
            long score = 1;

            while(v > 6)
            {
                v -= 7;
                score += GetDirectChildren(v - 2);
            }

            this.cache[initVal] = score;
            return score;
        }
    }
}
