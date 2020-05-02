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
            [XmlAttribute("Label")]
            public string Id { get; set; }
            
            [XmlAttribute("Cost")]
            public double Cost { get; set; }
            
            [XmlAttribute("Tier")]
            public double Tier { get; set; }

        }

        private bool AddTech(Tech tech)
        {
            if (_graph.Vertices.Any(t => t.Id == tech.Name))
                return false;

            var vertex = new Vertex
            {
                Id = tech.Name,
                Cost = tech.Cost,
                Tier = tech.Tier
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

            foreach (var tech in _techs.ToList())
            {
                if (!AddTech(tech))
                    continue;
                
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