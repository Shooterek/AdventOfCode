using AoE2022.Utils;

public class Day20 : IntListDay
{
    protected override object FirstTask()
    {
        var numbers = this.Input.Select((item, index) => (item, index)).ToList();

        var count = numbers.Count;
        for (int i = 0; i < numbers.Count; i++)
        {
            var item = numbers.First(n => n.index == i);
            if (item.item == 0)
                continue;
            var currIndex = numbers.IndexOf(item);
            var targetIndex = (currIndex + item.item);
            while (targetIndex <= 0)
            {
                targetIndex = targetIndex + count - 1;
            }
            while (targetIndex >= count)
            {
                targetIndex = targetIndex - count + 1;
            }
            numbers.Remove(item);
            numbers.Insert(targetIndex, item);
        }

        var index = numbers.FindIndex(0, count, c => c.item == 0);
        return numbers[(index + 1000) % count].item + numbers[(index + 2000) % count].item + numbers[(index + 3000) % count].item;
    }

    protected override object SecondTask()
    {
        var decryptionKey = 811589153l;
        var numbers = this.Input.Select((item, index) => (item: (item * decryptionKey) % (this.Input.Count - 1), index, value: item * decryptionKey)).ToList();
        var count = numbers.Count;

        for (int j = 0; j < 10; j++)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                var item = numbers.First(n => n.index == i);
                if (item.item == 0)
                    continue;
                var currIndex = numbers.IndexOf(item);
                var targetIndex = (currIndex + item.item);
                while (targetIndex <= 0)
                {
                    targetIndex = targetIndex + count - 1;
                }
                while (targetIndex >= count)
                {
                    targetIndex = targetIndex - count + 1;
                }
                numbers.Remove(item);
                numbers.Insert((int)targetIndex, item);
            }
        }

        var index = numbers.FindIndex(0, count, c => c.item == 0);
        return numbers[(index + 1000) % count].value + numbers[(index + 2000) % count].value+ numbers[(index + 3000) % count].value;
    }
}
