using AutoMapper;
using Bookshop.Application.Business;
using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.DTOs.Response;
using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace Bookshop.Unit.Application.Business
{
    public class AutorBusinessTest
    {
        private readonly Mock<IAutorRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<AutorRequest>> _validatorMock;
        private readonly AutorBusiness _autorBusiness;

        public AutorBusinessTest()
        {
            _repositoryMock = new Mock<IAutorRepository>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<AutorRequest>>();
            _autorBusiness = new AutorBusiness(_repositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetByNomeAsync_ShouldReturnEntity_WhenNomeAndSobrenomeAreValid()
        {
            // Arrange
            var nome = "John";
            var sobrenome = "Doe";
            var entity = new AutorEntity();
            var response = new AutorResponse();

            _repositoryMock.Setup(r => r.GetByNomeAndSobrenomeAsync(nome, sobrenome)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<AutorResponse>(entity)).Returns(response);

            // Act
            var result = await _autorBusiness.GetByNomeAsync(nome, sobrenome);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new AutorRequest { Nome = "John", Sobrenome = "Doe" };
            var entity = new AutorEntity { };
            var response = new AutorResponse { };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _repositoryMock.Setup(r => r.GetByNomeAndSobrenomeAsync(request.Nome, request.Sobrenome)).ReturnsAsync((AutorEntity)null);
            _mapperMock.Setup(m => m.Map<AutorEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<AutorResponse>(entity)).Returns(response);

            // Act
            var result = await _autorBusiness.AddAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnExistingEntity_WhenEntityAlreadyExists()
        {
            // Arrange
            var request = new AutorRequest { Nome = "John", Sobrenome = "Doe" };
            var existingResponse = new AutorResponse { };

            _repositoryMock.Setup(r => r.GetByNomeAndSobrenomeAsync(request.Nome, request.Sobrenome)).ReturnsAsync(new AutorEntity());
            _mapperMock.Setup(m => m.Map<AutorResponse>(It.IsAny<AutorEntity>())).Returns(existingResponse);

            // Act
            var result = await _autorBusiness.AddAsync(request);

            // Assert
            Assert.Equal(existingResponse, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new AutorRequest { };
            var entity = new AutorEntity { };
            var response = new AutorResponse { };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<AutorEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<AutorResponse>(entity)).Returns(response);

            // Act
            var result = await _autorBusiness.UpdateAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new AutorEntity { Cod = 1, Nome = "nome", Sobrenome = "sobrenome" };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _autorBusiness.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new AutorEntity { };
            var response = new AutorResponse { };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<AutorResponse>(entity)).Returns(response);

            // Act
            var result = await _autorBusiness.GetByIdAsync(id);

            // Assert
            Assert.Equal(response, result);
        }
    }
}