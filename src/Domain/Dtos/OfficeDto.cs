namespace Domain.Dtos;

public class OfficeDto
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public string PhoneNumber { get; set; } = null!;
    
    public AddressDto Address { get; set; } = null!;
}
