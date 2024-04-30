using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntityWithObjectPropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [DataRow("TestString")]
    [DataRow(1)]
    [TestMethod]
    public void Deserialize_ValidMapWithObjectProperty_ReturnsEntity(
        object objectProperty)
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "ObjectProperty", objectProperty }
        };

        EntityWithObjectProperty expected = new EntityWithObjectProperty()
        {
            ObjectProperty = objectProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithObjectProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithObjectProperty));
        Assert.AreEqual(expected.ObjectProperty, actual.ObjectProperty);
    }


    [DataRow("TestString")]
    [DataRow(1)]
    [TestMethod]
    public void Serialize_ValidEntityWithObjectProperty_ReturnsMap(
        object objectProperty)
    {
        // -- Arrange ---------------------------------------------------

        EntityWithObjectProperty entity = new EntityWithObjectProperty()
        {
            ObjectProperty = objectProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "ObjectProperty", objectProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("ObjectProperty"));
        Assert.AreEqual(expected["ObjectProperty"], actual["ObjectProperty"]);
    }
}