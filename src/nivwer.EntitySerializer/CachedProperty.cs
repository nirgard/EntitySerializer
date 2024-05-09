using System.Reflection;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class CachedProperty
{
    private readonly PropertyInfo Property;

    private readonly IPropertyAccessor PropertyAccessor;
    private readonly IPropertyMapper PropertyMapper;

    public CachedProperty(PropertyInfo property, IPropertyManager propertyManager)
    {
        Property = property;

        PropertyAccessor = propertyManager.Accessor;
        PropertyMapper = propertyManager.Mapper;
    }

    public string GetKey()
    {
        if (PropertyAccessor.HasSerializablePropertyKeyAttribute(Property))
            return PropertyAccessor.GetSerializablePropertyKey(Property);

        return GetName();
    }

    public string GetName()
    {
        return PropertyAccessor.GetPropertyName(Property);
    }

    public object? GetValue<T>(T entity)
    {
        object? propertyValue = PropertyAccessor.GetPropertyValue(entity, Property);
        return PropertyMapper.MapPropertyValue(Property, propertyValue);
    }

    public void SetValue<T>(T entity, object? value)
    {
        object? propertyValue = PropertyMapper.UnmapPropertyValue(Property, value);
        PropertyAccessor.SetPropertyValue(entity, Property, propertyValue);
    }
}