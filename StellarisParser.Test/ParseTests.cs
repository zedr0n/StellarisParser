using System.ComponentModel;
using System.IO;
using System.Linq;
using StellarisParser.Core;
using StellarisParser.Core.Components;
using StellarisParser.Core.Components.Afterburners;
using StellarisParser.Core.Components.Armors;
using StellarisParser.Core.Components.CombatComputers;
using StellarisParser.Core.Components.Drives;
using StellarisParser.Core.Components.Reactors;
using StellarisParser.Core.Components.Sensors;
using StellarisParser.Core.Components.Shields;
using StellarisParser.Core.Components.Thrusters;
using StellarisParser.Core.Techs;
using Xunit;
using Component = StellarisParser.Core.Components.Component;
using Container = SimpleInjector.Container;

namespace StellarisParser.Test
{
    public class ParseTests
    {
        private Parser CreateParser()
        {
            var container = new Container();
            var root = new CompositionRoot();
            root.ComposeApplication(container);

            container.Verify();
            return container.GetInstance<Parser>();
        }

        private Container CreateContainer()
        {
            var container = new Container();
            var root = new CompositionRoot();
            root.ComposeApplication(container);

            container.Verify();
            return container;
        }


        private const string TechCosts = @"# TECH COSTS
                                       @tier1cost1 = 2000
                                       @tier1cost2 = 2500
                                       @tier1cost3 = 3000

                                       @tier2cost1 = 4000
                                       @tier2cost2 = 5000
                                       @tier2cost3 = 6000";

        private const string SolarPanelNetworks = @"tech_solar_panel_network = {
	                        area = engineering
	                        tier = 0
	                        category = { voidcraft }
	                        prerequisites = { ""tech_starbase_2"" }
                            start_tech = yes

                            potential = {
                                is_gestalt = yes
                            } 
                        }";

        private const string DestroyersWithTechCosts = @"@tier2cost1 = 4000

            tech_destroyers = {
	        cost = @tier2cost1
	        area = engineering
	        tier = 2
	        category = { voidcraft }
	        prerequisites = { ""tech_corvettes"" }
            weight = @tier2weight1

            gateway = ship
        }";

        private const string Corvettes = @"tech_corvettes = {
	        cost = 0
	        area = engineering
	        start_tech = yes
	        category = { voidcraft }
	        prerequisites = { ""tech_starbase_2"" }
                tier = 0
                }
            }";
            
        private const string Destroyers = @"tech_destroyers = {
	        cost = @tier2cost1
	        area = engineering
	        tier = 2
	        category = { voidcraft }
	        prerequisites = { ""tech_corvettes"" }
            weight = @tier2weight1

            gateway = ship
        }";
        
        private const string Thruster = @"utility_component_template = {
	key = ""DESTROYER_SHIP_THRUSTER_1""
        size = small
            icon = ""GFX_ship_part_thruster_1""
        icon_frame = 1
        power = @destroyer_power_1
            resources = {
                category = ship_components
                cost = {
                alloys = @destroyer_cost1
                }
            }
	
        modifier = {
            		ship_base_speed_mult = 0.25
		            ship_evasion_add = 3
        }
		
        prerequisites = { ""tech_thrusters_1"" }
        component_set = ""thruster_components""
        size_restriction = { destroyer }
        upgrades_to = ""DESTROYER_SHIP_THRUSTER_2""
	
        ai_weight = {
            weight = 1
        }
    }
    ";

        [Fact]
        public void CanParseSolarPanelNetworks()
        {
            var parser = CreateParser();
            
            var tech = parser.RunVisitor<TechsList>(SolarPanelNetworks).Map.First().Value;
            
            Assert.Equal("tech_solar_panel_network", tech.Name);
            Assert.Equal("engineering", tech.Area);
            Assert.Equal(0, tech.Tier);
        }

        [Fact]
        public void CanDisableTech()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var techs = container.GetInstance<TechsList>();
            
            var file = DestroyersWithTechCosts + "\n" + SolarPanelNetworks;
            techs.Aggregate(parser.RunVisitor<TechsList>(file));

            foreach (var (key, tech) in techs.Map)
            {
                tech.Disable = true;
            }

            var str = parser.ApplyModifications(file);
            var target = @"

