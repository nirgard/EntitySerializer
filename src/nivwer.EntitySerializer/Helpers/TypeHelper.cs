namespace nivwer.EntitySerializer.Helpers;

/// <summary>
/// Provides helper methods for type-related checks.
/// </summary>
public static class TypeHelper
{
    public static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive;
    }

    public static bool IsString(Type type)
    {
        return type == typeof(string);
    }

    public static bool IsDateTime(Type type)
    {
        return type == typeof(DateTime);
    }

    public static bool IsBasicValueType(Type type)
    {
        return IsPrimitive(type) || type.IsEnum || IsString(type) || IsDateTime(type);
    }

    public static bool IsEntity(Type type)
    {
        bool IsClass = type.IsClass;

        return IsClass && !IsBasicValueType(type) && !IsCollection(type) && !IsDictionary(type);
    }

    public static bool IsCollection(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.IsArray;
        }

        return type.GetGenericTypeDefinition() == typeof(List<>);
    }

    public static bool IsDictionary(Type type)
    {
        bool IsGenericType = type.IsGenericType;

        if (!IsGenericType)
        {
            return false;
        }

        return type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }
}