#region Usings
using FluentValidation;
using Tekus.Providers.Application.DTOs.Provider;
#endregion

namespace Tekus.Providers.Application.Validators.Provider;

public class ProviderDTOValidator : AbstractValidator<ProviderDTO>
{
    public ProviderDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(70).WithMessage("Name cannot have more than 70 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        When(x => x.CustomFields != null, () =>
        {
            RuleFor(x => x.CustomFields)
                .Must(fields => fields.Any())
                .WithMessage("CustomFields dictionary must not be empty if it is provided.");
        });
    }
}
