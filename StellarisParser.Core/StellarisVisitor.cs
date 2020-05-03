using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace StellarisParser.Core
{
    public interface IStellarisVisitor<T>
    {
        T VisitContent([NotNull] stellarisParser.ContentContext context);
    }
    
    public abstract class StellarisVisitor<T> : stellarisBaseVisitor<T>, IStellarisVisitor<T>
    {
        public int Level(RuleContext context)
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