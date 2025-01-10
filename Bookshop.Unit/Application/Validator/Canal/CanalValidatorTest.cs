using Bookshop.Application.Validator.Canal;
using Bookshop.Domain.DTOs.Requests;
using FluentValidation.TestHelper;

namespace Bookshop.Unit.Application.Validator.Canal
{
    public class CanalValidatorTest
    {
        private readonly CanalValidator _validator;

        public CanalValidatorTest()
        {
            _validator = new CanalValidator();
        }

        [Fact]
        public void Should_HaveError_When_NomeIsEmpty()
        {
            // Arrange
            var model = new CanalRequest { Nome = string.Empty };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome is required.");
        }

        [Fact]
        public void Should_HaveError_When_NomeExceedsMaxLength()
        {
            // Arrange
            var model = new CanalRequest { Nome = new string('a', 41) };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Nome)
                .WithErrorMessage("Nome must not exceed 40 characters.");
        }

        [Fact]
        public void Should_NotHaveError_When_NomeIsValid()
        {
            // Arrange
            var model = new CanalRequest { Nome = "Valid Name" };

            // Act & Assert
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Nome);
        }
    }
}