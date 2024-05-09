using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

/// <summary>
/// Contains unit tests for deserialization and serialization of entities with basic properties.
/// </summary>
[TestClass]
public class EntityWithBasicPropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    /// <summary>
    /// Tests deserialization of a valid map with basic properties.
    /// </summary>
    [DataRow(1, "TestString1")]
    [DataRow(2, "TestString2")]
    [DataRow(3, "TestString3")]
    [TestMethod]
    public void Deserialize_ValidMapWithBasicProperties_ReturnsEntity(
        int integerProperty, string stringProperty)
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "IntegerProperty", integerProperty },
            { "StringProperty", stringProperty },
        };

        EntityWithBasicProperty expected = new EntityWithBasicProperty()
        {
            IntegerProperty = integerProperty,
            StringProperty = stringProperty
        };

        // -- Act -------------------------------------------------------

        EntityWithBasicProperty? actual = EntitySerializer?.Deserialize<EntityWithBasicProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithBasicProperty));
        Assert.AreEqual(expected.IntegerProperty, actual.IntegerProperty);
        Assert.AreEqual(expected.StringProperty, actual.StringProperty);
    }

    /// <summary>
    /// Tests serialization of a valid entity with basic properties.
    /// </summary>
    [DataRow(1, "TestString1")]
    [DataRow(2, "TestString2")]
    [DataRow(3, "TestString3")]
    [TestMethod]
    public void Serialize_ValidEntityWithBasicProperties_ReturnsMap(
        int integerProperty, string stringProperty)
    {
        // -- Arrange ---------------------------------------------------

        EntityWithBasicProperty entity = new EntityWithBasicProperty()
        {
            IntegerProperty = integerProperty,
            StringProperty = stringProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "IntegerProperty", integerProperty },
            { "StringProperty", stringProperty },
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("IntegerProperty"));
        Assert.IsTrue(actual.ContainsKey("StringProperty"));
        Assert.AreEqual(expected["IntegerProperty"], actual["IntegerProperty"]);
        Assert.AreEqual(expected["StringProperty"], actual["StringProperty"]);
    }
}