using System;
using System.Collections.Generic;
using System.Text;
namespace GraphLoom.Mapper.Configuration
{
    public interface IStatementsConfig : ISourceConfig, IEntityConfig
    {
        List<IRelationConfig> ListRelationConfigs();
        void AddRelationConfig(IRelationConfig config);
    }
}
