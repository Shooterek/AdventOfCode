using System;

namespace AoE2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var day = new Day5("./Inputs/day5.txt");

            Console.WriteLine(day.FirstTask());
            Console.WriteLine(day.SecondTask());
        }
    }
}
