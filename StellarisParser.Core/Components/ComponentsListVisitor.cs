using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ComponentsListVisitor : StellarisVisitor<ComponentsList>
    {
        private ThrusterVisitor _thrusterVisitor;

        public ComponentsListVisitor(ThrusterVisitor thrusterVisitor)
        {
            _thrusterVisitor = thrusterVisitor;
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
            var thruster = _thrusterVisitor.Visit(context);
            if (thruster != null)
                components.Add(thruster);
            return components;
        }
    }
}