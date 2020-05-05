namespace StellarisParser.Core.Techs
{
    public class HasPotential : stellarisBaseListener
    {
        public bool Result { get; set; }
        
        public override void EnterKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id() != null && context.key().id().GetText() == Specs.POTENTIAL_ID)
                Result = true;
                
            base.EnterKeyval(context);
        }
    }
}