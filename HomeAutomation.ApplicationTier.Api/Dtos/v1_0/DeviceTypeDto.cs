using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Api.Dtos.v1_0;

public partial class DeviceTypeDto : BaseDto<DeviceTypeDto, DeviceType>
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual Device? Device { get; set; }
}
