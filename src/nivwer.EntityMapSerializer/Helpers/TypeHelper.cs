using System.Collections;

namespace nivwer.EntityMapSerializer.Helpers;

public static class TypeHelper
{
    public static bool IsSimpleType(Type type)
    {
        return type.IsValueType || type.IsPrimitive || type == typeof(string);
    }

    public static bool IsCollection(Type type)
    {
        string interfaceFullName = typeof(IEnumerable).FullName ?? string.Empty;
        Type? interfaceType = type.GetInterface(interfaceFullName);

        return interfaceType != null;
    }
}