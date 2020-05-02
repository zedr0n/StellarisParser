using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SimpleInjector;

namespace StellarisParser.Core
{
    public class Parser
    {
        private readonly Container _container;

        public T RunVisitor<T>(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var lexer = new stellarisLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new stellarisParser(commonTokenStream)
            {
                ErrorHandler = new BailErrorStrategy()
            };

            var context = parser.content();

            var visitor = _container.GetInstance<StellarisVisitor<T>>();
            return visitor.VisitContent(context);
        }
        
        public void RunListeners(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var lexer = new stellarisLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new stellarisParser(commonTokenStream)
            {
                ErrorHandler = new BailErrorStrategy()
            };

            var context = parser.content();
            
            foreach(var listener in _container.GetAllInstances<IstellarisListener>())
                ParseTreeWalker.Default.Walk(listener, context);    
        }
        
        public Parser(Container container)
        {
            _container = container;
        }
    }
}