using HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

namespace HomeAutomation.ApplicationTier.Entity.Dtos.v1_0;

public partial class BuildingDto : BaseDto<BuildingDto, Building>
{
    public Guid Id { get; set; }

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Housenumber { get; set; } = null!;
}
