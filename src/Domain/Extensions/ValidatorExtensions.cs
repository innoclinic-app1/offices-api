using FluentValidation;

namespace Domain.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsPhoneNumber<T> (this IRuleBuilder<T, string> rule)
    {
        return rule
            .Matches(@"^[0-9]{7,14}$")
            .WithMessage("This phone number is not valid");
    }
}
