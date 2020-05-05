namespace StellarisParser.Core.Components
{
    public class KeyVisitor : SpecVisitorString
    {
        public override string SpecId { get; } = Specs.KEY_ID;
    }
}