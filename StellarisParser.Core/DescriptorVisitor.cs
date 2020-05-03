using Antlr4.Runtime.Tree;

namespace StellarisParser.Core
{
    public class DescriptorVisitor : StellarisVisitor<ModDescriptor>
    {
        public override ModDescriptor VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != null && !result.Equals(default(ModDescriptor)))
                    break;
            }
            return result;
        }
        
        public override ModDescriptor VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != "name")
                return null;

            var val = context.val().id().GetText();
            
            return new ModDescriptor
            {
                Name = val
            };
        }
    }
}