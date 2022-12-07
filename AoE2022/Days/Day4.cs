using AoE2022.Utils;

public class Day4 : StringListDay
{
    protected override object FirstTask()
    {
        return this.Input.Count(l =>
        {
            var elfs = l.Split(",").Select(r => new ElfRange(r)).ToArray();

            return elfs[0].Contains(elfs[1]) || elfs[1].Contains(elfs[0]);
        });
    }

    protected override object SecondTask()
    {
        return this.Input.Count(l =>
        {
            var elfs = l.Split(",").Select(r => new ElfRange(r)).ToArray();

            return elfs[0].Overlap(elfs[1]);
        });
    }

    private record ElfRange
    {
        public ElfRange(string range)
        {
            var numbers = range.Split("-").Select(int.Parse).ToArray();
            this.Start = numbers[0];
            this.End = numbers[1];
        }
        public int Start { get; set; }
        public int End { get; set; }

        public bool Contains(ElfRange elf2) => this.Start <= elf2.Start && this.End >= elf2.End;
        public bool Overlap(ElfRange elf2) => (this.Start <= elf2.End && this.End >= elf2.Start) || (elf2.Start <= this.End && elf2.End >= this.Start);
    }
}
