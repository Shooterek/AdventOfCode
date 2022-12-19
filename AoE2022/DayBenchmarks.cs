using AoE2022;
using BenchmarkDotNet.Attributes;

[InProcess]
[MemoryDiagnoser]
public class DayBenchmarks
{
    private Day benchmarkedDay;

    [GlobalSetup]
    public void Setup()
    {
        this.benchmarkedDay = new Day16();
        this.benchmarkedDay.LoadInput();
    }

    [Benchmark]
    public void Part1() => this.benchmarkedDay.RunFirstTask();

    [Benchmark]
    public void Part2() => this.benchmarkedDay.RunSecondTask();
}