using GraphLoom.Mapper.src.Core.InputSource;

namespace GraphLoom.Mapper.Core.InputSource
{
    public interface IEntityReference
    {
        PayloadType GetPayloadType();
        string GetPayload();
        string GetIteratorDef();
        void SetProperty(string key, string value);
        string GetProperty(string key);

    }
}
