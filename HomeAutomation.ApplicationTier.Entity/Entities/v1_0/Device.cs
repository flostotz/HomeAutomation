namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class Device
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Config { get; set; } = null!;

    public Guid DeviceType { get; set; }

    public virtual DeviceValueHistory? DeviceValueHistory { get; set; }

    public virtual DeviceType IdNavigation { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        return obj is Device device &&
               Id.Equals(device.Id) &&
               Name == device.Name &&
               Config == device.Config &&
               DeviceType.Equals(device.DeviceType);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Config, DeviceType, DeviceValueHistory);
    }
}
