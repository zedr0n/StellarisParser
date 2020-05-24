using Antlr4.Runtime.Tree;

namespace StellarisParser.Core
{
    public class MinMax
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }
    
    public abstract class MinMaxVisitor : StellarisVisitor<MinMax>
    {
        private readonly Variables _variables;

        protected MinMaxVisitor(Variables variables)
        {
            _variables = variables;
        }

        public abstract string SpecId { get; }
        
        public override MinMax VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != null && !result.Equals(default(MinMax)))
                    break;
            }
            return result;
        }

        public override MinMax VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != SpecId)
                return default;

            if (context.val().@group().expr() == null)
                return default;

            var min = double.NaN;
            var max = double.NaN;
            
            foreach (var e in context.val().@group().expr())
            {
                foreach (var k in e.keyval())
                {
                    if (k.key().id().GetText() == "min")
                    {
                        if (!double.TryParse(k.val().id().GetText(), out min))
                            min = _variables.Get(e.keyval()[0].val().id().GetText());
                    }

                    if (k.key().id().GetText() == "max")
                    {
                        if (!double.TryParse(k.val().id().GetText(), out max))
                            max = _variables.Get(e.keyval()[0].val().id().GetText());
                    }
                }
            }

            var minmax = new MinMax()
            {
                Min = min,
                Max = max
            };
            
            return minmax;
        }
    }
}