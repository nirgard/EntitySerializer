using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntityWithEntityPropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [TestMethod]
    public void Deserialize_ValidFlatMapWithEntityProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        EntityWithBasicProperty entityProperty = new EntityWithBasicProperty
        {
            IntegerProperty = 0,
            StringProperty = "TestString"
        };

        Dictionary<string, object?> flatMap = new Dictionary<string, object?> 
        {
            { "EntityProperty", entityProperty }
        };

        EntityWithEntityProperty expected = new EntityWithEntityProperty
        { 
            EntityProperty = entityProperty 
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithEntityProperty>(flatMap);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithEntityProperty));

        Assert.IsNotNull(actual.EntityProperty);
        Assert.IsInstanceOfType(actual.EntityProperty, typeof(EntityWithBasicProperty));
        
        Assert.AreEqual(
            expected.EntityProperty.IntegerProperty, actual.EntityProperty.IntegerProperty);
        Assert.AreEqual(
            expected.EntityProperty.StringProperty, actual.EntityProperty.StringProperty);
    }

    [TestMethod]
    public void Deserialize_ValidNestedMapWithEntityProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        EntityWithBasicProperty entityProperty = new EntityWithBasicProperty
        {
            IntegerProperty = 0,
            StringProperty = "TestString"
        };

        Dictionary<string, object?> nestedMap = new Dictionary<string, object?>
        {
            { "EntityProperty", EntitySerializer?.Serialize(entityProperty) }
        };

        EntityWithEntityProperty expected = new EntityWithEntityProperty
        {
            EntityProperty = entityProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithEntityProperty>(nestedMap);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithEntityProperty));

        Assert.IsNotNull(actual.EntityProperty);
        Assert.IsInstanceOfType(actual.EntityProperty, typeof(EntityWithBasicProperty));

        Assert.AreEqual(
            expected.EntityProperty.IntegerProperty, actual.EntityProperty.IntegerProperty);
        Assert.AreEqual(
            expected.EntityProperty.StringProperty, actual.EntityProperty.StringProperty);
    }

    [TestMethod]
    public void Serialize_ValidEntityWithEntityProperty_ReturnsFlatMap()
    {
        // -- Arrange ---------------------------------------------------

        EntityWithBasicProperty entityProperty = new EntityWithBasicProperty
        {
            IntegerProperty = 0,
            StringProperty = "TestString"
        };

        EntityWithEntityProperty entity = new EntityWithEntityProperty
        {
            EntityProperty = entityProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>
        {
            { "EntityProperty", entityProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("EntityProperty"));
        
        Assert.IsNotNull(actual["EntityProperty"]);
        Assert.IsInstanceOfType(actual["EntityProperty"], typeof(EntityWithBasicProperty));

        Assert.AreEqual(
            (expected["EntityProperty"] as EntityWithBasicProperty)?.IntegerProperty, 
            (actual["EntityProperty"] as EntityWithBasicProperty)?.IntegerProperty);
        Assert.AreEqual(
            (expected["EntityProperty"] as EntityWithBasicProperty)?.StringProperty, 
            (actual["EntityProperty"] as EntityWithBasicProperty)?.StringProperty);
    }

    [TestMethod]
    public void Serialize_ValidEntityWithEntityProperty_ReturnsNestedMap()
    {
        // -- Arrange ---------------------------------------------------

        EntityWithBasicProperty entityProperty = new EntityWithBasicProperty
        {
            IntegerProperty = 0,
            StringProperty = "TestString"
        };

        EntityWithEntityProperty entity = new EntityWithEntityProperty
        {
            EntityProperty = entityProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>
        {
            { "EntityProperty", EntitySerializer?.Serialize(entityProperty) }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("EntityProperty"));

        Assert.IsNotNull(actual["EntityProperty"]);
        Assert.IsInstanceOfType(actual["EntityProperty"], typeof(Dictionary<string, object?>));

        CollectionAssert.AreEquivalent(
            expected["EntityProperty"] as Dictionary<string, object?>, 
            actual["EntityProperty"] as Dictionary<string, object?>);
    }
}