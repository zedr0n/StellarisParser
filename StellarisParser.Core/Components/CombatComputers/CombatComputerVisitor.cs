namespace StellarisParser.Core.Components.CombatComputers
{
    public class CombatComputerVisitor : ComponentVisitor<CombatComputer>
    {
        public CombatComputerVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, SizeVisitor sizeVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor, sizeVisitor)
        {
        }
    }
}