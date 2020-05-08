namespace StellarisParser.Core.Components.Drives
{
    public class JumpDriveVisitor : SpecVisitor<bool>
    {
        public override string SpecId => Specs.JUMPDRIVE_ID;

        public override bool GetValue(stellarisParser.IdContext context)
        {
            if (context.GetText() == "yes")
                return true;
            return false;
        }
    }
}