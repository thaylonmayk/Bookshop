using Bookshop.Application.Validator.Assunto;
using Bookshop.Domain.DTOs.Requests;
using FluentValidation.TestHelper;

namespace Bookshop.Unit.Application.Validator.Assunto
{
    public class AssuntoValidatorTest
    {
        private readonly AssuntoValidator _validator;

        public AssuntoValidatorTest()
        {
            _validator = new AssuntoValidator();
        }

        [Fact]
        public void Should_HaveError_When_DescricaoIsEmpty()
        {
            // Arrange
            var model = new AssuntoRequest { Descricao = string.Empty };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("Descricao is required.");
        }

        [Fact]
        public void Should_HaveError_When_DescricaoExceedsMaxLength()
        {
            // Arrange
            var model = new AssuntoRequest { Descricao = new string('a', 41) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Descricao)
                .WithErrorMessage("Descricao must not exceed 40 characters.");
        }

        [Fact]
        public void Should_NotHaveError_When_DescricaoIsValid()
        {
            // Arrange
            var model = new AssuntoRequest { Descricao = "Valid Description" };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Descricao);
        }
    }
}