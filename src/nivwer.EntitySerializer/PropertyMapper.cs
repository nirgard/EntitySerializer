using System.Reflection;
using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.MapperStrategy;
using nivwer.EntitySerializer.MapperStrategy.Interface;

namespace nivwer.EntitySerializer;

public class PropertyMapper : IPropertyMapper
{
    private readonly IMapperStrategy RecursiveMapperStrategy;

    public PropertyMapper()
    {
        RecursiveMapperStrategy = new RecursiveMapperStrategy(this);
    }

    public PropertyMapper(IMapperStrategy recursiveMapperStrategy)
    {
        RecursiveMapperStrategy = recursiveMapperStrategy;
    }

    public string GetPropertyName(object? entity, PropertyInfo property)
    {
        return property.Name;
    }

    public object? GetPropertyValue(object? entity, PropertyInfo property)
    {
        object? value = null;

        try
        {
            value = property.GetValue(entity);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is MethodAccessException)
        {
            string message = $"Error getting property value: {ex.Message}";
            Console.WriteLine(message);
            Console.WriteLine(ex.StackTrace);
        }

        return value;
    }

    public void SetPropertyValue(object? entity, PropertyInfo property, object? value)
    {
        try
        {
            property.SetValue(entity, value);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is MethodAccessException)
        {
            string message = $"Error setting property value: {ex.Message}";
            Console.WriteLine(message);
            Console.WriteLine(ex.StackTrace);
        }
    }
    
    public object? MapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        object? mappedProperty = RecursiveMapperStrategy.MapValue(propertyType, value);

        return mappedProperty;
    }

    public object? UnmapPropertyValue(PropertyInfo property, object? value)
    {
        Type propertyType = property.PropertyType;
        object? UnmappedProperty = RecursiveMapperStrategy.UnmapValue(propertyType, value);

        return UnmappedProperty;
    }
}
