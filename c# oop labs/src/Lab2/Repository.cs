using System.Collections;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public class Repository<T> where T : BaseEntity
{
    private readonly Dictionary<Guid, T> values;

    public int Length { get; private set; }

    public void Add(T value)
    {
        ++Length;
        values.Add(value.ID, value);
    }

    public void Delete(Guid id)
    {
        --Length;
        values.Remove(id);
    }

    public IDictionaryEnumerator GetEnumerator()
    {
        return values.GetEnumerator();
    }

    public T GetValue(Guid id)
    {
        return values[id];
    }

    public Repository()
    {
        values = new Dictionary<Guid, T>();
        Length = 0;
    }
}
