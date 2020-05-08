namespace StellarisParser.Core.Components.Armors
{
    public class ArmorVisitor : ComponentVisitor, IStellarisVisitor<Armor>
    {
        private readonly ModifierVisitor<ArmorAddVisitor> _armorAddVisitor;
        private readonly ModifierVisitor<HullAddVisitor> _hullAddVisitor;
        private readonly ModifierVisitor<ShieldRegenVisitor> _shieldRegenVisitor;
        private readonly ModifierVisitor<ShieldAddVisitor> _shieldVisitor;
        
        public ArmorVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifierVisitor<ArmorAddVisitor> armorAddVisitor, ModifierVisitor<HullAddVisitor> hullAddVisitor, ModifierVisitor<ShieldRegenVisitor> shieldRegenVisitor, ModifierVisitor<ShieldAddVisitor> shieldVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _armorAddVisitor = armorAddVisitor;
            _hullAddVisitor = hullAddVisitor;
            _shieldRegenVisitor = shieldRegenVisitor;
            _shieldVisitor = shieldVisitor;
        }

        public override Component Create()
        {
            return new Armor();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var armor = base.VisitKeyval(context) as Armor;
            if (armor == null)
                return null;

            armor.ArmorAdd = _armorAddVisitor.Visit(context.val());
            armor.HullAdd = _hullAddVisitor.Visit(context.val());
            armor.ShieldRegen = _shieldRegenVisitor.Visit(context.val());
            armor.Shield = _shieldVisitor.Visit(context.val());
            
            return armor;
        }

        public new Armor VisitContent(stellarisParser.ContentContext context)
        {
            return base.VisitContent(context) as Armor;
        }
    }
}