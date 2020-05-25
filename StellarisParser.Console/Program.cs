using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using SimpleInjector;
using StellarisParser.Core;
using StellarisParser.Core.Components;
using StellarisParser.Core.Icons;
using StellarisParser.Core.Localisation;
using StellarisParser.Core.Techs;
using static StellarisParser.Core.Specs;

namespace StellarisParser.Console
{
    class Program
    {
        private static readonly HashSet<string> Read = new HashSet<string>();

        public static readonly List<string> Excludes = new List<string>
        {
            TECH_PATH + "\\00_repeatable.txt",
            COMPONENT_PATH + "\\README_weapon_component_stat_docs.txt"
        }; 
        
        private static Parser _parser;
        private static Mods _mods;

        private static void ReadTech(string file)
        {
            if (Excludes.Contains(file))
                return;

            if (!file.EndsWith(".txt"))
                return;
            
            if (File.Exists(file))
            {
                var read = Read.Add(file);

                if(!read)
                    System.Console.Write("Reading " + file + "...");
                    
                var isError = !_parser.ReadTechs(file);
                if(!read && !isError)
                    System.Console.WriteLine(" OK!");
                else if(!read)
                    System.Console.WriteLine(" Error!");
            }
            else 
                System.Console.WriteLine(file + " not found!");
        }

        private static void ReadComponents(string file)
        {
            if (Excludes.Contains(file))
                return;

            if (!file.EndsWith(".txt"))
                return;

            if (File.Exists(file))
            {
                var read = Read.Add(file);

                if (!read)
                    System.Console.Write("Reading " + file + "...");

                var isError = !_parser.ReadComponents(file); 
                if (!read && !isError)
                    System.Console.WriteLine(" OK!");
                else if(!read)
                    System.Console.WriteLine(" Error!");

            }
            else
                System.Console.WriteLine(file + " not found!");
        }

        private static void ReadComponentSets(string file)
        {
                if (Excludes.Contains(file))
                    return;

                if (!file.EndsWith(".txt"))
                    return;

                if (File.Exists(file))
                {
                    System.Console.Write("Reading " + file + "...");
                    if(_parser.ReadComponentSets(file))
                        System.Console.WriteLine(" OK!");
                    else
                        System.Console.WriteLine(" Error!");
                }
                else
                    System.Console.WriteLine(file + " not found!");
        }

        private static void ReadTechs(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
                ReadTech(file);
        }

        private static void ReadAllComponentSets(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
                ReadComponentSets(file);
        }
        
        private static void ReadAllComponents(string directory)
        {
            // reading twice to process all upgrades
            foreach (var file in Directory.GetFiles(directory))
                ReadComponents(file);
            foreach (var file in Directory.GetFiles(directory))
                ReadComponents(file);

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
                if (_parser.ReadVars(file))
                    System.Console.WriteLine(" OK!");
                else
                    System.Console.WriteLine(" Error!");
            }
        }
        
        private static void ReadLocalisations(string directory)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                System.Console.Write("Reading " + file + "...");
                if(_parser.ReadLocalisation(file))
                    System.Console.WriteLine(" OK!");
                else
                    System.Console.WriteLine(" Error!");
            }
        }

        private static void StoreIcons(TechsList techList, string path)
        {
            var storeIcons = true;
            if (storeIcons)
            {
                var converter = new IconConverter();
                foreach (var tech in techList.ToList())
                {
                    var file = path + "\\" + tech.Key + ".dds";
                    if (!Directory.Exists("icons"))
                        Directory.CreateDirectory("icons");
                    if (File.Exists(file))
                    {
                        var bitmap = converter.ConvertIcon(file);
                        bitmap.Save($".\\icons\\{tech.Key}.png", ImageFormat.Png);
                        // bitmap.Save(Path.ChangeExtension(path, ".png"), ImageFormat.Png);
                    }
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

                var localisationDir = modDir + "\\localisation\\english";
                if (Directory.Exists(localisationDir))
                    ReadLocalisations(localisationDir);
                
                var varsDir = modDir + "\\common\\scripted_variables";
                if (Directory.Exists(varsDir))
                    ReadVars(varsDir);
                var techDir = modDir + "\\common\\technology";
                if (Directory.Exists(techDir))
                    ReadAllTechs(techDir);

                var setDir = modDir + COMPONENT_SETS_POSTFIX;
                if (Directory.Exists(setDir))
                    ReadAllComponentSets(setDir);
                var componentDir = modDir + "\\common\\component_templates";
                if (Directory.Exists(componentDir))
                    ReadAllComponents(componentDir);
                
                
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
            //_parser.ReadVars(BASE_VARS);
            ReadVars(BASE_VARS_DIR);

            System.Console.WriteLine("Reading localisation...");
            ReadLocalisations(LOCALISATION_PATH);
            
            System.Console.WriteLine("Reading base techs...");
            ReadAllTechs(TECH_PATH);
            System.Console.WriteLine("Reading base components...");
            ReadAllComponentSets(COMPONENT_SETS_PATH);
            ReadAllComponents(COMPONENT_PATH);

            foreach (var d in args)
            {
                ReadMod(d);
                StoreIcons(container.GetInstance<TechsList>(),d + Specs.TECH_ICONS_POSTFIX);
            }


            var graph = container.GetInstance<Graph>();
            graph.Populate();
            graph.Serialise("graph");
            
            // store icons
            var path = Specs.BASE_PATH + Specs.TECH_ICONS_POSTFIX;
            var techList = container.GetInstance<TechsList>();
            StoreIcons(techList, path);
            
            // store json
            var techJson = JsonConvert.SerializeObject(techList.Map, Formatting.Indented);
            File.WriteAllText(@"techs.json", techJson);

            var componetList = container.GetInstance<ComponentsList>();
            var componentJson = JsonConvert.SerializeObject(componetList.Map, Formatting.Indented);
            File.WriteAllText(@"components.json", componentJson);

            var localisationList = container.GetInstance<Localisation>();
            var localisationJson = JsonConvert.SerializeObject(localisationList.Dictionary, Formatting.Indented);
            File.WriteAllText(@"localisation.json", localisationJson);
        }
    }
}