tech_destroyers   = { cost   = @tier2cost1     area   = engineering    tier   = 2    category   = { voidcraft  }    prerequisites   = { ""tech_corvettes""  }    weight   = @tier2weight1     gateway   = ship    potential   = { has_global_flag   = dummy     }     }   
tech_solar_panel_network   = { area   = engineering    tier   = 0    category   = { voidcraft  }    prerequisites   = { ""tech_starbase_2""  }    start_tech   = yes    potential   = { has_global_flag   = dummy     }     }   ";               
            target = target.Replace("\r", "");
                
            Assert.Equal(target, str);
        }

        [Fact]
        public void CanDisableAdvReactor2()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            
            parser.ReadVars(Specs.BASE_VARS);
            foreach (var f in Directory.GetFiles(Specs.TECH_PATH))
                parser.ReadTechs(f);


            const string varsDir = Specs.NHSC_PATH + "\\common\\scripted_variables";
            foreach(var f in Directory.GetFiles(varsDir))
                parser.ReadVars(f);
            const string techDir = Specs.NHSC_PATH + "\\common\\technology";
            foreach (var f in Directory.GetFiles(techDir))
                parser.ReadTechs(f);

            var techs = container.GetInstance<TechsList>();

            var tech = techs["nhsc_tech_advanced_reactor_1"];
            Assert.NotNull(tech);
            tech.Disable = true;
            
            var str = parser.ApplyModifications(File.ReadAllText(tech.Source));
        }

        [Fact]
        public void CanParseVariables()
        {
            var parser = CreateParser();

            var vars = parser.RunVisitor<Variables>(TechCosts);
            
            Assert.Equal(6, vars.Count);
            Assert.Equal(2500, vars.Get("tier1cost2"));
        }

        [Fact]
        public void CanParseTechWithVariables()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();
            
            var vars = parser.RunVisitor<Variables>(DestroyersWithTechCosts);
            variables.Aggregate(vars);
            
            var tech = parser.RunVisitor<TechsList>(DestroyersWithTechCosts).Map.First().Value;

            Assert.Equal(4000, tech.Cost);
        }

        [Fact]
        public void CanParseTechWithSeparateVariables()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();
            
            var vars = parser.RunVisitor<Variables>(TechCosts);
            variables.Aggregate(vars);
            
            var techs = parser.RunVisitor<TechsList>(Destroyers);

            Assert.Equal(4000, techs["tech_destroyers"].Cost);
        }

        [Fact]
        public void CanParseMultipleTechs()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();

            var str = DestroyersWithTechCosts + "\n" + SolarPanelNetworks;
            variables.Aggregate(parser.RunVisitor<Variables>(str));
            var techs = parser.RunVisitor<TechsList>(str);
            
            Assert.Equal(2, techs.Count);
            Assert.Equal(4000, techs["tech_destroyers"].Cost);
        }

        [Fact]
        public void CanParseFile()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile(Specs.TECH_PATH + "\\00_eng_tech.txt",Specs.BASE_VARS);
            
            Assert.Equal(80, techs.Count);
            var tech = techs["tech_destroyers"];
            Assert.Equal("tech_corvettes", tech.Prerequisites.Single().Name);
        }

        [Fact]
        public void CanSerialiseGraph()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile(Specs.TECH_PATH + "\\00_eng_tech.txt",Specs.BASE_VARS);
            var graph = container.GetInstance<Graph>();
            graph.Populate();
            graph.Serialise(nameof(CanSerialiseGraph));
        }

        [Fact]
        public void CanParseThruster()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_required_sets.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_thrusters.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(30, components.Count);
            Assert.Equal(1.25, components.ToList().OfType<Thruster>().Max(t => t.SpeedMultiplier));
        }
        
        [Fact]
        public void CanParseReactor()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_required_sets.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_reactors.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(46, components.Count);
            Assert.Equal(10000,components.ToList().OfType<Reactor>().Max(r => r.Power));
        }

        [Fact]
        public void CanParseDrive()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_required_sets.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_drives.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(6, components.Count);
            Assert.Equal(-0.8, components.ToList().OfType<FtlDrive>().Min(f => f.WindupMultiplier));
        }
        
        [Fact]
        public void CanParseAfterburner()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_utilities_afterburners.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_afterburners.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(2, components.Count);
            Assert.Equal(0.2, components.ToList().OfType<Afterburner>().Max(a => a.Speed));
        }
        
        [Fact]
        public void CanParseSensor()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_required_sets.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_sensors.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(4, components.Count);
            Assert.Equal( 4, components.ToList().OfType<Sensor>().Max(s => s.SensorRange));
        }

        [Fact]
        public void CanParseCombatComputer()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_required_sets.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_roles.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(40, components.Count);
            Assert.Equal( 0.2, components.ToList().OfType<CombatComputer>().Max(s => s.WeaponRange));
        }

        [Fact]
        public void CanParseArmor()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadVars(Specs.BASE_PATH + "\\common\\scripted_variables\\02_scripted_variables_component_cost.txt");
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_utilities.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(21, components.Count);
            Assert.Equal( 870, components.ToList().OfType<Armor>().Max(s => s.ArmorAdd));
            Assert.Equal( 1110, components.ToList().OfType<Armor>().Max(s => s.HullAdd));
        }

        [Fact]
        public void CanParseShield()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadVars(Specs.BASE_PATH + "\\common\\scripted_variables\\02_scripted_variables_component_cost.txt");
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_utilities_shields.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_shields.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(18, components.Count);
            Assert.Equal( 1440, components.ToList().OfType<Shield>().Max(s => s.ShieldAdd));
            Assert.Equal( 12.5, components.ToList().OfType<Shield>().Max(s => s.ShieldRegen));
        }

        [Fact]
        public void CanParseShieldBooster()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadVars(Specs.BASE_PATH + "\\common\\scripted_variables\\02_scripted_variables_component_cost.txt");
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponentSets(Specs.BASE_PATH + Specs.COMPONENT_SETS_POSTFIX + "\\00_utilities_shields.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_aux.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(1, components.Count);
            Assert.Equal( 0.1, components.ToList().OfType<Shield>().Max(s => s.ShieldMultiplier));
        }

    }
}