namespace nivwer.EntitySerializer.Interfaces;

public interface IPropertyMapper
{
    Dictionary<string, object?> MapPropertiesToDictionary<T>(T entity);
    object MapDictionaryToEntity(Type entityType, Dictionary<string, object?> map);
}