
namespace Domain.Entities;

/// <summary>
/// Represents an entity.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Unique identifier for the entity. 
    /// </summary>
    //NOTE:
    //     - Often we prefer use a GUID for this instead of an integer because it is more secure and it is most maintainable.
    public int Id { get; set; }

    //Overridde this method because an entity is EQUAL to another entity if they have the same Id !!!
    public override bool Equals(object? obj)
    {
        return obj is Entity entity &&
               Id == entity.Id;
    }

    //Overridde this method because an entity is EQUAL to another entity if they have the same Id !!!
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
