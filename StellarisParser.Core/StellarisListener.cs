using System.Collections.Generic;
using Antlr4.Runtime;

namespace StellarisParser.Core
{
    public abstract class StellarisListener : stellarisBaseListener
    {
        private List<RuleContext> GetParents(RuleContext context)
        {
            var parents = new List<RuleContext>();
            while(!context.Parent.IsEmpty)
                parents.Add(context.Parent);

            return parents;
        }
        
        
    }
}