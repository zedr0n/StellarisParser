using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SimpleInjector;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core
{
    public class Parser
    {
        private readonly Container _container;
        private readonly Variables _vars;
        private readonly TechsList _techsList;
        private readonly Components.ComponentsList _componentsList;
        public string CurrentSource { get; private set; }

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

            var visitor = _container.GetInstance<IStellarisVisitor<T>>();
            return visitor.VisitContent(context);
        }
        
        public TechsList ReadFile(string filename, string baseVars = "")
        {
            if (!File.Exists(filename))
                return null;

            if (baseVars != "" && File.Exists(baseVars))
                ReadVars(baseVars);
            
            // run parser twice to process prerequisites
            ReadTechs(filename);
            return ReadTechs(filename);
        }

        public void ReadComponents(string file)
        {
            CurrentSource = file;
            _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(file)));
            _componentsList.Aggregate(RunVisitor<Components.ComponentsList>(File.ReadAllText(file)));
        }
        
        public TechsList ReadTechs(string file)
        {
            CurrentSource = file;
            _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(file)));
            _techsList.Aggregate(RunVisitor<TechsList>(File.ReadAllText(file)));
            return _techsList;
            //_techs.Aggregate(RunVisitor<Techs>(File.ReadAllText(file)));
        }

        public void ReadVars(string file)
        {
            CurrentSource = file;
            _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(file)));
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

        public Parser(Container container, Variables vars, TechsList techsList, Components.ComponentsList componentsList)
        {
            _container = container;
            _vars = vars;
            _techsList = techsList;
            _componentsList = componentsList;
        }
    }
}