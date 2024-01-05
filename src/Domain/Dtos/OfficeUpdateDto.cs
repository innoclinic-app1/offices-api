using System.Text.Json.Serialization;

namespace Domain.Dtos;

public class OfficeUpdateDto : OfficeDto
{
    [JsonIgnore]
    public new int Id { get; set; }
}

