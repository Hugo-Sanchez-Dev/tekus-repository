#region Usings
using FluentValidation;
using Tekus.Providers.Application.DTOs.Catalog;
using Tekus.Providers.Application.Validators.Common;
#endregion

namespace Tekus.Providers.Application.Validators.Catalog
{
    public class CatalogDTOValidator : AbstractValidator<CatalogDTO>
    {
        public CatalogDTOValidator()
        {
            Include(new CatalogCommonRulesValidator<CatalogDTO>());
        }
    }
}
