using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoE2021
{
    public class Day9 : Day
    {
        public Day9() : base("day9")
        {
        }

        protected override object FirstTask()
        {
            var lines = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray()).ToArray();
            var sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var flag = true;
                    var currVal = lines[i][j];
                    if (i > 0 && lines[i - 1][j] <= currVal)
                        flag = false;
                    if (i < lines.Length - 1 && lines[i + 1][j] <= currVal)
                        flag = false;
                    if (j > 0 && lines[i][j - 1] <= currVal)
                        flag = false;
                    if (j < lines[i].Length - 1 && lines[i][j + 1] <= currVal)
                        flag = false;

                    if (flag)
                        sum += (currVal + 1);
                }
            }


            return sum;
        }

        protected override object SecondTask()
        {
            var lines = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray()).ToArray();
            var sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] != 9)
                        sum++;
                }
            }


            return sum;
        }
    }        
}
