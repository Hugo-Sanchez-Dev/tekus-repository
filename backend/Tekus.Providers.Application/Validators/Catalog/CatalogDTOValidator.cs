#region Usings
using FluentValidation;
using Tekus.Providers.Application.DTOs.Catalog;
#endregion

namespace Tekus.Providers.Application.Validators.Catalog
{
    public class CatalogDTOValidator : AbstractValidator<CatalogDTO>
    {
        public CatalogDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

            RuleFor(x => x.HourlyRate)
                .GreaterThan(0).WithMessage("El costo por hora debe ser mayor a 0.");
        }
    }
}
