namespace StellarisParser.Core.Components.Armors
{
    public class ArmorAddVisitor : SpecVisitorDouble
    {
        public ArmorAddVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.ARMOR_ID;
    }
}