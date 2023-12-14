using AoE2023.Utils;
using BenchmarkDotNet.Disassemblers;
using MoreLinq;

namespace AoE2023;

public class Day14 : StringListDay
{
    protected override object FirstTask()
    {
        var map = this.Input.Select(line => line.ToCharArray()).ToArray();
        MoveNorth(map);
        return CalculateScore(map);
    }

    private int CalculateScore(char[][] map)
        => RotateJaggedArrayCounterClockwise(map).Select(line => line.Select((c, index) => c == 'O' ? line.Length - index : 0).Sum()).Sum();

    protected override object SecondTask()
    {
        var dictionary = new Dictionary<(string, int), (int, int)>();
        var totalCycles = 4 * 1000000000L;
        var index = 0;
        var direction = 0;

        var map = this.Input.Select(line => line.ToCharArray()).ToArray();
        while (true)
        {
            Action<char[][]> func = (direction % 4) switch
            {
                0 => MoveNorth,
                1 => MoveWest,
                2 => MoveSouth,
                3 => MoveEast,
            };

            func(map);
            var representation = GetString(map);
            if (dictionary.ContainsKey((representation, direction)))
            {
                Console.WriteLine(index);
                var cycleBeginning = dictionary[(representation, direction)].Item1;
                var cycleLength = index - cycleBeginning;
                var fullCycles = (totalCycles - cycleBeginning) / cycleLength;


                return dictionary.First(kv => kv.Value.Item1 == totalCycles - fullCycles * cycleLength - cycleBeginning).Value.Item2;
            }

            dictionary.Add((representation, direction), (index, CalculateScore(map)));
            direction = (direction + 1) % 4;
            index++;
        }
    }

    private string GetString(char[][] src)
        => string.Join("", src.Select(s => string.Join("", s)));

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

    private void MoveNorth(char[][] map)
    {
        var height = map.Length;
        var width = map[0].Length;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = j; k > 0; k--)
                {
                    if (map[k][i] == 'O' && map[k - 1][i] == '.')
                    {
                        map[k - 1][i] = 'O';
                        map[k][i] = '.';
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

    private void MoveSouth(char[][] map)
    {
        var height = map.Length;
        var width = map[0].Length;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < height - 1; k++)
                {
                    if (map[k][i] == 'O' && map[k + 1][i] == '.')
                    {
                        map[k + 1][i] = 'O';
                        map[k][i] = '.';
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

    static void PrintJaggedArray(char[][] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                Console.Write(array[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}
