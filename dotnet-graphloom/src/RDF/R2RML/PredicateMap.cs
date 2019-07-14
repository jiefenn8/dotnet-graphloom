using GraphLoom.Mapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.RDF.R2RML
{
    public class PredicateMap : IRelationConfig
    {
        private string PredicateName;
        public string GetRelationName()
        {
            return PredicateName;
        }

        public void SetRelationName(string relation)
        {
            PredicateName = relation;
        }
    }
}
