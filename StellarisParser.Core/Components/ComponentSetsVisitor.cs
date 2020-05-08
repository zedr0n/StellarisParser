using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ComponentSetsVisitor : StellarisVisitor<ComponentSets>
    {
        public override ComponentSets VisitChildren(IRuleNode node)
        {
            var result = new ComponentSets();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result.Aggregate(nextResult);

            }
            return result;
        }

        public override ComponentSets VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;

            if (context.key().id()?.GetText() != Specs.SET_ID)
                return null;

            var componentSets = new ComponentSets();
            foreach (var c in context.val().@group().expr()[0].children)
            {
                var keyval = c as stellarisParser.KeyvalContext;
                if (keyval?.key()?.GetText() != "key")
                    continue;
                var set = keyval?.val().GetText()?.Replace('"'.ToString(),"");
                if (set != null) 
                    componentSets.Add(set);
            }

            return componentSets;
        }
    }
}