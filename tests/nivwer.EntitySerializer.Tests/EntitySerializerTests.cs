using nivwer.EntitySerializer.Interfaces;
using nivwer.EntitySerializer.Tests.Entities;

namespace nivwer.EntitySerializer.Tests;

[TestClass]
public class EntitySerializerTests
{
    private IEntitySerializer? EntitySerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntitySerializer = new EntitySerializer();
    }

    [DataRow(1, "Jack")]
    [DataRow(2, "Alice")]
    [TestMethod]
    public void DeserializeFromMap_WithValidMap_ReturnsEntity(int id, string username)
    {
        // -- Arrange ---------------------------------------------------

        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "Id", id },
            { "Username", username }
        };

        SimpleUserEntity expected = new SimpleUserEntity() { Id = id, Username = username };

        // -- Act -------------------------------------------------------

        SimpleUserEntity? actual = EntitySerializer?.DeserializeFromMap<SimpleUserEntity>(map);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(SimpleUserEntity));
        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Username, actual.Username);
    }

    [DataRow(1, "Jack")]
    [DataRow(2, "Alice")]
    [TestMethod]
    public void SerializeToMap_WithValidEntity_ReturnsMap(int id, string username)
    {
        // -- Arrange ---------------------------------------------------

        SimpleUserEntity entity = new SimpleUserEntity() { Id = id, Username = username };

        Dictionary<string, object?> expected = new Dictionary<string, object?>()
        {
            { "Id", id },
            { "Username", username }
        };

        // -- Act -------------------------------------------------------

        Dictionary<string, object?>? actual = EntitySerializer?.SerializeToMap(entity);

        // -- Assert ----------------------------------------------------

        Assert.IsNotNull(actual);
        Assert.IsTrue(actual.ContainsKey("Id"));
        Assert.IsTrue(actual.ContainsKey("Username"));
        Assert.AreEqual(expected["Id"], actual["Id"]);
        Assert.AreEqual(expected["Username"], actual["Username"]);
    }
}