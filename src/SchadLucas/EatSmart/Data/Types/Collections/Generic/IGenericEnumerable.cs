namespace SchadLucas.EatSmart.Data.Types.Collections.Generic
{
    public interface IGenericEnumerable<out T>
    {
        IGenericEnumerator<T> GetEnumerator();
    }
}