using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoE2021
{
    public class Day3 : Day
    {
        public Day3(string inputPath) : base(inputPath)
        {

        }
        public override string FirstTask()
        {
            var input = _inputLoader.LoadStringListInput(this._inputPath);
            StringBuilder gamma = new(), epsilon = new();
            for (var i = 0; i < input[0].Length; i++)
            {
                int zeros = 0, ones = 0;
                foreach (var number in input)
                {
                    if (number[i] == '1')
                    {
                        ones++;
                    }
                    else
                    {
                        zeros++;
                    }
                }

                if (ones > zeros)
                {
                    gamma.Append("1");
                    epsilon.Append("0");
                }
                else
                {
                    gamma.Append("0");
                    epsilon.Append("1");
                }
            }

            return (Convert.ToInt32(gamma.ToString(), 2) * Convert.ToInt32(epsilon.ToString(), 2)).ToString();
        }

        public override string SecondTask()
        {
            List<string> oxygenRatingList = _inputLoader.LoadStringListInput(this._inputPath), co2List = _inputLoader.LoadStringListInput(this._inputPath);
         
            for (var i = 0; i < oxygenRatingList[0].Length && oxygenRatingList.Count > 1;  i++)
            {
                int zeros = 0, ones = 0;
                foreach (var number in oxygenRatingList)
                {
                    if (number[i] == '1')
                    {
                        ones++;
                    }
                    else
                    {
                        zeros++;
                    }
                }
                // TODO fix edge cases
                oxygenRatingList = oxygenRatingList.Where(x => x[i] == (zeros > ones ? '0' : '1')).ToList();
                i %= oxygenRatingList[0].Length;
            }
         
            for (var i = 0; i < co2List[0].Length && co2List.Count > 1; i++)
            {
                int zeros = 0, ones = 0;
                foreach (var number in co2List)
                {
                    if (number[i] == '1')
                    {
                        ones++;
                    }
                    else
                    {
                        zeros++;
                    }
                }

                co2List = co2List.Where(x => x[i] == (zeros > ones ? '1' : '0')).ToList();
                i %= co2List[0].Length;
            }

            return (Convert.ToInt32(co2List.First().ToString(), 2) * Convert.ToInt32(oxygenRatingList.First().ToString(), 2)).ToString();
        }
    }
}
