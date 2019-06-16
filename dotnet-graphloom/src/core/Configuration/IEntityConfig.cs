using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLoom.Mapper.Configuration
{
    public interface IEntityConfig
    {
        string GetTemplate();
        void SetTemplate(string template);
        string GetClassName();
        void SetClassName(string className);
    }
}
