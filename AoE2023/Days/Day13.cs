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
        var rowSplitPoints = rowNumbers.Select(NewMethod).ToArray();
        var columnSplits = columnNumbers.Select(NewMethod).ToArray();

        return columnSplits.Sum() + rowSplitPoints.Sum(val => val * 100);
    }

    private static int NewMethod(long[] gr)
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
                return i + 1;
            }

            return 0;
        }

        return 0;
    }

    protected override object SecondTask()
    {
        return null;
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

    static void PrintJaggedArray(int[][] jaggedArray)
    {
        foreach (var row in jaggedArray)
        {
            foreach (var element in row)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }
    }
}
