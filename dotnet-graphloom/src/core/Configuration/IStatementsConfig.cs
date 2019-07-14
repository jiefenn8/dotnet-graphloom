using System;
using System.Collections.Generic;
using System.Text;
namespace GraphLoom.Mapper.Configuration
{
    public interface IStatementsConfig : ISourceConfig, IEntityConfig
    {
        IDictionary<IRelationConfig, IObjectsConfig> GetRelationObjectConfigPairs();
        void AddRelationObjectConfigPair(IRelationConfig relationConfig, IObjectsConfig objectsConfig);

    }
}
