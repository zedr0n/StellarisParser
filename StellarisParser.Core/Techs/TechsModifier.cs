using System.Linq;
using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Techs
{
    public class TechsModifier : StellarisVisitor<string>
    {
        private readonly TechModifier _techModifier;
        private readonly TechsList _techsList;
        
        public TechsModifier(TechModifier techModifier, TechsList techsList)
        {
            _techModifier = techModifier;
            _techsList = techsList;
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
            
            var id = context.key().id()?.GetText();
            if (id == null)
                return null;

            var tech = _techsList[id];
            var str = context.GetTextWithWhitespace();
            if (tech != null && tech.Disable)
                str = _techModifier.Execute(context);

            return str;
        }
    }
}