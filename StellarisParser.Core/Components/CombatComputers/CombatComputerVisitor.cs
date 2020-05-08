using StellarisParser.Core.Components.Afterburners;

namespace StellarisParser.Core.Components.CombatComputers
{
    public class CombatComputerVisitor : ComponentVisitor, IStellarisVisitor<CombatComputer>
    {
        private readonly ModifierVisitor<AccuracyVisitor> _accuracyVisitor;
        private readonly ModifierVisitor<EngagementRangeMultiplierVisitor> _engagementRangeMultiplierVisitor;
        private readonly ModifierVisitor<FireRateMultiplierVisitor> _fireRateMultiplierVisitor;
        private readonly ModifierVisitor<EvasionMultiplierVisitor> _evasionMultiplierVisitor;
        private readonly ModifierVisitor<WeaponRangeVisitor> _weaponRangeVisitor;
        private readonly ModifierVisitor<TrackingVisitor> _trackingVisitor;
        
        public CombatComputerVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifierVisitor<AccuracyVisitor> accuracyVisitor, ModifierVisitor<EngagementRangeMultiplierVisitor> engagementRangeMultiplierVisitor, ModifierVisitor<FireRateMultiplierVisitor> fireRateMultiplierVisitor, ModifierVisitor<EvasionMultiplierVisitor> evasionMultiplierVisitor, ModifierVisitor<WeaponRangeVisitor> weaponRangeVisitor, ModifierVisitor<TrackingVisitor> trackingVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _accuracyVisitor = accuracyVisitor;
            _engagementRangeMultiplierVisitor = engagementRangeMultiplierVisitor;
            _fireRateMultiplierVisitor = fireRateMultiplierVisitor;
            _evasionMultiplierVisitor = evasionMultiplierVisitor;
            _weaponRangeVisitor = weaponRangeVisitor;
            _trackingVisitor = trackingVisitor;
        }

        public override Component Create()
        {
            return new CombatComputer();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var computer = base.VisitKeyval(context) as CombatComputer;
            if (computer == null)
                return null;

            computer.Accuracy = _accuracyVisitor.Visit(context.val());
            computer.EvasionMultiplier = _evasionMultiplierVisitor.Visit(context.val());
            computer.Tracking = _trackingVisitor.Visit(context.val());
            computer.WeaponRange = _weaponRangeVisitor.Visit(context.val());
            computer.FireRateMultiplier = _fireRateMultiplierVisitor.Visit(context.val());
            computer.EngagementRangeMultiplier = _engagementRangeMultiplierVisitor.Visit(context.val());
            
            return computer;
        }

        public new CombatComputer VisitContent(stellarisParser.ContentContext context)
        {
            return base.VisitContent(context) as CombatComputer;
        }
    }
}