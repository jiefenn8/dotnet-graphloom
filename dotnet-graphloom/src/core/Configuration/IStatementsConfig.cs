using System;
using System.Collections.Generic;
using System.Text;
namespace GraphLoom.Mapper.Configuration
{
    public interface IStatementsConfig : ISourceConfig, IEntityConfig
    {
        IDictionary<IRelationConfig, IObjectsConfig> GetRelationObjectConfigs();
        void AddRelationObjectConfig(IRelationConfig relationConfig, IObjectsConfig objectsConfig);

    }
}
