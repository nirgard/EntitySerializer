using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

/// <summary>
/// Contains unit tests for deserialization and serialization of entities with properties marked with serializable attributes.
/// </summary>
[TestClass]
public class EntityWithSerializablePropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    /// <summary>
    /// Tests deserialization of a valid map with a property marked with a serializable attribute..
    /// </summary>
    [TestMethod]
    public void Deserialize_ValidMapWithSerializableProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        string serializableProperty = "TestSerializableProperty";
        string nonSerializableProperty = "TestNonSerializableProperty";

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "SerializableProperty", serializableProperty },
            { "NonSerializableProperty", nonSerializableProperty }
        };

        EntityWithSerializableProperty expected = new EntityWithSerializableProperty()
        {
            SerializableProperty = serializableProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithSerializableProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithSerializableProperty));
        Assert.AreEqual(expected.SerializableProperty, actual.SerializableProperty);
        Assert.AreEqual(expected.NonSerializableProperty, actual.NonSerializableProperty);
    }

    /// <summary>
    /// Tests serialization of a valid entity with a property marked with a serializable attribute...
    /// </summary>
    [TestMethod]
    public void Serialize_ValidEntityWithSerializableProperty_ReturnsMap()
    {
        // -- Arrange ---------------------------------------------------

        string serializableProperty = "TestSerializableProperty";
        string nonSerializableProperty = "TestNonSerializableProperty";

        EntityWithSerializableProperty entity = new EntityWithSerializableProperty()
        {
            SerializableProperty = serializableProperty,
            NonSerializableProperty = nonSerializableProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "SerializableProperty", serializableProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("SerializableProperty"));
        Assert.IsFalse(actual.ContainsKey("NonSerializableProperty"));
        Assert.AreEqual(expected["SerializableProperty"], actual["SerializableProperty"]);
    }
}