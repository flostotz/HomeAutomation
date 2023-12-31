﻿namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class DeviceValueHistory
{
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Value { get; set; }

    public Guid Device { get; set; }

    public virtual Device IdNavigation { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        return obj is DeviceValueHistory history &&
               Id.Equals(history.Id) &&
               Timestamp == history.Timestamp &&
               Value == history.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Timestamp, Value);
    }
}
