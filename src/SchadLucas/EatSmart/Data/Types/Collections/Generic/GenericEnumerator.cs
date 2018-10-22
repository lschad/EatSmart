namespace SchadLucas.EatSmart.Data.Types.Collections.Generic
{
    public class GenericEnumerator<T> : IGenericEnumerator<T>
    {
        private readonly T[] _array;
        private int _position = -1;

        public T Current => _array[_position];

        public GenericEnumerator(T[] array)
        {
            _array = array;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < _array.Length;
        }

        public void Reset() => _position = -1;
    }
}