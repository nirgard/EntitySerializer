using System.Reflection;
using nivwer.EntityMapSerializer.Helpers;
using nivwer.EntityMapSerializer.Interfaces;

namespace nivwer.EntityMapSerializer;

public class PropertyMapper : IPropertyMapper
{
  
    public T MapDictionaryToEntity<T>(Dictionary<string, object?> map)
    where T : new()
    {
        T entity = new();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (map.TryGetValue(property.Name, out object? value))
            {
                if (TypeHelper.IsSimpleType(property.PropertyType))
                {
                    property.SetValue(entity, value);
                }
            }
        }

        return entity;
    }

    public Dictionary<string, object?> MapPropertiesToDictionary<T>(T entity)
    {
        Dictionary<string, object?> map = new();

        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string propertyName = property.Name;
            object? propertyValue = property.GetValue(entity);

            map.Add(propertyName, propertyValue);
        }

        return map;
    }
}
