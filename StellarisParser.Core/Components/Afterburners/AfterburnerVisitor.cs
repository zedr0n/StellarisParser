namespace StellarisParser.Core.Components.Afterburners
{
    public class AfterburnerVisitor : ComponentVisitor<Afterburner>
    {
        public AfterburnerVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, 
            ComponentsList componentsList, ComponentSets componentSets, 
            ModifiersVisitor modifiersVisitor) 
            : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor)
        {
        }
    }
}