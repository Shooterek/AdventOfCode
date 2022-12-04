using AoE2022.Utils;
using System;
using System.Reflection;

namespace AoE2022;

internal class Program
{
    private static void Main(string[] args)
    {
        var currentDay = DateTime.Now.Day;
        var day = new Day4();
        day.LoadInput();

        day.RunFirstTask();

        day.LoadInput();
        day.RunSecondTask();
    }
}