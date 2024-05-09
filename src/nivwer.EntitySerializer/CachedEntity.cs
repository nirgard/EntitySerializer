using System.Reflection;
using nivwer.EntitySerializer.Attributes;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

/// <summary>
/// Caches information about an entity type for efficient serialization and deserialization.
/// </summary>
/// <typeparam name="T">Type of the entity.</typeparam>
public class CachedEntity<T>
where T : new()
{
    private readonly IPropertyManager PropertyManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="CachedEntity{T}"/> class.
    /// </summary>
    /// <param name="propertyManager">The property manager.</param>
    public CachedEntity(IPropertyManager propertyManager)
    {
        PropertyManager = propertyManager;

        EntityType = typeof(T);
        SerializableProperties = GetSerializableProperties(EntityType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CachedEntity{T}"/> class.
    /// </summary>
    /// <param name="propertyManager">The property manager.</param>
    /// <param name="propertyType">The specific property type.</param>
    public CachedEntity(IPropertyManager propertyManager, Type propertyType)
    {
        PropertyManager = propertyManager;

        EntityType = propertyType;
        SerializableProperties = GetSerializableProperties(EntityType);
    }

    /// <summary>
    /// Gets the type of the entity.
    /// </summary>
    public Type EntityType { get; }

    /// <summary>
    /// Gets the list of cached serializable properties for the entity.
    /// </summary>
    private List<CachedProperty> SerializableProperties { get; }

    /// <summary>
    /// Deserializes a map representation of an entity into an actual entity object.
    /// </summary>
    /// <param name="map">Map containing property names and their corresponding values.</param>
    /// <returns>The deserialized entity.</returns>
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

    /// <summary>
    /// Serializes an entity object into a map representation.
    /// </summary>
    /// <param name="entity">The entity to serialize.</param>
    /// <returns>A map containing property names and their corresponding values.</returns>
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

    private List<CachedProperty> GetSerializableProperties(Type entityType)
    {
        List<CachedProperty> serializableProperties = [];
        PropertyInfo[] properties = entityType.GetProperties();

        // Iterate through the properties of the entity type and add those that need to be serialized
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

    private bool SerializeAllProperties(PropertyInfo[] properties)
    {
        return !properties.Any(property =>
            PropertyManager.Accessor.HasSerializablePropertyAttribute(property));
    }
}