namespace StellarisParser.Core.Components.Auxilaries
{
    public class AuxilaryVisitor : ComponentVisitor<Auxilaries.Auxilary>
    {
        public AuxilaryVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, SizeVisitor sizeVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor, sizeVisitor)
        {
        }
    }
}