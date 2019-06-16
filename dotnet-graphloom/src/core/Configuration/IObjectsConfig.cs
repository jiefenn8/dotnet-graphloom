using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.Configuration
{
    public interface IObjectsConfig
    {
        string GetSourceName();
        void SetSourceName(string source);
    }
}
