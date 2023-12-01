using AoE2023.Utils;
using System.Diagnostics;
namespace AoE2023;

public abstract class Day
{
    protected Day()
    {
        var type = this.GetType().Name;
        _inputLoader = new InputLoader($"./Inputs/{type!.ToLower()}.txt");
    }

    protected readonly InputLoader _inputLoader;

    protected abstract object FirstTask();

    protected abstract object SecondTask();

    public abstract void LoadInput();

    public void RunFirstTask()
    {
        LoadInput();
        var sw = new Stopwatch();
        sw.Start();

        var result = FirstTask();

        sw.Stop();
        Console.WriteLine($"The result of the first task is {result} and it took {sw.ElapsedMilliseconds} ms to complete it");
    }

    public void RunSecondTask()
    {
        LoadInput();
        var sw = new Stopwatch();
        sw.Start();

        var result = SecondTask();

        sw.Stop();
        Console.WriteLine($"The result of the second task is {result} and it took {sw.ElapsedMilliseconds} ms to complete it");
    }
}
