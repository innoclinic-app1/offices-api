using Domain.Dtos;

namespace Infrastructure.Interfaces;

public interface IOfficeService
{
    public Task<OfficeDto> GetByIdAsync(int id);
    public Task<OfficeDto> CreateAsync(OfficeCreateDto officeCreateDto);
    public Task<OfficeDto> UpdateAsync(int id, OfficeUpdateDto officeUpdateDto);
    public Task<OfficeDto> ChangeStatusAsync(int id, bool newStatus);
    public void RemoveAsync(int id);
    public Task<ICollection<OfficeDto>> GetAllAsync(int skip, int take);
}
