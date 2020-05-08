namespace StellarisParser.Core.Components.Thrusters
{
    public class ThrusterVisitor : ComponentVisitor, IStellarisVisitor<Thruster>
    {
        private readonly ModifierVisitor<EvasionVisitor> _evasionVisitor;
        private readonly ModifierVisitor<BaseSpeedMultiplierVisitor> _speedVisitor;

        public override Component Create() => new Thruster(); 

        public ThrusterVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, 
            Parser parser, ComponentsList componentsList, ComponentSets componentSets, 
            ModifierVisitor<EvasionVisitor> evasionVisitor,
            ModifierVisitor<BaseSpeedMultiplierVisitor> speedVisitor) : 
            base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _evasionVisitor = evasionVisitor;
            _speedVisitor = speedVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var thruster = base.VisitKeyval(context) as Thruster;
            if (thruster == null)
                return null;
            
            var evasion = _evasionVisitor.Visit(context.val());
            var speed = _speedVisitor.Visit(context.val());

            thruster.Evasion = evasion;
            thruster.SpeedMultiplier = speed;

            return thruster;
        }

        public new Thruster VisitContent(stellarisParser.ContentContext context)
        {
            return (Thruster) base.VisitContent(context);
        }
    }
}