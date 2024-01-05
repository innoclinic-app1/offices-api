namespace Domain.Dtos;

public class AddressDto
{
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Suite { get; set; } = null!;
    public int Floor { get; set; }
    public int Room { get; set; }
}
