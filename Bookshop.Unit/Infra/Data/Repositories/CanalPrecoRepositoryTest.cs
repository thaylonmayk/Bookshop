using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Bookshop.Unit.Infra.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Repositories
{
    public class CanalPrecoRepositoryTest
    {
        private readonly Mock<MainContext> _contextMock;
        private readonly Mock<DbSet<CanalPrecoEntity>> _dbSetMock;
        private readonly CanalPrecoRepository _canalPrecoRepository;

        public CanalPrecoRepositoryTest()
        {
            _contextMock = DbContextMockFactory.Create();

            _dbSetMock = new Mock<DbSet<CanalPrecoEntity>>();
            _contextMock.Setup(c => c.Set<CanalPrecoEntity>()).Returns(_dbSetMock.Object);
            _canalPrecoRepository = new CanalPrecoRepository(_contextMock.Object);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenIdIsValid()
        {
            // Arrange
            var id = 1;
            var entity = new CanalPrecoEntity { Cod = 1, CodLivro = 1, CodCanal = 1, Valor = 10.0m };

            _dbSetMock.Setup(m => m.FindAsync(id)).ReturnsAsync(entity);

            // Act
            var result = await _canalPrecoRepository.GetByIdAsync(id);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var entity = new CanalPrecoEntity { Cod = 1, CodLivro = 2, CodCanal = 1, Valor = 20.0m };

            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _canalPrecoRepository.AddAsync(entity);

            // Assert
            _dbSetMock.Verify(m => m.Add(entity), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            var entity = new CanalPrecoEntity();

            _dbSetMock.Setup(m => m.Update(entity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _canalPrecoRepository.UpdateAsync(entity);

            // Assert
            _dbSetMock.Verify(m => m.Update(entity), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }
    }
}