namespace StellarisParser.Core.Modifiers
{
    public abstract class Modifier
    {
        public double Value { get; set; }
        public abstract string Id { get; }
    }
}