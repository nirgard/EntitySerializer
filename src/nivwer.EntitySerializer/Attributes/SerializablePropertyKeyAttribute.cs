namespace nivwer.EntitySerializer.Attributes;

/// <summary>
/// Marks a property with a custom key for serialization.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SerializablePropertyKeyAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SerializablePropertyKeyAttribute"/> class.
    /// </summary>
    /// <param name="key">The custom key for serialization.</param>
    public SerializablePropertyKeyAttribute(string key)
    {
        Key = key;
    }

    /// <summary>
    /// Gets the custom key for serialization.
    /// </summary>
    public string Key { get; private set; }
}