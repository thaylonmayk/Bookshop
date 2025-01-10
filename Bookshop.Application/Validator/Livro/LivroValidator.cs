using Bookshop.Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Application.Validator.Livro
{
    public class LivroValidator : AbstractValidator<LivroRequest>
    {
        public LivroValidator() 
        {
            RuleFor(p => p.Titulo)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
            RuleFor(p => p.Editora)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
            RuleFor(p => p.Edicao)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.AnoPublicacao)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(4).WithMessage("{PropertyName} must not exceed 4 characters.");
        }
    }
}
