namespace nivwer.EntitySerializer.MapperStrategy.Interface;

/// <summary>
/// Defines the contract for mapping and unmapping property values during entity serialization.
/// </summary>
public interface IMapperStrategy
{
    /// <summary>
    /// Maps a value based on the specified type.
    /// </summary>
    /// <param name="type">The target type.</param>
    /// <param name="value">The original value.</param>
    /// <returns>The mapped value.</returns>
    object? MapValue(Type type, object? value);

    /// <summary>
    /// Unmaps a value based on the specified type.
    /// </summary>
    /// <param name="type">The target type.</param>
    /// <param name="value">The mapped value.</param>
    /// <returns>The original value.</returns>
    object? UnmapValue(Type type, object? value);
}