using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StellarisParser.Core.Localisation
{
    public class Localisation
    {
        public Dictionary<string, LocalisationElement> Dictionary { get; } = new Dictionary<string, LocalisationElement>();

        public int Count => Dictionary.Count;
        
        public void Aggregate(Localisation other)
        {
            foreach (var (key, localisationElement) in other.Dictionary)
            {
                Dictionary[key] = localisationElement;
            }
        }

        public LocalisationElement this[string key] => Dictionary.ContainsKey(key) ? Dictionary[key] : new LocalisationElement();
    }

    public class LocalisationElement
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Name
    {
        public string Key { get; set; }
        public string Content { get; set; }
    }

    public class Description
    {
        public string Key { get; set; }
        public string Content { get; set; }
    }

    public class Core
    {
        public string Key { get; set; }
        public string Content { get; set; }
    }
    
    public class YamlParser
    {
        public Localisation LoadYaml(string file)
        {
            var localisation = new Localisation();
            if (!File.Exists(file))
                return localisation;

            var dict = localisation.Dictionary;
            var descriptions = new List<Description>();
            var names = new List<Name>();
            var cores = new List<Core>();

            var lines = File.ReadLines(file);
            foreach (var l in lines)
            {
                if (!l.Contains(':'))
                    continue;
                if (l.ToLower().Contains("_desc"))
                {
                    var key = l.Split(':')[0].Replace("_desc", string.Empty).Replace("_DESC", string.Empty).Replace(' '.ToString(), string.Empty);;
                    var content = l.Split('"')[1];

                    descriptions.Add(new Description
                    {
                        Key = key,
                        Content = content
                    });
                }
                else
                {
                    var key = l.Split(':')[0].Replace(' '.ToString(), string.Empty);
                    if (l.Contains('"'))
                    {
                        var content = l.Split('"')[1];

                        cores.Add(new Core
                        {
                            Key = key,
                            Content = content
                        });
                    }
                }
            }

            var keys = cores.Select(c => c.Key);
            foreach (var k in keys)
            {
                var name = cores.FirstOrDefault(c => c.Key == k)?.Content ?? string.Empty;
                if (name != string.Empty && name.Contains("$"))
                {
                    //var key = name.Replace("$", string.Empty);
                    var tokens = name.Split("$");
                    var key = tokens[1];
                    var newName = cores.FirstOrDefault(c => c.Key == key)?.Content ?? string.Empty;
                    name = tokens[0] + newName + tokens[2];
                }

                var desc = descriptions.FirstOrDefault(d => d.Key == k)?.Content ?? string.Empty;
                if (desc == string.Empty)
                    desc = descriptions.FirstOrDefault(d => k.EndsWith(d.Key))?.Content ?? string.Empty;

                if (desc != string.Empty && desc.Contains("$"))
                {
                    var tokens = desc.Split("$");
                    var key = tokens[1].Replace("_desc",string.Empty).Replace("_DESC",string.Empty);;
                    var value = descriptions.FirstOrDefault(c => c.Key == key)?.Content ?? string.Empty;
                    desc = tokens[0] + value + tokens[2];
                }

                if (name.Contains("$"))
                    name = string.Empty;
                if (desc.Contains("$"))
                    desc = string.Empty;
                
                dict[k] = new LocalisationElement
                {
                    Key = k,
                    Name = name,
                    Description = desc,
                };
            }
            
            return localisation;
        }
    }
}