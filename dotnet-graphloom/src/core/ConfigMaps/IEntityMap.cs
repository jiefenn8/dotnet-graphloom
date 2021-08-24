
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    public interface IEntityMap
    {
        string getIdName();
        ISourceResult GetSource();
        IUriNode GenerateEntityTerm(IEntity entity);
        IGraph GenerateClassTerms(IUriNode term);
        bool HasNodePairs();
        IGraph GenerateNodeTerms(IUriNode term, IEntity entity);
        IGraph GenerateRefNodeTerms(IUriNode term, IInputSource source);
    }
}
