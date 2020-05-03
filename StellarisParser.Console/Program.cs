using System;
using System.Collections.Generic;
using System.IO;
using SimpleInjector;
using StellarisParser.Core;
using static StellarisParser.Core.Specs;

namespace StellarisParser.Console
{
    class Program
    {
        private static readonly HashSet<string> Read = new HashSet<string>();

        public static readonly List<string> Excludes = new List<string>
        {
            TECH_PATH + "\\00_repeatable.txt"
        }; 
        
        private static Parser _parser;
        private static Mods _mods;

        private static void ReadTech(string file)
        {
            if (Excludes.Contains(file))
                return;
            
            if (File.Exists(file))
            {
                var read = Read.Add(file);
                try
                {
                    if(!read)
                        System.Console.Write("Reading " + file + "...");
                    
                    _parser.ReadTechs(file);
                    if(!read)
                        System.Console.WriteLine(" OK!");
                    
                }
                catch (Exception e)
                {
                    if (!read)
                        System.Console.WriteLine(" Error!");
                }
            }
            else 
                System.Console.WriteLine(file + " not found!");
        }
        
        private static void ReadTechs(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
                ReadTech(file);
        }

        private static void ReadAllTechs(string directory)
        {
            // reading twice to process all prerequisites
            ReadTechs(directory);
            ReadTechs(directory);
        }

        private static void ReadVars(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                System.Console.Write("Reading " + file + "...");
                try
                {
                    _parser.ReadVars(file);
                    //_vars.Aggregate(_parser.RunVisitor<Variables>(File.ReadAllText(file)));
                    System.Console.WriteLine(" OK!");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(" Error!");
                }
            }
        }

        public static void ReadMod(string modDir)
        {
            if (Directory.Exists(modDir))
            {
                var descriptor = modDir + "\\descriptor.mod";
                if (File.Exists(descriptor))
                {
                    var desc = _parser.RunVisitor<ModDescriptor>(File.ReadAllText(descriptor));
                    _mods.Map.Add(modDir, desc);
                }
                
                var varsDir = modDir + "\\common\\scripted_variables";
                if (Directory.Exists(varsDir))
                    ReadVars(varsDir);
                var techDir = modDir + "\\common\\technology";
                if (Directory.Exists(techDir))
                    ReadAllTechs(techDir);
            }

        }
        
        static void Main(string[] args)
        {
            if (!File.Exists(BASE_VARS))
            {
                System.Console.WriteLine(BASE_VARS + " not found!");
                return;
            }

            // read base vars
            var container = new Container();
            var root = new CompositionRoot();
            root.ComposeApplication(container);

            _parser = container.GetInstance<Parser>();
            _mods = container.GetInstance<Mods>();
            _parser.ReadVars(BASE_VARS);

            System.Console.WriteLine("Reading base techs...");
            ReadAllTechs(TECH_PATH);

            foreach (var d in args)
                ReadMod(d);
            
            var graph = container.GetInstance<Graph>();
            graph.Populate();
            graph.Serialise("graph");
        }
    }
}
