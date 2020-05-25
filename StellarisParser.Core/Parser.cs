using System;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using SimpleInjector;
using StellarisParser.Core.Components;
using StellarisParser.Core.Localisation;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core
{
    public static class ParseTreeExtensions
    {
        public static bool IsAllComments(this string text)
        {
            var lines = text.Split("\n");
            if (lines.All(l => l.StartsWith("#")))
                return true;
            return false;
        }
        
        public static string GetTextWithWhitespace(this IParseTree context)
        {
            if (context is TerminalNodeImpl terminalNode)
                return terminalNode.Symbol?.Text;
            
            if (context.ChildCount == 0)
                return string.Empty;
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < context.ChildCount; ++i)
            {
                var s = GetTextWithWhitespace(context.GetChild(i));
                stringBuilder.Append(s);
                if (s != "@")
                    stringBuilder.Append(" ");
            }

            return stringBuilder.ToString();
        }
    }
    
    public class Parser
    {
        private readonly Container _container;
        private readonly Variables _vars;
        private readonly ComponentSets _componentSets;
        private readonly TechsList _techsList;
        private readonly TechsModifier _techsModifier;
        private readonly ComponentsList _componentsList;

        private readonly Localisation.Localisation _localisation;
        
        public string CurrentSource { get; private set; }


        
        public string ApplyModifications(string text)
        {
            var inputStream = new AntlrInputStream(text);
            var lexer = new stellarisLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new stellarisParser(commonTokenStream)
            {
                ErrorHandler = new BailErrorStrategy()
            };


            var str = _techsModifier.Visit(parser.content());
            // var str = _techModifier.Execute(parser.content());
            
            return str;
        }


        public T RunVisitor<T>(string text)
        {
            if (text == "" || text.IsAllComments())
                return default;
            
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

        public bool ReadComponents(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Aggregate(false, (current, file) => current | ReadComponents(file));

            var isError = false;
            try
            {
                CurrentSource = path;
                _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(path)));
                _componentsList.Aggregate(RunVisitor<ComponentsList>(File.ReadAllText(path)));
            }
            catch (Exception e)
            {
                isError = true;
            }

            return !isError;
        }

        public bool ReadComponentSets(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Aggregate(false, (current, file) => current | ReadComponentSets(file));

            var isError = false;
            try
            {
                CurrentSource = path;
                _componentSets.Aggregate(RunVisitor<ComponentSets>(File.ReadAllText(path)));
            }
            catch (Exception e)
            {
                isError = true;
            }

            return !isError;
        }

        public bool ReadTechs(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Aggregate(false, (current, file) => current | ReadTechs(file));

            var isError = false;
            try
            {
                CurrentSource = path;
                _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(path)));
                _techsList.Aggregate(RunVisitor<TechsList>(File.ReadAllText(path)));
            }
            catch (Exception e)
            {
                isError = true;
            }

            return !isError;
        }

        public bool ReadVars(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Aggregate(false, (current, file) => current | ReadVars(file));

            var isError = false;
            try
            {
                CurrentSource = path; 
                _vars.Aggregate(RunVisitor<Variables>(File.ReadAllText(path)));

            }
            catch (Exception e)
            {
                isError = true;
            }

            return !isError;
        }
        
        public bool ReadLocalisation(string path)
        {
            if (Directory.Exists(path))
                return Directory.GetFiles(path).Aggregate(false, (current, file) => current | ReadLocalisation(file));

            var isError = false;
            try
            {
                CurrentSource = path;
                var parser = new YamlParser();
                var localisation = parser.LoadYaml(path);
                _localisation.Aggregate(localisation);
            }
            catch (Exception e)
            {
                isError = true;
            }

            return !isError;
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

        public Parser(Container container, Variables vars, TechsList techsList, Components.ComponentsList componentsList, TechsModifier techsModifier, ComponentSets componentSets, Localisation.Localisation localisation)
        {
            _container = container;
            _vars = vars;
            _techsList = techsList;
            _componentsList = componentsList;
            _techsModifier = techsModifier;
            _componentSets = componentSets;
            _localisation = localisation;
        }
    }
}