using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using QuickGraph;
using QuickGraph.Serialization;

namespace StellarisParser.Core
{
    public class Graph
    {
        private readonly Techs _techs;

        private readonly BidirectionalGraph<Vertex, SEdge<Vertex>> _graph
            = new BidirectionalGraph<Vertex, SEdge<Vertex>>();

        public Graph(Techs techs)
        {
            _techs = techs;
        }

        public class Vertex
        {
            [XmlAttribute("Id")]
            public string Id { get; set; }

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

        }

        private string GetSource(string source)
        {
            if (source.StartsWith(Specs.BASE_PATH))
                source = "StellarisBase";

            return source;
        }

        private string GetTechName(string tech)
        {
            return tech.Replace("tech_", string.Empty);
        }

        private bool AddTech(Tech tech)
        {
            if (_graph.Vertices.Any(t => t.Id == tech.Name))
                return false;

            var vertex = new Vertex
            {
                Id = tech.Name,
                Label = GetTechName(tech.Name),
                Cost = tech.Cost,
                Tier = tech.Tier,
                Source = GetSource(tech.Source),
                Area = tech.Area
            };

            _graph.AddVertex(vertex);
            return true;
        }

        private Vertex GetVertex(Tech tech)
        {
            return _graph.Vertices.SingleOrDefault(t => t.Id == tech.Name);
        }

        public void Populate()
        {
            _graph.Clear();

            foreach (var (key, tech) in _techs.Map)
            {
                AddTech(tech);
                
                foreach (var t in tech.Prerequisites)
                {
                    AddTech(t);

                    var edge = new SEdge<Vertex>(GetVertex(t),GetVertex(tech));
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