using MentolVKS.Data.Interfaces;

namespace MentolVKS.Data.EF.Configuration.ColumnMapping
{
    public class PropertyMap: IPropertyMap
    {
        public string ColumnName { get; private set; }

        public string PropertyName { get; }

        public PropertyMap(string propertyName) : this(propertyName, propertyName)
        {
        }

        public PropertyMap(string propertyName, string columnName)
        {
            PropertyName = propertyName;
            ColumnName = columnName;
        }

        public void ToColumn(string columnName, bool quot = true)
        {
            ColumnName = columnName;
        }
    }
}
