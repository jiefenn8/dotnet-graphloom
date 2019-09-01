using GraphLoom.Mappers.Api;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using VDS.RDF;

namespace GraphLoom.Mappers.Rdf.R2rml
{
    //
    // Summary:
    //     Implementation of R2RML SubjectMap with PropertyMap interface 
    //     using IUriNode as return type.
    public class SubjectMap : IPropertyMap
    {
        private static Regex s_templateRegex = new Regex("{(.*?)}", RegexOptions.Compiled);
        private NodeFactory _nodeFactory = new NodeFactory();
        private List<IUriNode> _classes = new List<IUriNode>();
        private string _template;

        public SubjectMap(string template)
        {
            _template = template;
        }

        public IUriNode GenerateEntityTerm(IReadOnlyDictionary<string, string> entityRow)
        {
            MatchCollection matches = s_templateRegex.Matches(_template);
            string generatedTerm = _template.Replace(matches[0].Groups[0].Value, entityRow[matches[0].Groups[1].Value]);
            return _nodeFactory.CreateUriNode(UriFactory.Create(generatedTerm)); 
        }

        public IReadOnlyCollection<IUriNode> ListEntityClasses()
        {
            return new ReadOnlyCollection<IUriNode>(_classes);
        }

        public void AddClass(IUriNode clazz)
        {
            _classes.Add(clazz);
        }

        public void AddClasses(IEnumerable<IUriNode> classes)
        {
            _classes.AddRange(classes);
        }
    }
}
