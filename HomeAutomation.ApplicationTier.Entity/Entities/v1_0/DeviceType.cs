﻿namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class DeviceType
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual Device? Device { get; set; }
}
