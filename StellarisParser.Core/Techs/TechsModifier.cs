using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Techs
{
    public class TechsModifier : StellarisVisitor<string>
    {
        private readonly TechModifier _techModifier;

        public TechsModifier(TechModifier techModifier)
        {
            _techModifier = techModifier;
        }

        public override string VisitChildren(IRuleNode node)
        {
            var result = "";
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result += "\n" + nextResult;

            }
            return result;
        }

        public override string VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;

            if (context.key().id() == null)
                return null;

            var str = _techModifier.Execute(context);

            return str;
        }
    }
}