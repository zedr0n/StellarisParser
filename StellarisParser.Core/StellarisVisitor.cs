using Antlr4.Runtime;

namespace StellarisParser.Core
{
    public class StellarisVisitor<T> : stellarisBaseVisitor<T>
    {
        protected int Level(RuleContext context)
        {
            var lvl = 0;
            var parent = context.Parent;
            while (!parent.IsEmpty)
            {
                lvl++;
                parent = parent.Parent;
            }

            return lvl;
        }
    }
}