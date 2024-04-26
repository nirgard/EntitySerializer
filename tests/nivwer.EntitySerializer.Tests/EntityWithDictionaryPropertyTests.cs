using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntityWithDictionaryPropertyTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [TestMethod]
    public void Deserialize_ValidMapWithDictionaryProperty_ReturnsEntity()
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, int> dictionaryProperty = new Dictionary<string, int>
        {
            { "Key1", 1 },
            { "Key2", 2 },
            { "Key3", 3 }
        };

        Dictionary<string, object?> map = new Dictionary<string, object?>
        {
            { "DictionaryProperty", dictionaryProperty }
        };

        EntityWithDictionaryProperty expected = new EntityWithDictionaryProperty
        {
            DictionaryProperty = dictionaryProperty
        };

        // -- Act -------------------------------------------------------

        var actual = EntitySerializer?.Deserialize<EntityWithDictionaryProperty>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(EntityWithDictionaryProperty));
        CollectionAssert.AreEquivalent(expected.DictionaryProperty, actual.DictionaryProperty);
    }

    [TestMethod]
    public void Serialize_ValidEntityWithDictionaryProperty_ReturnsMap()
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, int> dictionaryProperty = new Dictionary<string, int>
        {
            { "Key1", 1 },
            { "Key2", 2 },
            { "Key3", 3 }
        };

        EntityWithDictionaryProperty entity = new EntityWithDictionaryProperty
        {
            DictionaryProperty = dictionaryProperty
        };

        Dictionary<string, object?> expected = new Dictionary<string, object?>
        {
            { "DictionaryProperty", dictionaryProperty }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.Serialize(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("DictionaryProperty"));
        CollectionAssert.AreEquivalent(
            expected["DictionaryProperty"] as Dictionary<string, int>, 
            actual["DictionaryProperty"] as Dictionary<string, int>);
    }
}