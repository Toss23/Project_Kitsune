public class ArrayData<T>
{
    private T[] _values;

    public int Length => _values.Length;

    public ArrayData(T[] values)
    {
        _values = values;
    }

    public T Get(int index)
    {
        T result;

        if (index < _values.Length)
        {
            result = _values[index];
        }
        else
        {
            result = _values[Length - 1];
        }

        return result;
    }
}