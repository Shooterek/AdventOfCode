using AoE2023.Utils;

namespace AoE2023;

public class Day13 : StringBatchesDay
{
    protected override object FirstTask()
    {
        var grids = this.Input
            .Select(batch =>
                batch.Split("\r", StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Replace(" ", "").Replace(".", "0").Replace("#", "1").ToCharArray())
                    .ToArray())
            .ToArray();
        var rowNumbers = grids.Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();

        var columnNumbers = grids.Select(gr => RotateJaggedArrayCounterClockwise(gr))
            .Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();
        var rowSplitPoints = rowNumbers.Select(FindReflectionPoint).ToArray();
        var columnSplits = columnNumbers.Select(FindReflectionPoint).ToArray();

        return columnSplits.Select(r => r.FirstOrDefault()).Sum() + rowSplitPoints.Select(r => r.FirstOrDefault()).Sum(val => val * 100);
    }

    protected override object SecondTask()
    {
        var grids = this.Input
            .Select(batch =>
                batch.Split("\r", StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Replace(" ", "").Replace(".", "0").Replace("#", "1").ToCharArray())
                    .ToArray())
            .ToArray();
        var rowNumbers = grids.Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();

        var columnNumbers = grids.Select(gr => RotateJaggedArrayCounterClockwise(gr))
            .Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();
        var splitPoints = rowNumbers.Select(c => FindReflectionPoint(c).FirstOrDefault())
            .Zip(columnNumbers.Select(c => FindReflectionPoint(c).FirstOrDefault()), grids).ToArray();
        var sp = splitPoints.Select(s =>
        {
            var src = s.Third;
            var resultsX = new List<int>();
            var resultsY = new List<int>();
            for (int i = 0; i < src.Length; i++)
            {
                for (int z = 0; z < src[i].Length; z++)
                {
                    var copy = CopyJaggedArray(src);
                    copy[i][z] = copy[i][z] == '0' ? '1' : '0';

                    var rowNumbers2 = copy.Select(src => long.Parse(string.Join("", src))).ToArray();

                    var columnNumbers2 = RotateJaggedArrayCounterClockwise(copy).Select(src => long.Parse(string.Join("", src))).ToArray();
                    var rowSplitPoints2 = FindReflectionPoint(rowNumbers2).Where(r => r != 0 && r != s.First).ToArray();
                    var columnSplits2 = FindReflectionPoint(columnNumbers2).Where(r => r != 0 && r != s.Second).ToArray();

                    if (rowSplitPoints2.FirstOrDefault() is { } f && f > 0)
                    {
                        resultsX.Add(f);
                    }

                    if (columnSplits2.FirstOrDefault() is { } c && c > 0)
                    {
                        resultsY.Add(c);
                    }
                }
            }

            return resultsX.FirstOrDefault(0) * 100 + resultsY.FirstOrDefault(0);
        }).ToArray();

        return sp.Sum();
    }

    public object SecondTask2()
    {
        var grids = this.Input
            .Select(batch =>
                batch.Split("\r", StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Replace(" ", "").Replace(".", "0").Replace("#", "1").ToCharArray())
                    .ToArray())
            .ToArray();
        var g = grids.SelectMany(gr =>
        {
            var results = new List<char[][]>();
            for (int i = 0; i < gr.Length; i++)
            {
                for (int z = 0; z < gr[i].Length; z++)
                {
                    var copy = CopyJaggedArray(gr);
                    copy[i][z] = copy[i][z] == '0' ? '1' : '0';
                    results.Add(copy);
                }
            }
            return results;
        }).ToArray();

        var rowNumbers = g.Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();

        var columnNumbers = g.Select(gr => RotateJaggedArrayCounterClockwise(gr))
            .Select(row => row.Select(src => long.Parse(string.Join("", src))).ToArray()).ToArray();
        var rowSplitPoints = rowNumbers.Select(FindReflectionPoint).ToArray();
        var columnSplits = columnNumbers.Select(FindReflectionPoint).ToArray();

        return columnSplits.Sum(s => s.FirstOrDefault()) + rowSplitPoints.Sum(val => val.FirstOrDefault() * 100);
    }

    static T[][] RotateJaggedArrayCounterClockwise<T>(T[][] jaggedArray)
    {
        // Get the number of rows and columns
        int rows = jaggedArray.Length;
        int maxColumns = 0;

        foreach (var row in jaggedArray)
        {
            if (row.Length > maxColumns)
            {
                maxColumns = row.Length;
            }
        }

        // Transpose the array
        T[][] transposedArray = new T[maxColumns][];

        for (int i = 0; i < maxColumns; i++)
        {
            transposedArray[i] = new T[rows];
            for (int j = 0; j < rows; j++)
            {
                transposedArray[i][j] = jaggedArray[j][i];
            }
        }

        // Update the original jagged array with the rotated values
        return transposedArray;
    }

    private static IEnumerable<int> FindReflectionPoint(long[] gr)
    {
        for (int i = 0; i < gr.Length - 1; i++)
        {
            var current = gr[i];
            var next = gr[i + 1];
            if (current != next)
                continue;

            var lower = i;
            var upper = i + 1;
            while (lower >= 0 && upper <= gr.Length - 1)
            {
                current = gr[lower];
                next = gr[upper];

                if (current != next)
                    break;

                lower--;
                upper++;
            }

            if (current == next)
            {
                yield return i + 1;
            }
        }
    }

    static T[][] CopyJaggedArray<T>(T[][] originalArray)
    {
        int rows = originalArray.Length;
        T[][] newArray = new T[rows][];

        for (int i = 0; i < rows; i++)
        {
            int columns = originalArray[i].Length;
            newArray[i] = new T[columns];

            // Copy values from the original array to the new array
            Array.Copy(originalArray[i], newArray[i], columns);
        }

        return newArray;
    }
}
