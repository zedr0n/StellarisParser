namespace StellarisParser.Core.Components.Armors
{
    public class HullAddVisitor : SpecVisitorDouble 
    {
        public HullAddVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.HULL_ID;
    }
}