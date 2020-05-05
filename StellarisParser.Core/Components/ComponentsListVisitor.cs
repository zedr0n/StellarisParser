using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ComponentsListVisitor : StellarisVisitor<ComponentsList>
    {
        private readonly ThrusterVisitor _thrusterVisitor;
        private readonly ReactorVisitor _reactorVisitor;

        public ComponentsListVisitor(ThrusterVisitor thrusterVisitor, ReactorVisitor reactorVisitor)
        {
            _thrusterVisitor = thrusterVisitor;
            _reactorVisitor = reactorVisitor;
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

            var reactor = _reactorVisitor.Visit(context);
            if (reactor != null)
                components.Add(reactor);
            
            return components;
        }
    }
}