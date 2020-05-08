using StellarisParser.Core.Components.Armors;

namespace StellarisParser.Core.Components.Shields
{
    public class ShieldVisitor : ComponentVisitor, IStellarisVisitor<Shield>
    {
        private readonly ModifierVisitor<ShieldAddVisitor> _shieldAddVisitor;
        private readonly ModifierVisitor<ShieldRegenVisitor> _shieldRegenVisitor;
        private readonly ModifierVisitor<ShieldMultiplierVisitor> _shieldMultVisitor;
        
        public ShieldVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifierVisitor<ShieldAddVisitor> shieldAddVisitor, ModifierVisitor<ShieldRegenVisitor> shieldRegenVisitor, ModifierVisitor<ShieldMultiplierVisitor> shieldMultVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _shieldAddVisitor = shieldAddVisitor;
            _shieldRegenVisitor = shieldRegenVisitor;
            _shieldMultVisitor = shieldMultVisitor;
        }

        public override Component Create()
        {
            return new Shield();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var shield = base.VisitKeyval(context) as Shield;
            if (shield == null)
                return null;

            shield.ShieldRegen = _shieldRegenVisitor.Visit(context.val());
            shield.ShieldAdd = _shieldAddVisitor.Visit(context.val());
            shield.ShieldMultiplier = _shieldMultVisitor.Visit(context.val());
            
            return shield;
        }

        public new Shield VisitContent(stellarisParser.ContentContext context)
        {
            return base.VisitContent(context) as Shield;
        }
    }
}