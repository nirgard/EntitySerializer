using System.Reflection;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class CachedEntity<T>
where T : new()
{
    private readonly IPropertyMapper PropertyMapper;
    private readonly Type EntityType;
    private readonly PropertyInfo[] Properties;

    public CachedEntity(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
        EntityType = typeof(T);
        Properties = EntityType.GetProperties();
    }

    public CachedEntity(IPropertyMapper propertyMapper, Type propertyType)
    {
        PropertyMapper = propertyMapper;
        EntityType = propertyType;
        Properties = EntityType.GetProperties();
    }

    public T DeserializeFromMap(
        Dictionary<string, object?> map, bool useNestedMapping = false)
    {
        T entity = (T)Activator.CreateInstance(EntityType)!;

        foreach (PropertyInfo property in Properties)
        {
            string propertyName = PropertyMapper.GetPropertyName(entity, property);

            if (map.TryGetValue(propertyName, out object? value))
            {
                object? propertyValue;

                if (useNestedMapping)
                    propertyValue = PropertyMapper.UnmapPropertyValue(property, value);
                else
                    propertyValue = value;

                PropertyMapper.SetPropertyValue(entity, property, propertyValue);
            }
        }

        return entity;
    }

    public Dictionary<string, object?> SerializeToMap(
        T entity, bool useNestedMapping = false)
    {
        Dictionary<string, object?> map = new Dictionary<string, object?>();

        foreach (PropertyInfo property in Properties)
        {
            string propertyName = PropertyMapper.GetPropertyName(entity, property);
            object? propertyValue = PropertyMapper.GetPropertyValue(entity, property);

            object? value;

            if (useNestedMapping)
                value = PropertyMapper.MapPropertyValue(property, propertyValue);
            else
                value = propertyValue;

            map.Add(propertyName, value);
        }

        return map;
    }
}