namespace nivwer.EntitySerializer.Interfaces;

public interface IEntitySerializer
{
    Dictionary<string, object?> SerializeToMap<T>(T entity);
    T DeserializeFromMap<T>(Dictionary<string, object?> map) where T : new();
}