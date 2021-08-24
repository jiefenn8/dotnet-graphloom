
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    public interface IRelationMap
    {
        IUriNode GenerateRelationTerm(IEntity entity);
    }
}
