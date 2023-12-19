using BenchmarkDotNet.Running;

namespace AoE2023;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Contains("b")) {
            var summary = BenchmarkRunner.Run<DayBenchmarks>();
            return;
        }

        var currentDay = DateTime.Now.Day;
        var day = (Day)Activator.CreateInstance(null, $"AoE2023.Day{18}").Unwrap();

        day.RunFirstTask();

        day.RunSecondTask();
    }
}