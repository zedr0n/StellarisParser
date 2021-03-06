using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Techs
{
    public class TechModifier : stellarisBaseListener
    {
        private const string Potential = @"potential = { has_global_flag = dummy }";
        private bool _hasPotential;
        
        public class HasPotential : stellarisBaseListener
        {
            public bool Result { get; set; }
        
            public override void EnterKeyval(stellarisParser.KeyvalContext context)
            {
                if (context.key().id() != null && context.key().id().GetText() == Specs.POTENTIAL_ID)
                    Result = true;

                base.EnterKeyval(context);
            }
        }

        private static stellarisParser.KeyvalContext GetNoPotential()
        {
            var inputStream = new AntlrInputStream(Potential);
            var lexer = new stellarisLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new stellarisParser(commonTokenStream)
            {
                ErrorHandler = new BailErrorStrategy()
            };

            return parser.content().expr()[0].keyval()[0];
        }
        
        public string Execute(RuleContext context)
        {
            var walker = new ParseTreeWalker();
            var listener = new HasPotential();
            walker.Walk(listener, context);
            _hasPotential = listener.Result;
            
            walker.Walk(this, context);

            return context.GetTextWithWhitespace();
        }
        
        public int Level(RuleContext context)
        {
            var lvl = 0;
            var parent = context.Parent;
            while (!parent.IsEmpty)
            {
                lvl++;
                parent = parent.Parent;
            }

            return lvl;
        }
        
        public override void EnterKeyval(stellarisParser.KeyvalContext context)
        {
            var id = context.key()?.id()?.GetText();
            if (_hasPotential && id == Specs.POTENTIAL_ID)
            {
                var ctx = GetNoPotential();

                var firstChild = context.val().@group().GetChild(0);
                var lastChild = context.val().@group().GetChild(context.val().@group().ChildCount - 1);
                context.val().@group().CopyFrom(ctx.val().@group());
                context.val().@group().children.Add(firstChild);
                context.val().@group().children.Add(ctx.val().@group().children[1]);
                context.val().@group().children.Add(lastChild);

                return;
            }
            
            var lvl = Level(context);
            if (lvl > 1)
                return;

            if (id == null)
                return;
            
            if (!_hasPotential)
            {
                var ctx2 = GetNoPotential();
                ctx2.Parent = context.val().@group().expr()[0];
                context.val().group().expr()[0].AddChild(ctx2);
            }
            
            base.EnterKeyval(context);
        }
    }
}