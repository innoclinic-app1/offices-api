using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Domain.Profiles;

public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<Office, OfficeDto>()
            .ForPath(
                dto => dto.Address.Country,
                expression => expression.MapFrom(s => s.AddressGlobal.Country))
            .ForPath(
                dto => dto.Address.City,
                expression => expression.MapFrom(s => s.AddressGlobal.City))
            .ForPath(
                dto => dto.Address.Floor,
                expression => expression.MapFrom(s => s.AddressLocal.Floor))
            .ForPath(
                dto => dto.Address.Suite,
                expression => expression.MapFrom(s => s.AddressGlobal.Suite))
            .ForPath(
                dto => dto.Address.Room,
                expression => expression.MapFrom(s => s.AddressLocal.Room))
            .ForPath(
                dto => dto.Address.Street,
                expression => expression.MapFrom(s => s.AddressGlobal.Street))
            .ReverseMap();

        CreateMap<OfficeDto, OfficeUpdateDto>().ReverseMap();
    }
}
