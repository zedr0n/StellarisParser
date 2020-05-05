using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore.Internal;
using QuickGraph;
using QuickGraph.Serialization;
using StellarisParser.Core.Components;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core
{
    public class Graph
    {
        private readonly TechsList _techsList;
        private readonly Components.ComponentsList _componentsList;
        private readonly Mods _mods;

        private readonly BidirectionalGraph<Vertex, SEdge<Vertex>> _graph
            = new BidirectionalGraph<Vertex, SEdge<Vertex>>();

        public Graph(TechsList techsList, Mods mods, Components.ComponentsList componentsList)
        {
            _techsList = techsList;
            _mods = mods;
            _componentsList = componentsList;
        }

        public class Vertex
        {
            [XmlAttribute("Id")]
            public string Id { get; set; }

            [XmlAttribute("Name")]
            public string Name { get; set; }
            
            [XmlAttribute("Label")]
            public string Label { get; set; }

            [XmlAttribute("Cost")]
            public double Cost { get; set; }
            
            [XmlAttribute("Tier")]
            public double Tier { get; set; }
            
            [XmlAttribute("Source")]
            public string Source { get; set; }
            
            [XmlAttribute("Area")]
            public string Area { get; set; }
            
            // Component
            [XmlAttribute("Power")]
            public double Power { get; set; }
            
            // Thruster
            [XmlAttribute("Speed")]
            public double Speed { get; set; }
            
            [XmlAttribute("Evasion")]
            public double Evasion { get; set; }
        }

        private string GetSource(string source)
        {
            if (source.StartsWith(Specs.BASE_PATH))
                return "StellarisBase";

            var modDescriptor = _mods.Map.SingleOrDefault(x => source.StartsWith(x.Key)).Value;
            if (modDescriptor != default)
                return modDescriptor.Name;

            return source;
        }

        private string GetTechName(string tech)
        {
            return tech.Replace("tech_", string.Empty);
        }

        private bool AddComponent(Component component)
        {
            if (_graph.Vertices.Any(t => t.Id == component.Key))
                return false;

            var tech = component.Prerequisites.FirstOrDefault();

            var thruster = component as Thruster;
            var vertex = new Vertex
            {
                Id = component.Key,
                Name = component.Key,
                Power = component.Power,
                Tier = tech?.Tier ?? 0,
                Cost = tech?.Cost ?? 0,
                Source = GetSource(component.Source),
                Label = component.Key,
                Speed = thruster?.SpeedMultipler ?? 0,
                Evasion = thruster?.Evasion ?? 0
            };
            _graph.AddVertex(vertex);
            return true;
        }
        
        private bool AddTech(Tech tech)
        {
            if (_graph.Vertices.Any(t => t.Id == tech.Name))
                return false;

            var vertex = new Vertex
            {
                Id = tech.Name,
                Name = tech.Name,
                Label = $"[{tech.Tier}]{GetTechName(tech.Name)}",
                Cost = tech.Cost,
                Tier = tech.Tier,
                Source = GetSource(tech.Source),
                Area = tech.Area
            };

            _graph.AddVertex(vertex);
            return true;
        }

        private Vertex GetVertex(Component component)
        {
            return _graph.Vertices.SingleOrDefault(x => x.Id == component.Key);
        }

        private Vertex GetVertex(Tech tech)
        {
            return _graph.Vertices.SingleOrDefault(t => t.Id == tech.Name);
        }

        public void Populate()
        {
            _graph.Clear();

            foreach (var (key, tech) in _techsList.Map)
            {
                AddTech(tech);
                
                foreach (var t in tech.Prerequisites)
                {
                    AddTech(t);

                    var edge = new SEdge<Vertex>(GetVertex(t),GetVertex(tech));
                    _graph.AddEdge(edge);
                }
            }

            foreach (var (key, component) in _componentsList.Map)
            {
                AddComponent(component);
                foreach (var t in component.Prerequisites)
                {
                    var edge = new SEdge<Vertex>(GetVertex(t), GetVertex(component));
                    _graph.AddEdge(edge);
                }
            }
        }
        
        public void Serialise(string filename = "streams")
        {
            _graph.SerializeToGraphML<Vertex, SEdge<Vertex>, BidirectionalGraph<Vertex,  SEdge<Vertex>>>(filename + ".graphml");
        }
    }
}