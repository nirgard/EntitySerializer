using System.Reflection;
using nivwer.EntitySerializer.Attributes;
using nivwer.EntitySerializer.Interfaces;

namespace nivwer.EntitySerializer;

public class PropertyAccessor : IPropertyAccessor
{
    public string GetPropertyName(PropertyInfo property)
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
            Console.WriteLine(ex.Message);
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
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

    public bool HasSerializablePropertyAttribute(PropertyInfo property)
    {
        Type attributeType = typeof(SerializablePropertyAttribute);
        return Attribute.IsDefined(property, attributeType);
    }

    public bool HasSerializablePropertyKeyAttribute(PropertyInfo property)
    {
        Type attributeType = typeof(SerializablePropertyKeyAttribute);
        return Attribute.IsDefined(property, attributeType);
    }

    public string GetSerializablePropertyKey(PropertyInfo property)
    {
        Type attributeType = typeof(SerializablePropertyKeyAttribute);

        var attribute = property.GetCustomAttributes(attributeType, false)
            .FirstOrDefault() as SerializablePropertyKeyAttribute;

        return attribute?.Key ?? GetPropertyName(property);
    }
}