using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoE2021
{
    class Day1 : Day
    {
        public Day1(string inputPath) : base(inputPath)
        {

        }
        public override string FirstTask()
        {
            var increasedDepth = 0;
            var input = _inputLoader.LoadIntListInput(_inputPath);

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] > input[i - 1])
                    increasedDepth++;
            }

            return increasedDepth.ToString();
        }

        public override string SecondTask()
        {
            var increasedDepth = 0;
            var input = _inputLoader.LoadIntListInput(_inputPath);

            for (int i = 3; i < input.Count; i++)
            {
                var previousWindow = input[i] + input[i - 1] + input[i - 2];
                var currentWindow = input[i - 1] + input[i - 2] + input[i - 3];

                if (previousWindow > currentWindow)
                    increasedDepth++;
            }

            return increasedDepth.ToString();
        }
    }
}
