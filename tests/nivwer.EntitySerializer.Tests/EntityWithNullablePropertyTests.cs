using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntityWithNullablePropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [DataRow("TestString")]
    [DataRow(null)]
    [TestMethod]
    public void Deserialize_ValidMapWithNullableProperties_ReturnsEntity(
        string? nullableProperty)
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "NullableProperty", nullableProperty },
        };

        EntityWithNullableProperty expected = new EntityWithNullableProperty()
        {
            NullableProperty = nullableProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithNullableProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithNullableProperty));
        Assert.AreEqual(expected.NullableProperty, actual.NullableProperty);
    }

    [DataRow("TestString")]
    [DataRow(null)]
    [TestMethod]
    public void Serialize_ValidEntityWithNullableProperties_ReturnsMap(
        string? nullableProperty)
    {
        // -- Arrange ---------------------------------------------------

        EntityWithNullableProperty entity = new EntityWithNullableProperty()
        {
            NullableProperty = nullableProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "NullableProperty", nullableProperty },
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("NullableProperty"));
        Assert.AreEqual(expected["NullableProperty"], actual["NullableProperty"]);
    }
}