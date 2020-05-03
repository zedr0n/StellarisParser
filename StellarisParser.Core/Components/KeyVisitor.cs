namespace StellarisParser.Core.Components
{
    public class KeyVisitor : SpecVisitor<string>
    {
        public override string SpecId { get; } = Specs.KEY_ID;
        public override string GetValue(stellarisParser.IdContext context)
        {
            return context.GetText().Replace('"'.ToString(),"").ToLower();
        }
    }
}