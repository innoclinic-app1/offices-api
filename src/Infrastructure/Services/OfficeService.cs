using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OfficeService : IOfficeService
{
    private IMapper Mapper { get; }
    private IOfficeRepository Repository { get; }

    public OfficeService(IOfficeRepository repository, IMapper mapper)
    {
        Mapper = mapper;
        Repository = repository;
    }
    
    public async Task<OfficeDto> GetByIdAsync(int id)
    {
        var office = await Repository.GetByIdAsync(id);
        
        return Mapper.Map<OfficeDto>(office);
    }

    public async Task<OfficeDto> CreateAsync(OfficeCreateDto officeCreateDto)
    {
        var office = Mapper.Map<Office>(officeCreateDto);

        await Repository.InsertAsync(office);

        return Mapper.Map<OfficeDto>(office);
    }

    public async Task<OfficeDto> UpdateAsync(int id, OfficeUpdateDto officeUpdateDto)
    {
        var office = Mapper.Map<Office>(officeUpdateDto);

        await Repository.UpdateAsync(id, office);
        
        return Mapper.Map<OfficeDto>(office);
    }

    public async Task<OfficeDto> ChangeStatusAsync(int id, bool newStatus)
    {
        var office = await Repository.ChangeStatusAsync(id, newStatus);

        return Mapper.Map<OfficeDto>(office);
    }

    public async void RemoveAsync(int id)
    {
        await Repository.DeleteAsync(id);
    }

    public async Task<ICollection<OfficeDto>> GetAllAsync(int skip, int take)
    {
        var offices = await Repository.GetAllAsync(skip, take);
        var officesDto = Mapper.Map<ICollection<OfficeDto>>(offices);

        return officesDto;
    }
}
