using HomeAutomation.ApplicationTier.Entity.Dtos;
using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;

public partial class DeviceValueHistoryDto : BaseDto<DeviceValueHistoryDto, DeviceValueHistory>
{
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Value { get; set; }

    public Guid Device { get; set; }

    public virtual Device IdNavigation { get; set; } = null!;
}
