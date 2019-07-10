using GraphLoom.Mapper.Configuration;
using System;

namespace GraphLoom.Mapper
{
    public interface IGraphMapper
    {
        IGenericGraph MapToGraph(IInputSource source, IMapperConfig config);
        Type GetGraphType();
        void StopTask();
    }
}
