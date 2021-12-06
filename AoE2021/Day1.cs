using AoE2021.Utils;

namespace AoE2021
{
    class Day1 : Day
    {
        public Day1() : base("day1")
        {

        }
        protected override object FirstTask()
        {
            var increasedDepth = 0;
            var input = _inputLoader.LoadIntListInput();

            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] > input[i - 1])
                    increasedDepth++;
            }

            return increasedDepth;
        }

        protected override object SecondTask()
        {
            var increasedDepth = 0;
            var input = _inputLoader.LoadIntListInput();

            for (int i = 3; i < input.Count; i++)
            {
                var previousWindow = input[i] + input[i - 1] + input[i - 2];
                var currentWindow = input[i - 1] + input[i - 2] + input[i - 3];

                if (previousWindow > currentWindow)
                    increasedDepth++;
            }

            return increasedDepth;
        }
    }
}
