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
    public class CanalPrecoBusinessTest
    {
        private readonly Mock<IBaseRepository<CanalPrecoEntity>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CanalPrecoRequest>> _validatorMock;
        private readonly CanalPrecoBusiness _canalPrecoBusiness;

        public CanalPrecoBusinessTest()
        {
            _repositoryMock = new Mock<IBaseRepository<CanalPrecoEntity>>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<CanalPrecoRequest>>();
            _canalPrecoBusiness = new CanalPrecoBusiness(_repositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new CanalPrecoRequest { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };
            var entity = new CanalPrecoEntity { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };
            var response = new CanalPrecoResponse { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<CanalPrecoEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalPrecoResponse>(entity)).Returns(response);

            // Act
            var result = await _canalPrecoBusiness.AddAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new CanalPrecoRequest { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };
            var entity = new CanalPrecoEntity { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };
            var response = new CanalPrecoResponse { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<CanalPrecoEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalPrecoResponse>(entity)).Returns(response);

            // Act
            var result = await _canalPrecoBusiness.UpdateAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new CanalPrecoEntity { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _canalPrecoBusiness.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new CanalPrecoEntity { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };
            var response = new CanalPrecoResponse { Cod = 1, CodCanal = 1, CodLivro = 1, Valor = 10 };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalPrecoResponse>(entity)).Returns(response);

            // Act
            var result = await _canalPrecoBusiness.GetByIdAsync(id);

            // Assert
            Assert.Equal(response, result);
        }
    }
}