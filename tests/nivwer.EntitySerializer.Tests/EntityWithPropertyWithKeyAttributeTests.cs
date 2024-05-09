using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

/// <summary>
/// Contains unit tests for deserialization and serialization of entities with properties marked with key attributes.
/// </summary>
[TestClass]
public class EntityWithPropertyWithKeyAttributeTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    /// <summary>
    /// Tests deserialization of a valid map with a property marked with a key attribute.
    /// </summary>
    [TestMethod]
    public void Deserialize_ValidMapWithPropertyWithKeyAttribute_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        string stringProperty = "TestString";

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "PropertyWithKeyAttribute", stringProperty },
        };

        var expected = new EntityWithPropertyWithKeyAttribute()
        {
            StringProperty = stringProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithPropertyWithKeyAttribute>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithPropertyWithKeyAttribute));
        Assert.AreEqual(expected.StringProperty, actual.StringProperty);
    }

    /// <summary>
    /// Tests serialization of a valid entity with a property marked with a key attribute.
    /// </summary>
    [TestMethod]
    public void Serialize_ValidEntityWithPropertyWithKeyAttribute_ReturnsMap()
    {
        // -- Arrange ---------------------------------------------------

        string stringProperty = "TestString";

        var entity = new EntityWithPropertyWithKeyAttribute()
        {
            StringProperty = stringProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "PropertyWithKeyAttribute", stringProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("PropertyWithKeyAttribute"));
        Assert.AreEqual(
            expected["PropertyWithKeyAttribute"], actual["PropertyWithKeyAttribute"]);
    }
}