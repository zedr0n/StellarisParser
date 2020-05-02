namespace StellarisParser.Core
{
    public class Variable<T>
    {
        public Variable(string id, T value)
        {
            Id = id;
            Value = value;
        }

        public string Id { get; }
        public T Value { get; }
    }
}