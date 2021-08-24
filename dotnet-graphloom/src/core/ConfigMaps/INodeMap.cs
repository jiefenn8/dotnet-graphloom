
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    public interface INodeMap
    {
        INode GenerateNodeTerm(IEntity entity);
    }
}
