using AoE2023.Utils;

namespace AoE2023;

public class Day14 : StringListDay
{
    protected override object FirstTask()
    {
        var map = this.Input.Select(line => line.ToCharArray()).ToArray();
        map = RotateJaggedArrayCounterClockwise(map);
        MoveWest(map);
        return map.Select(line => line.Select((c, index) => c == 'O' ? line.Length - index : 0).Sum()).Sum();
    }

    protected override object SecondTask()
    {
        return null;
    }

    private void MoveWest(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                for (int k = j; k > 0; k--)
                {
                    if (map[i][k] == 'O' && map[i][k - 1] == '.')
                    {
                        map[i][k - 1] = 'O';
                        map[i][k] = '.';
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

    private void MoveEast(char[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = map[i].Length - 1; j >= 0; j--)
            {
                for (int k = j; k < map[i].Length - 1; k++)
                {
                    if (map[i][k] == 'O' && map[i][k + 1] == '.')
                    {
                        map[i][k + 1] = 'O';
                        map[i][k] = '.';
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
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
}
