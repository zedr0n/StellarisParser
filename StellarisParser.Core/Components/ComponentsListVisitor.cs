using System.Collections.Generic;
using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ComponentsListVisitor : StellarisVisitor<ComponentsList>
    {
        private readonly IEnumerable<IComponentVisitor> _componentVisitors;

        public ComponentsListVisitor(IEnumerable<IComponentVisitor> componentVisitors)
        {
            _componentVisitors = componentVisitors;
        }

        public override ComponentsList VisitChildren(IRuleNode node)
        {
            var result = new ComponentsList();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result.Aggregate(nextResult);

            }
            return result;
        }

        public override ComponentsList VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;

            if (context.key().id() == null)
                return null;

            var components = new ComponentsList();
            foreach (var visitor in _componentVisitors)
            {
                var component = visitor.Visit(context);
                if (component != null)
                    components.Add(component);
            }
            
            return components;
        }
    }
}