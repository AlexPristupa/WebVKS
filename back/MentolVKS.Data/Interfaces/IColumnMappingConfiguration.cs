namespace MentolVKS.Data.Interfaces
{
    public interface IColumnMappingConfiguration
    {
        void AddMap<T>(IColumnMapping<T> mapping);

        IColumnMapping<T> GetMapping<T>();
    }
}