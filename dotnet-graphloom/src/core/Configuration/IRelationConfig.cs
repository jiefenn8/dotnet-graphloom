using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.Configuration
{
    public interface IRelationConfig : IObjectsConfig
    {
        string GetRelationName();
        void SetRelationName(string relation);
    }
}
