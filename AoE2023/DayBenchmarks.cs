using AoE2023;
using BenchmarkDotNet.Attributes;

[InProcess]
[MemoryDiagnoser]
public class DayBenchmarks
{
    private Day benchmarkedDay;

    [GlobalSetup]
    public void Setup()
    {
        var currentDay = DateTime.Now.Day;
        this.benchmarkedDay = (Day)Activator.CreateInstance(null, $"AoE2023.Day{Math.Min(25, 12)}").Unwrap();
        this.benchmarkedDay.LoadInput();
    }

    [Benchmark]
    public void Part1() => this.benchmarkedDay.RunFirstTask();

    [Benchmark]
    public void Part2() => this.benchmarkedDay.RunSecondTask();
}