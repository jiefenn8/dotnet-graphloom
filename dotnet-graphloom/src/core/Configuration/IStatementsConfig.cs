using System.Collections.Generic;
namespace GraphLoom.Mapper.Configuration
{
    public interface IStatementsConfig : ISourceConfig, IEntityConfig
    {
        IDictionary<IRelationConfig, IObjectsConfig> GetRelationObjectConfigs();
        void AddRelationObjectConfig(IRelationConfig relationConfig, IObjectsConfig objectsConfig);

    }
}
