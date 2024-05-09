using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class EntitySerializer : IEntitySerializer
{
    private readonly IPropertyManager PropertyManager;

    public EntitySerializer(IPropertyManager? propertyManager = null)
    {
        PropertyManager = propertyManager ?? new PropertyManager();
    }

    public T Deserialize<T>(Dictionary<string, object?> map)
    where T : new()
    {
        T entity;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyManager);
        entity = cachedEntity.DeserializeFromMap(map);

        return entity;
    }

    public Dictionary<string, object?> Serialize<T>(T entity)
    where T : new()
    {
        Dictionary<string, object?> map;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyManager);
        map = cachedEntity.SerializeToMap(entity);

        return map;
    }
}