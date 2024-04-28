using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntityWithCollectionPropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [TestMethod]
    public void Deserialize_ValidMapWithListProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        List<string> listProperty = new List<string> { "Item1", "Item2", "Item3" };

        Dictionary<string, object?> map = new Dictionary<string, object?>
        {
            { "ListProperty", listProperty }
        };

        EntityWithListProperty expected = new EntityWithListProperty
        {
            ListProperty = listProperty
        };

        // -- Act -------------------------------------------------------

        EntityWithListProperty? actual = EntitySerializer?.Deserialize<EntityWithListProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithListProperty));
        CollectionAssert.AreEqual(expected.ListProperty, actual.ListProperty);
    }

    [TestMethod]
    public void Deserialize_ValidMapWithArrayProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        string[] arrayProperty = new string[] { "Element1", "Element2", "Element3" };

        Dictionary<string, object?> map = new Dictionary<string, object?>
        {
            { "ArrayProperty", arrayProperty }
        };

        EntityWithArrayProperty expected = new EntityWithArrayProperty
        {
            ArrayProperty = arrayProperty
        };

        // -- Act -------------------------------------------------------

        EntityWithArrayProperty? actual = EntitySerializer?.Deserialize<EntityWithArrayProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithArrayProperty));
        CollectionAssert.AreEqual(expected.ArrayProperty, actual.ArrayProperty);
    }

    [TestMethod]
    public void Serialize_ValidEntityWithListProperty_ReturnsMap()
    {
        // -- Arrange ---------------------------------------------------

        List<string> listProperty = new List<string> { "Item1", "Item2", "Item3" };

        EntityWithListProperty entity = new EntityWithListProperty()
        {
            ListProperty = listProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>
        {
            { "ListProperty", listProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("ListProperty"));
        CollectionAssert.AreEqual(
            expected["ListProperty"] as List<string>, actual["ListProperty"] as List<string>);
    }

    [TestMethod]
    public void Serialize_ValidEntityWithArrayProperty_ReturnsMap()
    {
        // -- Arrange ---------------------------------------------------

        string[] arrayProperty = new string[] { "Element1", "Element2", "Element3" };

        EntityWithArrayProperty entity = new EntityWithArrayProperty()
        {
            ArrayProperty = arrayProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>
        {
            { "ArrayProperty", arrayProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("ArrayProperty"));
        CollectionAssert.AreEqual(
            expected["ArrayProperty"] as string[], actual["ArrayProperty"] as string[]);
    }
}