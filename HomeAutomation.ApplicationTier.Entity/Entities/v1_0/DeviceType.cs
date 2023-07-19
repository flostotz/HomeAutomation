namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class DeviceType
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual Device? Device { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DeviceType type &&
               Id.Equals(type.Id) &&
               Type == type.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Type, Device);
    }
}
