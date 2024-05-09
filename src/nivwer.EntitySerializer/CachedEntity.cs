using System.Reflection;
using nivwer.EntitySerializer.Attributes;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class CachedEntity<T>
where T : new()
{
    private readonly IPropertyManager PropertyManager;

    private readonly Type EntityType;
    private readonly List<CachedProperty> SerializableProperties;

    public CachedEntity(IPropertyManager propertyManager)
    {
        PropertyManager = propertyManager;

        EntityType = typeof(T);
        SerializableProperties = GetSerializableProperties(EntityType);
    }

    public CachedEntity(IPropertyManager propertyManager, Type propertyType)
    {
        PropertyManager = propertyManager;

        EntityType = propertyType;
        SerializableProperties = GetSerializableProperties(EntityType);
    }

    private List<CachedProperty> GetSerializableProperties(Type entityType)
    {
        List<CachedProperty> serializableProperties = [];
        PropertyInfo[] properties = entityType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (SerializeAllProperties(properties)
                || PropertyManager.Accessor.HasSerializablePropertyAttribute(property))
            {
                CachedProperty cachedProperty = new(property, PropertyManager);
                serializableProperties.Add(cachedProperty);
            }
        }

        return serializableProperties;
    }

    public T DeserializeFromMap(Dictionary<string, object?> map)
    {
        T entity = (T)Activator.CreateInstance(EntityType)!;

        foreach (CachedProperty cachedProperty in SerializableProperties)
        {
            string key = cachedProperty.GetKey();

            if (map.TryGetValue(key, out object? value))
                cachedProperty.SetValue(entity, value);
        }

        return entity;
    }

    public Dictionary<string, object?> SerializeToMap(T entity)
    {
        Dictionary<string, object?> map = new Dictionary<string, object?>();

        foreach (CachedProperty cachedProperty in SerializableProperties)
        {
            string key = cachedProperty.GetKey();
            object? value = cachedProperty.GetValue(entity);

            map.Add(key, value);
        }

        return map;
    }

    private bool SerializeAllProperties(PropertyInfo[] properties)
    {
        return !properties.Any(property => 
            PropertyManager.Accessor.HasSerializablePropertyAttribute(property));
    }
}