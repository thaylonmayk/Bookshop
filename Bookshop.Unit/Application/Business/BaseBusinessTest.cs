using AutoMapper;
using Bookshop.Application.Business;
using Bookshop.Domain.Interfaces.Repositories;
using FluentValidation;
using Moq;

namespace Bookshop.Unit.Application.Business
{
    public class BaseBusinessTest
    {
        private readonly Mock<IBaseRepository<TestEntity>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<TestRequest>> _validatorMock;
        private readonly BaseBusiness<TestEntity, TestRequest, TestResponse> _baseBusiness;

        public BaseBusinessTest()
        {
            _repositoryMock = new Mock<IBaseRepository<TestEntity>>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<TestRequest>>();
            _baseBusiness = new BaseBusiness<TestEntity, TestRequest, TestResponse>(_repositoryMock.Object, _mapperMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = new List<TestEntity> { new TestEntity(), new TestEntity() };
            var responses = new List<TestResponse> { new TestResponse(), new TestResponse() };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            _mapperMock.Setup(m => m.Map<IEnumerable<TestResponse>>(entities)).Returns(responses);

            // Act
            var result = await _baseBusiness.GetAllAsync();

            // Assert
            Assert.Equal(responses, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new TestEntity();
            var response = new TestResponse();

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<TestResponse>(entity)).Returns(response);

            // Act
            var result = await _baseBusiness.GetByIdAsync(id);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDefault_WhenEntityNotFound()
        {
            // Arrange
            var id = 1;

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((TestEntity)null);

            // Act
            var result = await _baseBusiness.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity_WhenRequestIsValid()
        {
            // Arrange
            var request = new TestRequest { /* set properties */ };
            var entity = new TestEntity { /* set properties */ };
            var response = new TestResponse { /* set properties */ };

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _mapperMock.Setup(m => m.Map<TestEntity>(request)).Returns(entity);
            _repositoryMock.Setup(r => r.AddAsync(entity)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<TestResponse>(entity)).Returns(response);

            // Act
            var result = await _baseBusiness.AddAsync(request);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowValidationException_WhenRequestIsInvalid()
        {
            // Arrange
            var request = new TestRequest { /* set properties */ };
            var validationResult = new FluentValidation.Results.ValidationResult(new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Property", "Error message")
            });

            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _baseBusiness.AddAsync(request));
        }

        // Additional tests for UpdateAsync and DeleteAsync can be added similarly
    }

    // Dummy classes for testing
    public class TestEntity { }
    public class TestRequest { }
    public class TestResponse { }
}