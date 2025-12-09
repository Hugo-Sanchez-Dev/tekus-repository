#region Usings
using FluentValidation;
using Tekus.Providers.Application.DTOs.Provider;
using Tekus.Providers.Application.Validators.Common;
#endregion

namespace Tekus.Providers.Application.Validators.Provider
{
    public class UpdateProviderDTOValidator : AbstractValidator<ProviderDTO>
    {
        public UpdateProviderDTOValidator()
        {
            Include(new ProviderCommonRulesValidator<ProviderDTO>());
        }
    }
}
