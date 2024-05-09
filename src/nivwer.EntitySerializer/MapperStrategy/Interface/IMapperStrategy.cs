namespace nivwer.EntitySerializer.MapperStrategy.Interface;

public interface IMapperStrategy
{
    object? MapValue(Type type, object? value);
    object? UnmapValue(Type type, object? value);
}