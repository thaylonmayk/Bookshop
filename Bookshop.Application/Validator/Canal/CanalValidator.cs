using Bookshop.Domain.DTOs.Requests;
using FluentValidation;

namespace Bookshop.Application.Validator.Canal
{
    public class CanalValidator : AbstractValidator<CanalRequest>
    {
        public CanalValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
        }
    }
}
