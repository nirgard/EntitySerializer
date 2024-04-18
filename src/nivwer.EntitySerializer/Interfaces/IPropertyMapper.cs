using System.Reflection;

namespace nivwer.EntitySerializer.Interfaces;

public interface IPropertyMapper
{
    string GetPropertyName(object? entity, PropertyInfo property);
    object? GetPropertyValue(object? entity, PropertyInfo property);
    void SetPropertyValue(object? entity, PropertyInfo property, object? value);
    
    object? MapPropertyValue(PropertyInfo property, object? value);
    object? UnmapPropertyValue(PropertyInfo property, object? value);
}