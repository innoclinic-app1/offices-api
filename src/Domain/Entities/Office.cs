namespace Domain.Entities;

public class Office
{
    public int Id { get; set; }
    public int AddressLocalId { get; set; }
    public int AddressGlobalId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public string PhoneNumber { get; set; } = null!;
    
    public AddressLocal AddressLocal { get; set; } = null!;
    public AddressGlobal AddressGlobal { get; set; } = null!;
}