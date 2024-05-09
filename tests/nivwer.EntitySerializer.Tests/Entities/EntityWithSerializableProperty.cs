using nivwer.EntitySerializer.Attributes;

namespace nivwer.EntitySerializer.Tests.Entities;

public class EntityWithSerializableProperty
{
    [SerializableProperty]
    public string SerializableProperty  { get; set; } = string.Empty;
    public string NonSerializableProperty   { get; set; } = string.Empty;
}   