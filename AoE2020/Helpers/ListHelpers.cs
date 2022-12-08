using System.Collections.Generic;
using System.Linq;

public static class ListHelpers{
    public static bool ContainsAll<T>(this IEnumerable<T> containingList, IEnumerable<T> lookupList) {
        return !lookupList.Except(containingList).Any();
    }
}