using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core
{
    public abstract class SingleModifierVisitor : SpecVisitorDouble
    {
        public SingleModifierVisitor(Variables variables) : base(variables)
        {
        }

        public abstract override string SpecId { get; }
        public abstract Modifier Modifier { get; }
    }
    
    public class SingleModifierVisitor<T> : SingleModifierVisitor
        where T : Modifier, new()
    {
        private readonly T _modifier;
        
        public SingleModifierVisitor(Variables variables) : base(variables)
        {
            _modifier = new T();
        }

        public override string SpecId => _modifier.Id;
        public override Modifier Modifier => new T();
    }
}