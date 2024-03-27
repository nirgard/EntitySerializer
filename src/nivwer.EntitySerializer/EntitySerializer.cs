using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class EntitySerializer : IEntitySerializer
{
    private readonly IPropertyMapper PropertyMapper;

    public EntitySerializer()
    {
        PropertyMapper = new PropertyMapper();
    }

    public T DeserializeFromMap<T>(Dictionary<string, object?> map) 
    where T : new()
    {
        object Entity = PropertyMapper.MapDictionaryToEntity(typeof(T), map);
        return (T)Entity;
    }

    public Dictionary<string, object?> SerializeToMap<T>(T entity)
    {
        var map = PropertyMapper.MapPropertiesToDictionary(entity);
        return map;
    }
}