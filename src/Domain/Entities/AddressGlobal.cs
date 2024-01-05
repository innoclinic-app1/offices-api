namespace Domain.Entities;

public class AddressGlobal
{
    public int Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Suite { get; set; } = null!;

    public ICollection<Office> Offices { get; set; } = new List<Office>();
}