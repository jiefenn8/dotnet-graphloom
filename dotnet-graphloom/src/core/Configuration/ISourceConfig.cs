using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.Configuration
{
    public interface ISourceConfig
    {
        string GetSourceName();
        void SetSourceName(string source);
    }
}
