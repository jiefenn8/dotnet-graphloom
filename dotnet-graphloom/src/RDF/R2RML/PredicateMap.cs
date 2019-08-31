using GraphLoom.Mapper.Api;
using VDS.RDF;

namespace GraphLoom.Mapper.RDF.R2RML
{
    //
    // Summary:
    //     Implementation of R2RML PredicateMap with RelationMap interface. 
    public class PredicateMap : IRelationMap
    {
        private IUriNode _predicate;

        public PredicateMap(IUriNode predicate)
        {
            _predicate = predicate;
        }

        public IUriNode GetRelationTerm()
        {
            return _predicate;
        }
    }
}
