using System.Reflection;

namespace nivwer.EntitySerializer.Interfaces;

/// <summary>
/// Defines the contract for mapping and unmapping property values during entity serialization.
/// </summary>
public interface IPropertyMapper
{
    /// <summary>
    /// Gets or sets whether nested mapping should be used.
    /// </summary>
    bool UseNestedMapping { get; set; }

    /// <summary>
    /// Maps a property value based on the specified property information.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <param name="value">The original property value.</param>
    /// <returns>The mapped property value.</returns>
    object? MapPropertyValue(PropertyInfo property, object? value);

    /// <summary>
    /// Unmaps a property value based on the specified property information.
    /// </summary>
    /// <param name="property">The property information.</param>
    /// <param name="value">The mapped property value.</param>
    /// <returns>The original property value.</returns>
    object? UnmapPropertyValue(PropertyInfo property, object? value);
}