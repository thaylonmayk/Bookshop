﻿using Bookshop.Domain.DTOs.Requests;
using FluentValidation;

namespace Bookshop.Application.Validator.Assunto
{
    public class AssuntoValidator : AbstractValidator<AssuntoRequest>
    {
        public AssuntoValidator()
        {
            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
        }
    }
}
