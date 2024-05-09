namespace nivwer.EntitySerializer.Interfaces;

/// <summary>
/// Defines the contract for serializing and deserializing entities.
/// </summary>
public interface IEntitySerializer
{
    /// <summary>
    /// Deserializes a map representation of an entity into an actual entity object.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    /// <param name="map">Map containing property names and their corresponding values.</param>
    /// <returns>The deserialized entity.</returns>
    T Deserialize<T>(Dictionary<string, object?> map) where T : new();

    /// <summary>
    /// Serializes an entity object into a map representation.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    /// <param name="entity">The entity to serialize.</param>
    /// <returns>A map containing property names and their corresponding values.</returns>
    Dictionary<string, object?> Serialize<T>(T entity) where T : new();
}