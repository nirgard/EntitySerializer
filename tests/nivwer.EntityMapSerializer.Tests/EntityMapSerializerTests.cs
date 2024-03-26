using nivwer.EntityMapSerializer.Interfaces;
using nivwer.EntityMapSerializer.Tests.Entities;

namespace nivwer.EntityMapSerializer.Tests;

[TestClass]
public class EntityMapSerializerTests
{
    private IEntityMapSerializer? EntityMapSerializer;

    [TestInitialize]
    public void SetUp()
    {
        EntityMapSerializer = new EntityMapSerializer();
    }

    [DataRow(1, "John")]
    [DataRow(2, "Alice")]
    [TestMethod]
    public void DeserializeFromMap_WithValidMap_ReturnsEntity(int id, string username)
    {
        // Arrange
        Dictionary<string, object?> map = new Dictionary<string, object?>()
        {
            { "Id", id },
            { "Username", username }
        };

        SimpleUserEntity expected = new SimpleUserEntity() { Id = id, Username = username };

        // Act
        SimpleUserEntity? actual = EntityMapSerializer?.DeserializeFromMap<SimpleUserEntity>(map);

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(actual, typeof(SimpleUserEntity));
        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Username, actual.Username);
    }
}