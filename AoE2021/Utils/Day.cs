using System;
using System.Diagnostics;

namespace AoE2021.Utils
{
    public abstract class Day
    {
        protected Day(string inputPath)
        {
            _inputLoader = new InputLoader($"./Inputs/{inputPath}.txt");
        }

        protected readonly InputLoader _inputLoader;

        protected abstract object FirstTask();
        protected abstract object SecondTask();

        public void RunFirstTask()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = FirstTask();

            sw.Stop();
            Console.WriteLine($"The result of the first task is {result} and it took {sw.ElapsedMilliseconds} ms to complete it");
        }

        public void RunSecondTask()
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = SecondTask();

            sw.Stop();
            Console.WriteLine($"The result of the second task is {result} and it took {sw.ElapsedMilliseconds} ms to complete it");
        }
    }
}
