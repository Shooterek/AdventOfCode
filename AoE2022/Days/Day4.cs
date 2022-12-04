using AoE2022.Utils;

public class Day4 : StringListDay
{
    protected override object FirstTask()
    {
        return this.Input.Sum(l => {
            var elfs = l.Split(",");
            var elf1 = elfs[0].Split("-").Select(int.Parse).ToArray();
            var elf2 = elfs[1].Split("-").Select(int.Parse).ToArray();

            return (elf1, elf2) switch {
                ([int e11, int e12], [int e21, int e22]) when e22 >= e12 && e21 <= e11 => 1,
                ([int e11, int e12], [int e21, int e22]) when e12 >= e22 && e11 <= e21 => 1,
                (_, _) => 0,
            };
        });
    }

    protected override object SecondTask()
    {
        return this.Input.Sum(l => {
            var elfs = l.Split(",");
            var elf1 = elfs[0].Split("-").Select(int.Parse).ToArray();
            var elf2 = elfs[1].Split("-").Select(int.Parse).ToArray();

            return (elf1, elf2) switch {
                ([int e11, int e12], [int e21, int e22]) when e11 >= e21 && e11 <= e22 => 1,
                ([int e11, int e12], [int e21, int e22]) when e12 >= e21 && e12 <= e22 => 1,
                ([int e11, int e12], [int e21, int e22]) when e21 >= e11 && e21 <= e12 => 1,
                ([int e11, int e12], [int e21, int e22]) when e22 >= e11 && e22 <= e12 => 1,
                (_, _) => 0,
            };
        });
    }
}
