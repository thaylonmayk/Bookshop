using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Bookshop.Unit.Infra.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Repositories
{
    public class BaseRepositoryTest
    {
        private readonly Mock<MainContext> _contextMock;
        private readonly Mock<DbSet<AssuntoEntity>> _dbSetMock;
        private readonly BaseRepository<AssuntoEntity> _baseRepository;

        public BaseRepositoryTest()
        {
            _contextMock = DbContextMockFactory.Create();
            _dbSetMock = new Mock<DbSet<AssuntoEntity>>();
            _contextMock.Setup(c => c.Set<AssuntoEntity>()).Returns(_dbSetMock.Object);
            _baseRepository = new BaseRepository<AssuntoEntity>(_contextMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new AssuntoEntity { Cod = 1, Descricao = "Test Description" };

            _dbSetMock.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);

            // Act
            var result = await _baseRepository.GetByIdAsync(id);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var entity = new AssuntoEntity { Cod = 0, Descricao = "Test Description" };

            _dbSetMock.Setup(m => m.Add(entity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _baseRepository.AddAsync(entity);

            // Assert
            _dbSetMock.Verify(m => m.Add(entity), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            var entity = new AssuntoEntity { Cod = 1, Descricao = "Test Description" };

            _dbSetMock.Setup(m => m.Update(entity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _baseRepository.UpdateAsync(entity);

            // Assert
            _dbSetMock.Verify(m => m.Update(entity), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }

    }
}