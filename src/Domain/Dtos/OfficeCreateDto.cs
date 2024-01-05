using System.Text.Json.Serialization;

namespace Domain.Dtos;

public class OfficeCreateDto : OfficeDto
{
    [JsonIgnore]
    public new int Id { get; set; }
}
