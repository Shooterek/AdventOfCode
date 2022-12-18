using System.Text.RegularExpressions;
using AoE2022.Utils;

public class Day18 : StringListDay
{
    private static Regex NumberPattern = new Regex(@"\d+");
    private static List<(int, int, int)> Dirs = new() {
            (-1, 0, 0),
            (1, 0, 0),
            (0, -1, 0),
            (0, 1, 0),
            (0, 0, -1),
            (0, 0, 1),
        };

    protected override object FirstTask()
    {
        var cubes = this.Input.Select(l => new Cube(NumberPattern.Matches(l).Select(m => int.Parse(m.Captures.First().Value)).ToList())).ToList();
        return GetVisibleSides(cubes);
    }

    protected override object SecondTask()
    {
        var cubes = this.Input.Select(l => new Cube(NumberPattern.Matches(l).Select(m => int.Parse(m.Captures.First().Value)).ToList())).ToList();
        var maxX = cubes.MaxBy(c => c.X).X + 1;
        var maxY = cubes.MaxBy(c => c.Y).Y + 1;
        var maxZ = cubes.MaxBy(c => c.Z).Z + 1;

        var adjacentCubes = new List<Cube>() {
            new(new List<int> { 0, 0, 0 })
        };
        var markedCubesMap = new HashSet<Cube>(cubes);
        while (adjacentCubes.Count > 0)
        {
            foreach (var cube in adjacentCubes)
            {
                markedCubesMap.Add(cube);
            }

            adjacentCubes = adjacentCubes.SelectMany(cube => GetAdjacentCubes(markedCubesMap, cube, maxX, maxY, maxZ)).Distinct().ToList();
        }

        foreach (var x in Enumerable.Range(0, maxX))
        {
            foreach (var y in Enumerable.Range(0, maxY))
            {
                foreach (var z in Enumerable.Range(0, maxZ))
                {
                    var cube = new Cube(new List<int> { x, y, z });
                    if (!markedCubesMap.Contains(cube))
                    {
                        cubes.Add(cube);
                    }
                }
            }
        }

        return GetVisibleSides(cubes);
    }

    private record Cube
    {
        public Cube(List<int> vals)
        {
            this.X = vals[0];
            this.Y = vals[1];
            this.Z = vals[2];
        }

        public Cube(int x, int y, int z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }
    }

    private record Side(int Dim1, int Dim2);

    private IEnumerable<Cube> GetAdjacentCubes(HashSet<Cube> map, Cube cube, int maxX, int maxY, int maxZ)
        => Dirs.Select(dir => cube with { X = cube.X + dir.Item1, Y = cube.Y + dir.Item2, Z = cube.Z + dir.Item3 })
            .Where(c => !map.Contains(c))
            .Where(c => c.X < maxX && c.Y < maxY && c.Z < maxZ && c.X >= 0 && c.Y >= 0 && c.Z >= 0);

    private int GetVisibleSides(List<Cube> cubes)
    {
        var d = new List<(Func<Cube, int>, Func<Cube, int>, Func<Cube, int>)>() {
            (new(c => c.X), new(c => c.Y), new(c => c.Z)),
            (new(c => c.Y), new(c => c.Z), new(c => c.X)),
            (new(c => c.Z), new(c => c.X), new(c => c.Y)),
        };

        var score = 0;
        foreach (var act in d)
        {
            var perspective = cubes.OrderBy(act.Item3).GroupBy(v => new Side(act.Item1(v), act.Item2(v)), act.Item3).ToList();
            foreach (var side in perspective)
            {
                var items = side.ToList();
                var separateObjects = 0;
                for (int i = 0; i < items.Count;)
                {
                    if (i == items.Count - 1)
                    {
                        separateObjects++;
                        break;
                    }
                    var item = items[i];
                    while (i < items.Count - 1)
                    {
                        var next = items[++i];
                        if (next - item == 1)
                        {
                            item = next;
                            continue;
                        }
                        else
                        {
                            separateObjects++;
                            break;
                        }
                    }
                }

                score += separateObjects * 2;
            }
        }

        return score;
    }
}
