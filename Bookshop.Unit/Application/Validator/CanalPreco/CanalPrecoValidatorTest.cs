using Bookshop.Application.Validator.CanalPreco;
using Bookshop.Domain.DTOs.Requests;
using FluentValidation.TestHelper;

namespace Bookshop.Unit.Application.Validator.CanalPreco
{
    public class CanalPrecoValidatorTest
    {
        private readonly CanalPrecoValidator _validator;

        public CanalPrecoValidatorTest()
        {
            _validator = new CanalPrecoValidator();
        }

        [Fact]
        public void Should_HaveError_When_CodLivroIsEmpty()
        {
            // Arrange
            var model = new CanalPrecoRequest { Valor = 10, CodCanal = 10 };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CodLivro)
                .WithErrorMessage("Cod Livro is required.");
        }

        [Fact]
        public void Should_HaveError_When_CodCanalIsEmpty()
        {
            // Arrange
            var model = new CanalPrecoRequest { Valor = 10, CodLivro = 10 };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.CodCanal)
                .WithErrorMessage("Cod Canal is required.");
        }

        [Fact]
        public void Should_HaveError_When_ValorIsEmpty()
        {
            // Arrange
            var model = new CanalPrecoRequest { Valor = 0 };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Valor)
                .WithErrorMessage("Valor is required.");
        }

        [Fact]
        public void Should_NotHaveError_When_AllFieldsAreValid()
        {
            // Arrange
            var model = new CanalPrecoRequest { CodLivro = 1, CodCanal = 1, Valor = 100 };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.CodLivro);
            result.ShouldNotHaveValidationErrorFor(x => x.CodCanal);
            result.ShouldNotHaveValidationErrorFor(x => x.Valor);
        }
    }
}