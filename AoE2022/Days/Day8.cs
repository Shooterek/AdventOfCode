using AoE2022.Utils;

public class Day8 : StringListDay
{
    protected override object FirstTask()
    {
        var numbers = this.Input.Select(l => l.Select(n => (int)Char.GetNumericValue(n)).ToArray()).ToArray();
        var right = numbers.Select((array, index) => Count(array.WithIndex(), null, index)).SelectMany(l => l);
        var left = numbers.Select((array, index) => Count(array.WithIndex().Reverse(), null, index)).SelectMany(l => l);
        var down = UpToDown(numbers).Select((array, index) => Count(array.WithIndex(), index, null)).SelectMany(l => l);
        var up = UpToDown(numbers).Select((array, index) => Count(array.WithIndex().Reverse(), index, null)).SelectMany(l => l);
        var count = right
            .Concat(left)
            .Concat(up) 
            .Concat(down)
            .Distinct();

        var x = count.Where(p => numbers[p.X][p.Y] == 0).ToList();
        return count.Count();
    }

    protected override object SecondTask()
    {
        throw new NotImplementedException();
    }

    private List<Point> Count(IEnumerable<(int item, int index)> source, int? x, int? y) => 
        source.Aggregate((visible: true, list: new List<Point>(), max: -1), (state, next) =>
        {
            if (state.visible && next.item > state.max )
            {
                state.list.Add(new(x ?? next.index, y ?? next.index));
                state.max = next.item;
                return state;
            }
            else if (next.item == state.max) {
                return state;
            }

            state.visible = false;
            return state;
        }).list;

    private IEnumerable<IEnumerable<int>> UpToDown(int[][] source) {
        for (int i = 0; i < source[0].Length; i++)
        {
            var r = new List<int>();
            for (int j = 0; j < source.Length; j++)
            {
                r.Add(source[j][i]);
            }

            yield return r;
        }
    }

    private record Point(int X, int Y);
}

public static class IEnumerableExtensions
{
    public static IEnumerable<(T, int)> WithIndex<T> (this IEnumerable<T> source)
        => source.Select((item, index) => (item, index));
}
