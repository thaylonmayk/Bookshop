﻿using Bookshop.Domain.DTOs.Requests;
using FluentValidation;

namespace Bookshop.Application.Validator.Autor
{
    public class AutorValidator : AbstractValidator<AutorRequest>
    {
        public AutorValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed 40 characters.");
        }
    }
}
