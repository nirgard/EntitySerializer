using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

/// <summary>
/// Provides serialization and deserialization functionality for entities.
/// </summary>
public class EntitySerializer : IEntitySerializer
{
    private readonly IPropertyManager PropertyManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntitySerializer"/> class.
    /// </summary>
    /// <param name="propertyManager">Optional property manager.</param>
    public EntitySerializer(IPropertyManager? propertyManager = null)
    {
        PropertyManager = propertyManager ?? new PropertyManager();
    }

    /// <inheritdoc />
    public T Deserialize<T>(Dictionary<string, object?> map)
    where T : new()
    {
        T entity;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyManager);
        entity = cachedEntity.DeserializeFromMap(map);

        return entity;
    }

    /// <inheritdoc />
    public Dictionary<string, object?> Serialize<T>(T entity)
    where T : new()
    {
        Dictionary<string, object?> map;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyManager);
        map = cachedEntity.SerializeToMap(entity);

        return map;
    }
}