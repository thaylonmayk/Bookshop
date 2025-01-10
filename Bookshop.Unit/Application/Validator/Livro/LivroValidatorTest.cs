using Bookshop.Application.Validator.Livro;
using Bookshop.Domain.DTOs.Requests;
using FluentValidation.TestHelper;

namespace Bookshop.Unit.Application.Validator.Livro
{
    public class LivroValidatorTest
    {
        private readonly LivroValidator _validator;

        public LivroValidatorTest()
        {
            _validator = new LivroValidator();
        }

        [Fact]
        public void Should_HaveError_When_TituloIsEmpty()
        {
            // Arrange
            var model = new LivroRequest { AnoPublicacao = "2021", Edicao = 1, Editora = new string('a', 40) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Titulo)
                .WithErrorMessage("Titulo is required.");
        }

        [Fact]
        public void Should_HaveError_When_TituloExceedsMaxLength()
        {
            // Arrange
            var model = new LivroRequest { Titulo = new string('a', 41), AnoPublicacao = "2021", Edicao = 1, Editora = new string('a', 40) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Titulo)
                .WithErrorMessage("Titulo must not exceed 40 characters.");
        }

        [Fact]
        public void Should_HaveError_When_EditoraIsEmpty()
        {
            // Arrange
            var model = new LivroRequest { Titulo = new string('a', 41), AnoPublicacao = "2021", Edicao = 1, };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Editora)
                .WithErrorMessage("Editora is required.");
        }

        [Fact]
        public void Should_HaveError_When_EditoraExceedsMaxLength()
        {
            // Arrange
            var model = new LivroRequest { Titulo = "Valid Title", AnoPublicacao = "2021", Edicao = 1, Editora = new string('a', 41) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Editora)
                .WithErrorMessage("Editora must not exceed 40 characters.");
        }

        [Fact]
        public void Should_HaveError_When_EdicaoIsEmpty()
        {
            // Arrange
            var model = new LivroRequest
            {
                Titulo = "Valid Title",
                Editora = "Valid Editora",
                AnoPublicacao = "2021"
            };
            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Edicao)
                .WithErrorMessage("Edicao is required.");
        }

        [Fact]
        public void Should_HaveError_When_AnoPublicacaoIsEmpty()
        {
            // Arrange
            var model = new LivroRequest { Titulo = new string('a', 41), Edicao = 1, Editora = new string('a', 40) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AnoPublicacao)
                .WithErrorMessage("Ano Publicacao is required.");
        }

        [Fact]
        public void Should_HaveError_When_AnoPublicacaoExceedsMaxLength()
        {
            // Arrange
            var model = new LivroRequest { Titulo = new string('a', 41), AnoPublicacao = "20210", Edicao = 1, Editora = new string('a', 40) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.AnoPublicacao)
                .WithErrorMessage("Ano Publicacao must not exceed 4 characters.");
        }

        [Fact]
        public void Should_NotHaveError_When_AllFieldsAreValid()
        {
            // Arrange
            var model = new LivroRequest
            {
                Titulo = "Valid Title",
                Editora = "Valid Editora",
                Edicao = 1,
                AnoPublicacao = "2021"
            };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Titulo);
            result.ShouldNotHaveValidationErrorFor(x => x.Editora);
            result.ShouldNotHaveValidationErrorFor(x => x.Edicao);
            result.ShouldNotHaveValidationErrorFor(x => x.AnoPublicacao);
        }
    }
}