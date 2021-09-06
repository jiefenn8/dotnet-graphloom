
using System.Collections.Generic;
using GraphLoom.Mapper.Core.InputSource;
using VDS.RDF;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    public interface IPropertyMap
    {
        IUriNode GenerateEntityTerm(IEntity entity);
        List<IUriNode> ListEntityClasses();
    }
}
