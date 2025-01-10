using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Bookshop.Unit.Infra.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Repositories
{
    public class AutorRepositoryTest
    {
        private readonly Mock<MainContext> _contextMock;
        private readonly Mock<DbSet<AutorEntity>> _dbSetMock;
        private readonly AutorRepository _autorRepository;

        public AutorRepositoryTest()
        {
            _contextMock = DbContextMockFactory.Create();
            _dbSetMock = new Mock<DbSet<AutorEntity>>();
            _contextMock.Setup(c => c.Set<AutorEntity>()).Returns(_dbSetMock.Object);
            _autorRepository = new AutorRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new AutorEntity { Cod = id };

            _dbSetMock.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);

            // Act
            var result = await _autorRepository.GetByIdAsync(id);

            // Assert
            Assert.Equal(entity, result);
        }
    }
}