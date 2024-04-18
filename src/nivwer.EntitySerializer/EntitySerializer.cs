using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class EntitySerializer : IEntitySerializer
{
    private readonly IPropertyMapper PropertyMapper;

    public EntitySerializer()
    {
        PropertyMapper = new PropertyMapper();
    }

    public EntitySerializer(IPropertyMapper propertyMapper)
    {
        PropertyMapper = propertyMapper;
    }

    public T Deserialize<T>(Dictionary<string, object?> map)
    where T : new()
    {
        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyMapper);
        T entity = cachedEntity.DeserializeFromMap(map);

        return entity;
    }

    public Dictionary<string, object?> Serialize<T>(T entity)
    where T : new()
    {
        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyMapper);
        Dictionary<string, object?> map = cachedEntity.SerializeToMap(entity);

        return map;
    }
}