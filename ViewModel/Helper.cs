using System.Collections.ObjectModel;

namespace ClickWars2.ViewModel;

internal static class Helper
{
    public static void InsertSorted<T>(this ObservableCollection<T> collection, T item, Comparison<T> comparison)
    {
        if (collection.Count == 0)
            collection.Add(item);
        else
        {
            var last = true;
            for (var i = 0; i < collection.Count; i++)
            {
                var result = comparison.Invoke(collection[i], item);

                if (result < 1) continue;

                collection.Insert(i, item);
                last = false;
                break;
            }
            if (last)
                collection.Add(item);
        }
    }
}