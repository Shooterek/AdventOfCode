namespace AoE2022;

internal class Program
{
    private static void Main(string[] args)
    {
        var currentDay = DateTime.Now.Day;
        var day = (Day)Activator.CreateInstance(null, $"Day{15}").Unwrap();

        day.RunFirstTask();

        day.RunSecondTask();
    }
}