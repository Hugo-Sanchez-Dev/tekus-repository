#region Usings
using FluentValidation;
#endregion


namespace Tekus.Providers.Application.Validators.Common
{
    public class ProviderCommonRulesValidator<T> : AbstractValidator<T>
        where T : class, new()
    {
        public ProviderCommonRulesValidator()
        {
            // Regla para Nit
            RuleFor(x => (string)typeof(T).GetProperty("Nit").GetValue(x))
                .NotEmpty().WithMessage("NIT is required.")
                .MaximumLength(11).WithMessage("NIT cannot have more than 11 characters.")
                .WithName("Nit");

            // Regla para Name
            RuleFor(x => (string)typeof(T).GetProperty("Name").GetValue(x))
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(70).WithMessage("Name cannot have more than 70 characters.")
                .WithName("Name");

            // Regla para Email
            RuleFor(x => (string)typeof(T).GetProperty("Email").GetValue(x))
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.")
                .WithName("Email");
        }
    }
}