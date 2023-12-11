using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day11 : StringListDay
{
    protected override object FirstTask()
    {
        var map = this.Input.Select(line => line.ToCharArray()).ToArray();

        var emptyXes = Enumerable.Range(0, this.Input[0].Length)
            .Select(index => {
                return this.Input.Select(line => line[index]).All(c => c == '.') ? index : (int?)null;
            }).OfType<int>().ToArray();
        var emptyYes = this.Input.Select((table, index) => (table, index)).Where(r => r.table.All(c => c == '.')).Select(r => r.index).ToArray();

        var galaxies = this.Input.SelectMany((line, rowIndex) => line.Select((c, xIndex) => c == '#' ? new Coords(xIndex, rowIndex) : null).OfType<Coords>()).ToArray();

        var distance = 0;
        var g = new HashSet<(Coords, Coords)>();
        foreach (var galaxy in galaxies) {
            foreach (var galaxy2 in galaxies) {
                if (!g.Add((galaxy, galaxy2)) || !g.Add((galaxy2, galaxy)))
                    continue;
                var xValues = Enumerable.Range(Math.Min(galaxy2.X, galaxy.X), Math.Abs(galaxy2.X - galaxy.X));
                var yValues = Enumerable.Range(Math.Min(galaxy2.Y, galaxy.Y), Math.Abs(galaxy2.Y - galaxy.Y));

                foreach (var x in xValues) {
                    distance += emptyXes.Contains(x) ? 2 : 1;
                }

                foreach (var y in yValues) {
                    distance += emptyYes.Contains(y) ? 2 : 1;
                }
            }
        }
        return distance;
    }

    protected override object SecondTask()
    {
        var map = this.Input.Select(line => line.ToCharArray()).ToArray();

        var emptyXes = Enumerable.Range(0, this.Input[0].Length)
            .Select(index => {
                return this.Input.Select(line => line[index]).All(c => c == '.') ? index : (int?)null;
            }).OfType<int>().ToArray();
        var emptyYes = this.Input.Select((table, index) => (table, index)).Where(r => r.table.All(c => c == '.')).Select(r => r.index).ToArray();

        var galaxies = this.Input.SelectMany((line, rowIndex) => line.Select((c, xIndex) => c == '#' ? new Coords(xIndex, rowIndex) : null).OfType<Coords>()).ToArray();

        var distance = 0L;
        var g = new HashSet<(Coords, Coords)>();
        foreach (var galaxy in galaxies) {
            foreach (var galaxy2 in galaxies) {
                if (!g.Add((galaxy, galaxy2)) || !g.Add((galaxy2, galaxy)))
                    continue;
                var xValues = Enumerable.Range(Math.Min(galaxy2.X, galaxy.X), Math.Abs(galaxy2.X - galaxy.X));
                var yValues = Enumerable.Range(Math.Min(galaxy2.Y, galaxy.Y), Math.Abs(galaxy2.Y - galaxy.Y));

                foreach (var x in xValues) {
                    distance += emptyXes.Contains(x) ? 1_000_000 : 1;
                }

                foreach (var y in yValues) {
                    distance += emptyYes.Contains(y) ? 1_000_000 : 1;
                }
            }
        }
        return distance;
    }

    public record Coords(int X, int Y);
}
