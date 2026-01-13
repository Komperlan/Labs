namespace Itmo.CSharpMicroservices.Lab1.Task1;

public static class Zipper
{
    public static IEnumerable<IEnumerable<T?>> ZipAll<T>(IEnumerable<T> source, params IEnumerable<T>[] collections)
    {
        IEnumerable<List<T>> enumerables = collections.Select(e => e.ToList());

        var enumerableList = enumerables.ToList();
        var sourceList = source.ToList();

        var allCollections = new List<List<T>>(enumerableList.Count + 1);
        allCollections.Add(sourceList.Cast<T>().ToList());
        allCollections.AddRange(enumerableList.Select(l => l.Cast<T>().ToList()));

        if (allCollections.Count == 0)
        {
            yield break;
        }

        int minLength = allCollections.Min(e => e.Count);

        for (int i = 0; i < minLength; i++)
        {
            var resultRow = new List<T>();
            foreach (List<T> list in allCollections)
            {
                resultRow.Add(list[i]);
            }

            yield return resultRow;
        }
    }
}