namespace nivwer.EntitySerializer.Interfaces;

/// <summary>
/// Defines the contract for managing property access and mapping during entity serialization.
/// </summary>
public interface IPropertyManager
{
    /// <summary>
    /// Gets the property accessor.
    /// </summary>
    IPropertyAccessor Accessor { get; }

    /// <summary>
    /// Gets the property mapper.
    /// </summary>
    IPropertyMapper Mapper { get; }

    /// <summary>
    /// Sets a custom property accessor.
    /// </summary>
    /// <param name="accessor">The custom property accessor.</param>
    void SetPropertyAccessor(IPropertyAccessor accessor);

    /// <summary>
    /// Sets a custom property mapper.
    /// </summary>
    /// <param name="mapper">The custom property mapper.</param>
    void SetPropertyMapper(IPropertyMapper mapper);
}