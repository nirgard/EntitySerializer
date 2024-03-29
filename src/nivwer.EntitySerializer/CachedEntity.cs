using System.Reflection;
using nivwer.EntitySerializer.Helpers;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class CachedEntity<T>
where T : new()
{
    private readonly IPropertyMapper PropertyMapper;
    private readonly PropertyInfo[] Properties;

    public CachedEntity(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
        Properties = typeof(T).GetProperties();
    }

    public T DeserializeFromMap(T entity, Dictionary<string, object?> map)
    {
        foreach (PropertyInfo property in Properties)
        {
            if (map.TryGetValue(property.Name, out object? value))
            {
                if (TypeHelper.IsSimpleType(property.PropertyType))
                    PropertyMapper.SetPropertyValue(entity, property, value);
                else if (TypeHelper.IsCollection(property.PropertyType))
                    PropertyMapper.SetCollectionPropertyValue(entity, property, value, PropertyMapper);
                else
                    PropertyMapper.SetNestedPropertyValue(entity, property, value, PropertyMapper);
            }
        }

        return entity;
    }

    public Dictionary<string, object?> SerializeToMap(T entity)
    {
        Dictionary<string, object?> map = new Dictionary<string, object?>();

        foreach (PropertyInfo property in Properties)
        {
            string propertyName = property.Name;
            object? propertyValue = property.GetValue(entity);

            map.Add(propertyName, propertyValue);
        }

        return map;
    }
}