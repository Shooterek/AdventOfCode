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
            var yLength = lines.Length;
            var xLength = lines[0].Length;
            var stack = new Stack<(int x, int y)>();
            var sizes = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] != 9)
                    {
                        sizes.Add(BackTrack(j, i));
                    }
                }
            }

            return sizes.OrderByDescending(x => x).Take(3).Aggregate(1, (aggr, val) => aggr * val);

            int BackTrack(int x, int y)
            {
                var size = 1;
                var result = new List<int>();
                stack.Push((x, y));
                while (stack.Count >= 0)
                {
                    result.Add(lines[y][x]);
                    lines[y][x] = 9;
                    if (y + 1 < yLength && lines[y + 1][x] != 9)
                    {
                        stack.Push((x, y));
                        y += 1;
                        size++;
                    }
                    else if (y - 1 >= 0 && lines[y - 1][x] != 9)
                    {
                        stack.Push((x, y));
                        y -= 1;
                        size++;
                    }
                    else if (x + 1 < xLength && lines[y][x + 1] != 9)
                    {
                        stack.Push((x, y));
                        x += 1;
                        size++;
                    }
                    else if (x - 1 >= 0 && lines[y][x - 1] != 9)
                    {
                        stack.Push((x, y));
                        x -= 1;
                        size++;
                    }
                    else
                    {
                        if (stack.Count > 0)
                            (x, y) = stack.Pop();
                        else return size;
                    }
                }

                return size;
            }
        }
    }        
}
