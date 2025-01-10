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
    public class CanalBusinessTest
    {
        private readonly Mock<ICanalRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<CanalRequest>> _validatorMock;
        private readonly CanalBusiness _canalBusiness;

        public CanalBusinessTest()
        {
            _repositoryMock = new Mock<ICanalRepository>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<CanalRequest>>();
            _canalBusiness = new CanalBusiness(_repositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new CanalRequest { /* set properties */ };
            var entity = new CanalEntity { /* set properties */ };
            var response = new CanalResponse { /* set properties */ };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<CanalEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalResponse>(entity)).Returns(response);

            // Act
            var result = await _canalBusiness.AddAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new CanalRequest { /* set properties */ };
            var entity = new CanalEntity { /* set properties */ };
            var response = new CanalResponse { /* set properties */ };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<CanalEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.UpdateAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalResponse>(entity)).Returns(response);

            // Act
            var result = await _canalBusiness.UpdateAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new CanalEntity { /* set properties */ };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _repositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _canalBusiness.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new CanalEntity { /* set properties */ };
            var response = new CanalResponse { /* set properties */ };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<CanalResponse>(entity)).Returns(response);

            // Act
            var result = await _canalBusiness.GetByIdAsync(id);

            // Assert
            Assert.Equal(response, result);
        }
    }
}