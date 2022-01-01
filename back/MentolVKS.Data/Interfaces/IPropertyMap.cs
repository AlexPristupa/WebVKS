namespace MentolVKS.Data.Interfaces
{
    public interface IPropertyMap
    {
        string ColumnName { get; }

        string PropertyName { get; }

        void ToColumn(string columnName, bool quot = true);
    }
}