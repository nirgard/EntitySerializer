using System.Reflection;

namespace nivwer.EntitySerializer.Interfaces;

public interface IPropertyAccessor
{   
    string GetPropertyName(PropertyInfo property);
    object? GetPropertyValue(object? entity, PropertyInfo property);
    void SetPropertyValue(object? entity, PropertyInfo property, object? value);

    bool HasSerializablePropertyAttribute(PropertyInfo property);
    bool HasSerializablePropertyKeyAttribute(PropertyInfo property);
    string GetSerializablePropertyKey(PropertyInfo property);
}