namespace HomeAutomation.ApplicationTier.Entity.Entities.v1_0;

public partial class Building
{
    public Guid Id { get; set; }

    public string State { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Housenumber { get; set; } = null!;

    public virtual Room? Room { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Building building &&
               Id.Equals(building.Id) &&
               State == building.State &&
               ZipCode == building.ZipCode &&
               Street == building.Street &&
               Housenumber == building.Housenumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, State, ZipCode, Street, Housenumber);
    }
}
