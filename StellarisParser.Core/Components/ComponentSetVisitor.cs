namespace StellarisParser.Core.Components
{
    public class ComponentSetVisitor : SpecVisitor<string>
    {
        public override string SpecId => Specs.SET_ID;
        public override string GetValue(stellarisParser.IdContext context)
        {
            return context.GetText().Replace('"'.ToString(),"");
        }
    }
}