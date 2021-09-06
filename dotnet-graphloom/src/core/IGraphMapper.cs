using System;
using GraphLoom.Mapper.Configuration;

namespace GraphLoom.Mapper
{
    public interface IGraphMapper
    {
        IGenericGraph MapToGraph(IInputSource source, IMapperConfig config);
        Type GetGraphType();
        void StopTask();
    }
}
