#region Usings
using FluentValidation;
using Tekus.Providers.Application.DTOs.Catalog;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Validators.Common;
#endregion

namespace Tekus.Providers.Application.Validators.Provider
{
    public class UpdateCatalogDTOValidator : AbstractValidator<CatalogDTO>
    {
        public UpdateCatalogDTOValidator()
        {
            Include(new CatalogCommonRulesValidator<CatalogDTO>());
        }
    }
}
