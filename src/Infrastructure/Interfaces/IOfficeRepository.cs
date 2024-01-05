using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IOfficeRepository
{
    public Task<Office> GetByIdAsync(int id);
    public Task InsertAsync(Office newOffice);
    public Task<Office> UpdateAsync(int id, Office updatedOffice);
    public Task<Office> ChangeStatusAsync(int id, bool newStatus);
    public Task DeleteAsync(int id);
    public Task<List<Office>> GetAllAsync(int skip, int take);
}
