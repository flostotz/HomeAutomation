namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class Device
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Config { get; set; } = null!;

    public Guid DeviceType { get; set; }

    public virtual DeviceValueHistory? DeviceValueHistory { get; set; }

    public virtual DeviceType IdNavigation { get; set; } = null!;
}
