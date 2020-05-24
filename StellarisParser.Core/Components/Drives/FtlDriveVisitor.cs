namespace StellarisParser.Core.Components.Drives
{
    public class FtlDriveVisitor : ComponentVisitor<FtlDrive>
    {
        private readonly JumpDriveVisitor _jumpDriveVisitor;
        public FtlDriveVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, JumpDriveVisitor jumpDriveVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor)
        {
            _jumpDriveVisitor = jumpDriveVisitor;
        }

        public override FtlDrive VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var ftlDrive = base.VisitKeyval(context);
            if (ftlDrive == null)
                return null;
            
            var isJumpDrive = _jumpDriveVisitor.Visit(context.val());

            ftlDrive.JumpDrive = isJumpDrive;
            return ftlDrive;
        }
    }
}