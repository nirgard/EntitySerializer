namespace nivwer.EntityMapSerializer.Interfaces;

public interface IPropertyMapper
{
    Dictionary<string, object?> MapPropertiesToDictionary<T>(T entity);
    T MapDictionaryToEntity<T>(Dictionary<string, object?> map) where T : new();
}