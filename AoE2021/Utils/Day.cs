using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoE2021.Utils
{
    public abstract class Day
    {
        protected Day(string inputPath)
        {
            _inputPath = inputPath;
            _inputLoader = new InputLoader();
        }

        protected readonly InputLoader _inputLoader;
        protected readonly string _inputPath;

        public abstract string FirstTask();
        public abstract string SecondTask();
    }
}
