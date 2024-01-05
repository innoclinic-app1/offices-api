namespace Domain.Entities;

public class AddressLocal
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Room { get; set; }

    public Office Office { get; set; } = null!;
}
