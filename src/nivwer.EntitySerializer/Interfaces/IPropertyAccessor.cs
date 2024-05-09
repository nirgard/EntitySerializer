using System.Reflection;

namespace nivwer.EntitySerializer.Interfaces;

/// <summary>
/// Defines the contract for accessing and manipulating property values during entity serialization.
/// </summary>
public interface IPropertyAccessor
{
    /// <summary>
    /// Gets the name of the property.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <returns>The name of the property.</returns>
    string GetPropertyName(PropertyInfo property);

    /// <summary>
    /// Gets the value of a property from an entity object.
    /// </summary>
    /// <param name="entity">The entity object.</param>
    /// <param name="property">The property information.</param>
    /// <returns>The value of the property.</returns>
    object? GetPropertyValue(object? entity, PropertyInfo property);

    /// <summary>
    /// Sets the value of a property on an entity object.
    /// </summary>
    /// <param name="entity">The entity object.</param>
    /// <param name="property">The property information.</param>
    /// <param name="value">The value to set.</param>
    void SetPropertyValue(object? entity, PropertyInfo property, object? value);

    /// <summary>
    /// Checks if a property has the <see cref="SerializablePropertyAttribute"/>.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <returns>True if the property has the attribute; otherwise, false.</returns>
    bool HasSerializablePropertyAttribute(PropertyInfo property);

    /// <summary>
    /// Checks if a property has the <see cref="SerializablePropertyKeyAttribute"/>.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <returns>True if the property has the attribute; otherwise, false.</returns>
    bool HasSerializablePropertyKeyAttribute(PropertyInfo property);

    /// <summary>
    /// Gets the serializable property key for a property.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <returns>The serializable property key (or property name if not specified).</returns>
    string GetSerializablePropertyKey(PropertyInfo property);
}