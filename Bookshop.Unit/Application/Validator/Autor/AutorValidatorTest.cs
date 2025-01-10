using Bookshop.Application.Validator.Autor;
using Bookshop.Domain.DTOs.Requests;
using FluentValidation.TestHelper;

namespace Bookshop.Unit.Application.Validator.Autor
{
    public class AutorValidatorTest
    {
        private readonly AutorValidator _validator;

        public AutorValidatorTest()
        {
            _validator = new AutorValidator();
        }

        [Fact]
        public void Should_HaveError_When_NomeIsEmpty()
        {
            // Arrange
            var model = new AutorRequest { Nome = string.Empty };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome is required.");
        }

        [Fact]
        public void Should_HaveError_When_NomeExceedsMaxLength()
        {
            // Arrange
            var model = new AutorRequest { Nome = new string('a', 41) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome must not exceed 40 characters.");
        }

        [Fact]
        public void Should_NotHaveError_When_NomeIsValid()
        {
            // Arrange
            var model = new AutorRequest { Nome = "Valid Name" };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }
    }
}