using GraphLoom.Mapper.Configuration;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class PredicateMap : IRelationConfig
    {
        private string _predicateName;
        public string GetRelationName()
        {
            return _predicateName;
        }

        public void SetRelationName(string relation)
        {
            _predicateName = relation;
        }
    }
}
