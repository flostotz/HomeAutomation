namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class Room
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Building { get; set; }

    public virtual Building IdNavigation { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        return obj is Room room &&
               Id.Equals(room.Id) &&
               Name == room.Name &&
               Building.Equals(room.Building);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Building);
    }
}
