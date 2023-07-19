using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Api.Dtos.v1_0;

public partial class RoomDto : BaseDto<RoomDto, Room>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Building { get; set; }

    public virtual Building IdNavigation { get; set; } = null!;
}
