using nivwer.EntitySerializer.Attributes;

namespace nivwer.EntitySerializer.Tests.Entities;

public class EntityWithPropertyWithKeyAttribute
{
    [SerializablePropertyKey("PropertyWithKeyAttribute")]
    public string StringProperty { get; set; } = string.Empty;
}