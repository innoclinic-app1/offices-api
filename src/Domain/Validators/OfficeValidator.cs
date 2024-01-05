using Domain.Dtos;
using Domain.Extensions;
using FluentValidation;

namespace Domain.Validators;

public class OfficeValidator<T> : AbstractValidator<T> where T : OfficeDto
{
    public OfficeValidator()
    {
        RuleFor(o => o.Capacity)
            .GreaterThan(0)
            .LessThan(5)
            .NotNull();
        
        RuleFor(o => o.IsActive)
            .NotNull();
        
        RuleFor(o => o.PhoneNumber).IsPhoneNumber().NotNull();
        
        RuleFor(o => o.Address).ChildRules(address =>
        {
            address.RuleFor(a => a.Country).NotEmpty();

            address.RuleFor(a => a.City).NotEmpty();

            address.RuleFor(a => a.Street).NotEmpty();

            address.RuleFor(a => a.Suite).NotEmpty();
            
            address.RuleFor(a => a.Floor)
                .NotEmpty()
                .InclusiveBetween(1, 1000);

            address.RuleFor(a => a.Room)
                .NotEmpty()
                .InclusiveBetween(1, 1000);
        });
    }
}
