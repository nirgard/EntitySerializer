using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

/// <summary>
/// Manages property access and mapping for entity serialization.
/// </summary>
public class PropertyManager : IPropertyManager
{

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyManager"/> class.
    /// </summary>
    /// <param name="propertyAccessor">Optional property accessor.</param>
    /// <param name="propertyMapper">Optional property mapper.</param>
    public PropertyManager(
        IPropertyAccessor? propertyAccessor = null,
        IPropertyMapper? propertyMapper = null
    )
    {
        Accessor = propertyAccessor ?? new PropertyAccessor();
        Mapper = propertyMapper ?? new PropertyMapper(this);
    }

    /// <inheritdoc />
    public IPropertyAccessor Accessor { get; private set; }

    /// <inheritdoc />
    public IPropertyMapper Mapper { get; private set; }

    /// <inheritdoc />
    public void SetPropertyAccessor(IPropertyAccessor accessor)
    {
        Accessor = accessor;
    }

    /// <inheritdoc />
    public void SetPropertyMapper(IPropertyMapper mapper)
    {
        Mapper = mapper;
    }
}