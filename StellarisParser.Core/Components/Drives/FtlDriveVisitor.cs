namespace StellarisParser.Core.Components.Drives
{
    public class FtlDriveVisitor : ComponentVisitor, IStellarisVisitor<FtlDrive>
    {
        private readonly ModifierVisitor<ShipWindupVisitor> _shipWindupVisitor;
        private readonly ModifierVisitor<JumpDriveRangeMultiplierVisitor> _rangeVisitor;
        private readonly JumpDriveVisitor _jumpDriveVisitor;
        
        public FtlDriveVisitor(
            KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor,
            Parser parser,
            ComponentsList componentsList, ComponentSets componentSets,
            ModifierVisitor<ShipWindupVisitor> shipWindupVisitor, ModifierVisitor<JumpDriveRangeMultiplierVisitor> rangeVisitor, JumpDriveVisitor jumpDriveVisitor)
            : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _shipWindupVisitor = shipWindupVisitor;
            _rangeVisitor = rangeVisitor;
            _jumpDriveVisitor = jumpDriveVisitor;
        }
        
        public override Component Create()
        {
            return new FtlDrive();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var ftlDrive = base.VisitKeyval(context) as FtlDrive;
            if (ftlDrive == null)
                return null;
            
            var shipWindup = _shipWindupVisitor.Visit(context.val());
            var jumpDriveRangeMultiplier = _rangeVisitor.Visit(context.val());
            var isJumpDrive = _jumpDriveVisitor.Visit(context.val());

            ftlDrive.JumpDrive = isJumpDrive;
            ftlDrive.WindupMultiplier = shipWindup;
            ftlDrive.JumpDriveRangeMultiplier = jumpDriveRangeMultiplier;

            return ftlDrive;
        }

        public FtlDrive VisitContent(stellarisParser.ContentContext context)
        {
            return (FtlDrive) base.VisitContent(context);
        }
    }
}