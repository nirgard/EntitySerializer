namespace nivwer.EntitySerializer.Interfaces;

public interface IEntitySerializer
{
    T Deserialize<T>(Dictionary<string, object?> map) where T : new();
    Dictionary<string, object?> Serialize<T>(T entity) where T : new();
}