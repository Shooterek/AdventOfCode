using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day3 : StringListDay
{
    private (int number, int line, int characterIndex)[][] numbersPerLine;

    public override void LoadInput() {
        base.LoadInput();

        this.numbersPerLine = this.Input.Select((line, row) => {
            var numberRegex = new Regex(@"\d+");
            var matches = numberRegex.Matches(line);

            return matches.Select(match => (int.Parse(match.Value), row, match.Index)).ToArray();
        }).ToArray();
    }

    protected override object FirstTask()
    {
        var sum = 0;
        foreach (var line in this.numbersPerLine) {
            foreach (var number in line) {
                var adjacentSquares = GetAdjacentSquares(number.number.ToString().Length, number.characterIndex, number.line, this.numbersPerLine.Length, this.Input.First().Length);
                var squares = adjacentSquares.Select(sq => this.Input[sq.Item2][sq.Item1]);
                if (squares.Any(sq => !Char.IsLetterOrDigit(sq) && sq != '.')) {
                    sum += number.number;
                }
            }
        }

        return sum;
    }

    protected override object SecondTask()
    {
        return this.Input.Select((text, lineIndex) => {
            var regex = new Regex(@"\*");
            var matches = regex.Matches(text);
            var sum = 0;
            foreach (Match match in matches) {
                var adjacentSquares = GetAdjacentSquares(1, match.Index, lineIndex, this.Input.Count, this.Input.First().Length).ToArray();
                var lines = adjacentSquares.Select(xy => xy.y).ToArray();
                var x = adjacentSquares.Select(xy => xy.x).ToArray();
                var adjacentNumbers = this.numbersPerLine
                    .Where((n, y) => lines.Contains(y))
                    .SelectMany(n => n)
                    .Where(n => Enumerable.Range(n.characterIndex, n.number.ToString().Length).Intersect(x).Any())
                    .ToArray();
                if (adjacentNumbers.Length == 2)
                {
                    sum += adjacentNumbers[0].number * adjacentNumbers[1].number;
                }
            }
            return sum;
        })
        .Sum();
    }

    private (int x, int y)[] GetAdjacentSquares(int lengthOfNumber, int index, int line, int maxHeight, int maxLength) {
        return Enumerable
            .Range(index - 1, lengthOfNumber + 2)
            .SelectMany(x => Enumerable.Range(line - 1, 3).Select(y => (x, y)))
            .Where(xy => xy.x >=0 && xy.x < maxLength && xy.y >= 0 && xy.y < maxHeight).ToArray();
    }
}