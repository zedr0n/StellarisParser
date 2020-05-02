using Antlr4.Runtime.Tree;

namespace StellarisParser.Core
{
    public class VariableVisitor : StellarisVisitor<Variables>
    {
        public override Variables VisitChildren(IRuleNode node)
        {
            var result = new Variables();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result.Aggregate(nextResult);

            }
            return result;
        }

        public override Variables VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().attrib() == null)
                return null;

            var val = double.Parse(context.val().id().GetText());

            var variable = new Variable<double>(context.key().attrib().id().GetText(), val);
            var vars = new Variables();
            vars.Add(variable);
            return vars;
        }
    }
}