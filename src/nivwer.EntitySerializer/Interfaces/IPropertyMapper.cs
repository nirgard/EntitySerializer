using System.Reflection;

namespace nivwer.EntitySerializer.Interfaces;

public interface IPropertyMapper
{
    bool UseNestedMapping { get; set; }

    object? MapPropertyValue(PropertyInfo property, object? value);
    object? UnmapPropertyValue(PropertyInfo property, object? value);
}