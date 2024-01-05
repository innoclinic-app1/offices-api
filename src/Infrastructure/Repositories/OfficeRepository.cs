using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OfficeRepository(DataContext context) : IOfficeRepository
{
    private IQueryable<Office> OfficesWithAddress { get; } = context.Offices
        .Include(o => o.AddressLocal)
        .Include(o => o.AddressGlobal);

    public async Task<Office> GetByIdAsync(int id)
    {
        try
        {
            return await OfficesWithAddress.FirstAsync(o => o.Id == id);
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException(nameof(Office), id);
        }
    }

    public async Task InsertAsync(Office newOffice)
    {
        await CheckPhoneNumberExistsAsync(newOffice.PhoneNumber);
        
        await context.Offices.AddAsync(newOffice);

        await context.SaveChangesAsync();
    }

    public async Task<Office> UpdateAsync(int id, Office updatedOffice)
    {
        var office = await GetByIdAsync(id);
        
        if (office.PhoneNumber != updatedOffice.PhoneNumber)
        {
            await CheckPhoneNumberExistsAsync(updatedOffice.PhoneNumber);
        }
        
        context.Entry(office).CurrentValues.SetValues(updatedOffice);
        context.Entry(office.AddressLocal).CurrentValues.SetValues(updatedOffice.AddressLocal);
        context.Entry(office.AddressGlobal).CurrentValues.SetValues(updatedOffice.AddressGlobal);
        
        await context.SaveChangesAsync();
        
        return office;
    }

    public async Task<Office> ChangeStatusAsync(int id, bool newStatus)
    {
        var office = await GetByIdAsync(id);
        
        office.IsActive = newStatus;
        
        await context.SaveChangesAsync();
        
        return office;
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var office = await GetByIdAsync(id);
        
            context.Offices.Remove(office);
            await context.SaveChangesAsync(); 
        }
        catch (InvalidOperationException)
        {
            throw new NotFoundException(nameof(Office), id);
        }
    }
    
    public async Task<List<Office>> GetAllAsync(int skip, int take)
    {
        return await OfficesWithAddress.Skip(skip).Take(take).ToListAsync();
    }
    
    private async Task CheckPhoneNumberExistsAsync(string phoneNumber)
    {
        var isExistPhoneNumber = await context.Offices
            .AnyAsync(o => o.PhoneNumber == phoneNumber);

        if (isExistPhoneNumber)
        {
            throw new AlreadyExistsException("Office with this phone number already exists");
        }
    }
}
