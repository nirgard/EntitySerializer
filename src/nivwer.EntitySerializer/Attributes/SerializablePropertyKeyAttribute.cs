namespace nivwer.EntitySerializer.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SerializablePropertyKeyAttribute : Attribute
{
    public SerializablePropertyKeyAttribute(string key)
    {
        Key = key;
    }

    public string Key { get; private set; }
}