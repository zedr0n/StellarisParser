using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core
{
    public abstract class ModifierVisitor : SpecVisitorDouble
    {
        public ModifierVisitor(Variables variables) : base(variables)
        {
        }

        public abstract override string SpecId { get; }
        public abstract Modifier Modifier { get; }
    }
    
    public class ModifierVisitor<T> : ModifierVisitor
        where T : Modifier, new()
    {
        private readonly T _modifier;
        
        public ModifierVisitor(Variables variables) : base(variables)
        {
            _modifier = new T();
        }

        public override string SpecId => _modifier.Id;
        public override Modifier Modifier => new T();
    }
}