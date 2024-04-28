using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class EntitySerializer : IEntitySerializer
{
    public SerializationOptions Options;
    private readonly IPropertyMapper PropertyMapper;

    public EntitySerializer(
        IPropertyMapper? propertyMapper = null, bool? useNestedMapping = null)
    {
        Options = new SerializationOptions();
        Options.UseNestedMapping = useNestedMapping ?? Options.UseNestedMapping;

        PropertyMapper = propertyMapper ?? new PropertyMapper();
    }

    public T Deserialize<T>(Dictionary<string, object?> map)
    where T : new()
    {
        T entity;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyMapper);
        entity = cachedEntity.DeserializeFromMap(map, Options.UseNestedMapping);

        return entity;
    }

    public Dictionary<string, object?> Serialize<T>(T entity)
    where T : new()
    {
        Dictionary<string, object?> map;

        CachedEntity<T> cachedEntity = new CachedEntity<T>(PropertyMapper);
        map = cachedEntity.SerializeToMap(entity, Options.UseNestedMapping);

        return map;
    }
}