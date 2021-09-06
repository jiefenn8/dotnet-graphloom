
using System;
using GraphLoom.Mapper.Core.InputSource;

namespace GraphLoom.Mapper.Core.ConfigMaps
{
    public interface ISourceResult
    {
        IEntityReference GetEntityReference();
        void ForEachEntity(IInputSource source, Action<IEntity> action)
        {
            if (action is null) throw new ArgumentNullException();
            source.ExecuteEntityQuery(GetEntityReference(), (r) =>
            {
                while (r.HasNext())
                {
                    action.Invoke(r.NextEntity());
                }
            });
        }
    }
}
