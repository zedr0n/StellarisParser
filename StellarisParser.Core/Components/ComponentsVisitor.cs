using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ComponentsVisitor : StellarisVisitor<Components>
    {
        private ThrusterVisitor _thrusterVisitor;

        public ComponentsVisitor(ThrusterVisitor thrusterVisitor)
        {
            _thrusterVisitor = thrusterVisitor;
        }

        public override Components VisitChildren(IRuleNode node)
        {
            var result = new Components();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result.Aggregate(nextResult);

            }
            return result;
        }

        public override Components VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;

            if (context.key().id() == null)
                return null;

            var components = new Components();
            var thruster = _thrusterVisitor.Visit(context);
            if (thruster != null)
                components.Add(thruster);
            return components;
        }
    }
}