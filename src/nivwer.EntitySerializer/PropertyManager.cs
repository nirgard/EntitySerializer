using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class PropertyManager : IPropertyManager
{
    public PropertyManager(
        IPropertyAccessor? propertyAccessor = null,
        IPropertyMapper? propertyMapper = null
    )
    {
        Accessor = propertyAccessor ?? new PropertyAccessor();
        Mapper = propertyMapper ?? new PropertyMapper(this);
    }

    public IPropertyAccessor Accessor { get; private set; }
    public IPropertyMapper Mapper { get; private set; }

    public void SetPropertyAccessor(IPropertyAccessor accessor)
    {
        Accessor = accessor;
    }

    public void SetPropertyMapper(IPropertyMapper mapper)
    {
        Mapper = mapper;
    }
}