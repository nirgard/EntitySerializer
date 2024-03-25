using nivwer.EntityMapSerializer.Interfaces;

namespace nivwer.EntityMapSerializer;

public class EntityMapSerializer : IEntityMapSerializer
{
    private readonly IPropertyMapper PropertyMapper;

    public EntityMapSerializer()
    {
        PropertyMapper = new PropertyMapper();
    }

    public T DeserializeFromMap<T>(Dictionary<string, object?> map) 
    where T : new()
    {
        T entity = PropertyMapper.MapDictionaryToEntity<T>(map);
        return entity;
    }

    public Dictionary<string, object?> SerializeToMap<T>(T entity)
    {
        var map = PropertyMapper.MapPropertiesToDictionary(entity);
        return map;
    }
}