namespace nivwer.EntityMapSerializer.Interfaces;

public interface IEntityMapSerializer
{
    Dictionary<string, object?> SerializeToMap<T>(T entity);
    T DeserializeFromMap<T>(Dictionary<string, object?> map) where T : new();
}