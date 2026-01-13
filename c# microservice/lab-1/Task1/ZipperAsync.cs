namespace Itmo.CSharpMicroservices.Lab1.Task1;

public static class ZipperAsync
{
    public static async IAsyncEnumerable<IEnumerable<T>> ZipAllAsync<T>(
        this IAsyncEnumerable<T> source,
        params IAsyncEnumerable<T>[] others)
    {
        IAsyncEnumerable<T>[] sequences = new[] { source }.Concat(others).ToArray();
        IAsyncEnumerator<T>[] enumerators = sequences.Select(s => s.GetAsyncEnumerator()).ToArray();

        while (true)
        {
            bool[] moveNextResults = await Task.WhenAll(enumerators.Select(e => e.MoveNextAsync().AsTask()));
            if (moveNextResults.Any(moved => !moved))
                break;

            var currentElements = enumerators.Select(e => e.Current).ToArray() as IEnumerable<T>;
            yield return currentElements;
        }
    }
}
