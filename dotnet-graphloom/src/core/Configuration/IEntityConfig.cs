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
