using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Bookshop.Infra.Data.Repositories;
using Bookshop.Unit.Infra.Data.Factory;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Repositories
{
    public class LivroRepositoryTest
    {
        private readonly Mock<MainContext> _contextMock;
        private readonly Mock<DbSet<LivroEntity>> _dbSetMock;
        private readonly LivroRepository _livroRepository;

        public LivroRepositoryTest()
        {
            _contextMock = DbContextMockFactory.Create();
            _dbSetMock = new Mock<DbSet<LivroEntity>>();
            _livroRepository = new LivroRepository(_contextMock.Object);
        }


        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var entity = new LivroEntity { Cod = 1, Titulo = "Book 1" };

            _dbSetMock.Setup(m => m.Add(entity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _livroRepository.AddAsync(entity);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity()
        {
            // Arrange
            var entity = new LivroEntity { Cod = 1, Titulo = "Book 1 Teste" };

            _dbSetMock.Setup(m => m.Update(entity));
            _contextMock.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            var result = await _livroRepository.UpdateAsync(entity);

            // Assert
            _contextMock.Verify(c => c.SaveChangesAsync(default), Times.Once);
            Assert.Equal(entity, result);
        }
    }
}