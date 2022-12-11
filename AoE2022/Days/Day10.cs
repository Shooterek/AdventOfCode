using AoE2022.Utils;
using MoreLinq;

public class Day10 : StringListDay
{
    protected override object FirstTask()
    {
        var x = 1;
        var cycle = 0;
        var score = 0;

        foreach (var instr in this.Input) {
            var c = instr.Split(" ").ToArray();

            if (c is ["noop"]) {
                Tick(0);
            }
            else if (c is ["addx", var val]) {
                Tick(0);
                Tick(int.Parse(val));
            }
        }

        return score;

        void Tick(int valToAdd) {
            cycle += 1;
            if ((cycle - 20) % 40 == 0) {
                score += cycle * x;
            }
            x += valToAdd;
        }
    }

    protected override object SecondTask()
    {
        var x = 1;
        var cycle = 0;
        var crt = new List<char[]>();
        Enumerable.Range(0, 6).ForEach(_ => crt.Add(new char[40]));

        foreach (var instr in this.Input) {
            var c = instr.Split(" ").ToArray();

            if (c is ["noop"]) {
                Tick(0);
            }
            else if (c is ["addx", var val]) {
                Tick(0);
                Tick(int.Parse(val));
            }
        }

        foreach (var line in crt)
        {
            foreach (var pixel in line)
            {
                Console.Write(pixel);
            }
            Console.WriteLine();
        }

        return string.Empty;

        void Tick(int valToAdd) {
            var pos = cycle;
            cycle += 1;
            if (cycle < 240) {
                if (x - 1 == pos % 40 || x == pos % 40 || x + 1 == pos % 40)
                    crt[cycle / 40][pos % 40] = '#';
                else
                    crt[cycle / 40][pos % 40] = ' ';
            }
            x += valToAdd;
        }
    }
}
