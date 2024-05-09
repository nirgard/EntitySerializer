using System.Reflection;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

/// <summary>
/// Represents a cached property for efficient serialization and deserialization.
/// </summary>
public class CachedProperty
{
    private readonly PropertyInfo Property;

    private readonly IPropertyAccessor PropertyAccessor;
    private readonly IPropertyMapper PropertyMapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CachedProperty"/> class.
    /// </summary>
    /// <param name="property">The property to cache.</param>
    /// <param name="propertyManager">The property manager.</param>
    public CachedProperty(PropertyInfo property, IPropertyManager propertyManager)
    {
        Property = property;

        PropertyAccessor = propertyManager.Accessor;
        PropertyMapper = propertyManager.Mapper;
    }

    /// <summary>
    /// Gets the key for the property.
    /// </summary>
    /// <returns>The key (property name or custom key) for serialization.</returns>
    public string GetKey()
    {
        if (PropertyAccessor.HasSerializablePropertyKeyAttribute(Property))
            return PropertyAccessor.GetSerializablePropertyKey(Property);

        return GetName();
    }

    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    /// <returns>The property name.</returns>
    public string GetName()
    {
        return PropertyAccessor.GetPropertyName(Property);
    }

    /// <summary>
    /// Gets the value of the property from an entity.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    /// <param name="entity">The entity instance.</param>
    /// <returns>The property value.</returns>
    public object? GetValue<T>(T entity)
    {
        object? propertyValue = PropertyAccessor.GetPropertyValue(entity, Property);
        return PropertyMapper.MapPropertyValue(Property, propertyValue);
    }

    /// <summary>
    /// Sets the value of the property on an entity.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    /// <param name="entity">The entity instance.</param>
    /// <param name="value">The value to set.</param>
    public void SetValue<T>(T entity, object? value)
    {
        object? propertyValue = PropertyMapper.UnmapPropertyValue(Property, value);
        PropertyAccessor.SetPropertyValue(entity, Property, propertyValue);
    }
}