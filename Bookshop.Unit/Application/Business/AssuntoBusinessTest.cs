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
    public class AssuntoBusinessTest
    {
        private readonly Mock<IAssuntoRepository> _assuntoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<AssuntoRequest>> _validatorMock;
        private readonly AssuntoBusiness _assuntoBusiness;

        public AssuntoBusinessTest()
        {
            _assuntoRepositoryMock = new Mock<IAssuntoRepository>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<AssuntoRequest>>();
            _assuntoBusiness = new AssuntoBusiness(_assuntoRepositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetByDescricaoAsync_ShouldReturnAssuntoResponse_WhenAssuntoExists()
        {
            // Arrange
            var descricao = "Test Description";
            var assuntoEntity = new AssuntoEntity { Descricao = descricao };
            var assuntoResponse = new AssuntoResponse { Descricao = descricao };

            _assuntoRepositoryMock.Setup(repo => repo.GetByDescricaoAsync(descricao)).ReturnsAsync(assuntoEntity);
            _mapperMock.Setup(mapper => mapper.Map<AssuntoResponse>(assuntoEntity)).Returns(assuntoResponse);

            // Act
            var result = await _assuntoBusiness.GetByDescricaoAsync(descricao);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(descricao, result.Descricao);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnExistingAssuntoResponse_WhenAssuntoAlreadyExists()
        {
            // Arrange
            var descricao = "Test Description";
            var assuntoRequest = new AssuntoRequest { Descricao = descricao };
            var assuntoResponse = new AssuntoResponse { Descricao = descricao };

            _assuntoRepositoryMock.Setup(repo => repo.GetByDescricaoAsync(descricao)).ReturnsAsync(new AssuntoEntity { Descricao = descricao });
            _mapperMock.Setup(mapper => mapper.Map<AssuntoResponse>(It.IsAny<AssuntoEntity>())).Returns(assuntoResponse);

            // Act
            var result = await _assuntoBusiness.AddAsync(assuntoRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(descricao, result.Descricao);
        }

        [Fact]
        public async Task AddAsync_ShouldAddNewAssunto_WhenAssuntoDoesNotExist()
        {
            // Arrange
            var descricao = "New Description";
            var assuntoRequest = new AssuntoRequest { Cod = 0, Descricao = descricao };
            var assuntoEntity = new AssuntoEntity { Cod = 0, Descricao = descricao };
            var assuntoResponse = new AssuntoResponse { Cod = 0, Descricao = descricao };

            _assuntoRepositoryMock.Setup(repo => repo.GetByDescricaoAsync(descricao)).ReturnsAsync((AssuntoEntity)null);
            _mapperMock.Setup(mapper => mapper.Map<AssuntoEntity>(assuntoRequest)).Returns(assuntoEntity);
            _mapperMock.Setup(mapper => mapper.Map<AssuntoResponse>(assuntoEntity)).Returns(assuntoResponse);
            _assuntoRepositoryMock.Setup(repo => repo.AddAsync(assuntoEntity)).ReturnsAsync(assuntoEntity);
            _validatorMock.Setup(validator => validator.ValidateAsync(assuntoRequest, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            // Act
            var result = await _assuntoBusiness.AddAsync(assuntoRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(descricao, result.Descricao);
        }
    }
}