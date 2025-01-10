using Bookshop.Domain.DTOs.Requests;
using FluentValidation;

namespace Bookshop.Application.Validator.CanalPreco
{
    public class CanalPrecoValidator : AbstractValidator<CanalPrecoRequest>
    {
        public CanalPrecoValidator()
        {
            RuleFor(p => p.CodLivro)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.CodCanal)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Valor)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
