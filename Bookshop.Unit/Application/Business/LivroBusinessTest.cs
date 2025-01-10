using AutoMapper;
using Bookshop.Application.Business;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace Bookshop.Unit.Application.Business
{
    public class LivroBusinessTest
    {
        private readonly Mock<ILivroRepository> _livroRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<LivroRequest>> _validatorMock;
        private readonly Mock<IAutorBusiness> _autorBusinessMock;
        private readonly Mock<IAssuntoBusiness> _assuntoBusinessMock;
        private readonly LivroBusiness _livroBusiness;

        public LivroBusinessTest()
        {
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<LivroRequest>>();
            _autorBusinessMock = new Mock<IAutorBusiness>();
            _assuntoBusinessMock = new Mock<IAssuntoBusiness>();
            _livroBusiness = new LivroBusiness(
                _livroRepositoryMock.Object,
                _mapperMock.Object,
                _validatorMock.Object,
                _autorBusinessMock.Object,
                _assuntoBusinessMock.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var request = new LivroRequest();
            var validationResult = new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Property", "Error message")
            });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _livroBusiness.AddAsync(request));
            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public async Task AddAsync_ShouldAddLivro_WhenValidationSucceeds()
        {
            // Arrange
            var request = new LivroRequest
            {
                Autores = new List<int> { 1 },
                Assuntos = new List<int> { 1 }
            };
            var validationResult = new FluentValidation.Results.ValidationResult();
            var livroEntity = new LivroEntity();
            var livroResponse = new LivroResponse();

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);
            _autorBusinessMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new AutorResponse());
            _assuntoBusinessMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new AssuntoResponse());
            _mapperMock.Setup(m => m.Map<LivroEntity>(request)).Returns(livroEntity);
            _livroRepositoryMock.Setup(r => r.AddAsync(livroEntity)).ReturnsAsync(livroEntity);
            _mapperMock.Setup(m => m.Map<LivroResponse>(livroEntity)).Returns(livroResponse);

            // Act
            var result = await _livroBusiness.AddAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livroResponse, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowValidationException_WhenValidationFails()
        {
            // Arrange
            var request = new LivroRequest();
            var validationResult = new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Property", "Error message")
            });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _livroBusiness.UpdateAsync(request));
            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateLivro_WhenValidationSucceeds()
        {
            // Arrange
            var request = new LivroRequest
            {
                Autores = new List<int> { 1 },
                Assuntos = new List<int> { 1 }
            };
            var validationResult = new FluentValidation.Results.ValidationResult();
            var livroEntity = new LivroEntity();
            var livroResponse = new LivroResponse();

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);
            _autorBusinessMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new AutorResponse());
            _assuntoBusinessMock.Setup(a => a.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new AssuntoResponse());
            _mapperMock.Setup(m => m.Map<LivroEntity>(request)).Returns(livroEntity);
            _livroRepositoryMock.Setup(r => r.UpdateAsync(livroEntity)).ReturnsAsync(livroEntity);
            _mapperMock.Setup(m => m.Map<LivroResponse>(livroEntity)).Returns(livroResponse);

            // Act
            var result = await _livroBusiness.UpdateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livroResponse, result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowArgumentException_WhenLivroNotFound()
        {
            // Arrange
            var id = 1;
            _livroRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((LivroEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _livroBusiness.DeleteAsync(id));
            Assert.Equal("Livro não encontrado.", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteLivro_WhenLivroExists()
        {
            // Arrange
            var id = 1;
            var livroEntity = new LivroEntity
            {
                Cod = id,
                LivroAutores = new List<LivroAutorEntity>(),
                LivroAssuntos = new List<LivroAssuntoEntity>()
            };

            _livroRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(livroEntity);
            _livroRepositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _livroBusiness.DeleteAsync(id);

            // Assert
            _livroRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}