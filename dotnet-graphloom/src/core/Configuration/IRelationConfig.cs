using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.Configuration
{
    public interface IRelationConfig
    {
        string GetRelationName();
        void SetRelationName(string relation);
    }
}
