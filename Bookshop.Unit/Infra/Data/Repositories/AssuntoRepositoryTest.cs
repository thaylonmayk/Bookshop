using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Bookshop.Unit.Infra.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Repositories
{
    public class AssuntoRepositoryTest
    {
        private readonly Mock<MainContext> _contextMock;
        private readonly Mock<DbSet<AssuntoEntity>> _dbSetMock;
        private readonly AssuntoRepository _assuntoRepository;

        public AssuntoRepositoryTest()
        {

            _contextMock = DbContextMockFactory.Create();
            _dbSetMock = new Mock<DbSet<AssuntoEntity>>();
            _contextMock.Setup(c => c.Set<AssuntoEntity>()).Returns(_dbSetMock.Object);
            _assuntoRepository = new AssuntoRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new AssuntoEntity { Cod = id };

            _dbSetMock.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);

            // Act
            var result = await _assuntoRepository.GetByIdAsync(id);

            // Assert
            Assert.Equal(entity, result);
        }
    }
}