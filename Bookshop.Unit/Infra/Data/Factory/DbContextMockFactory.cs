using Bookshop.Domain.Entities;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Bookshop.Unit.Infra.Data.Factory
{
    public static class DbContextMockFactory
    {
        public static Mock<MainContext> Create()
        {
            var options = new DbContextOptionsBuilder<MainContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var mockContext = new Mock<MainContext>(options);
            var livroEntities = new List<LivroEntity>
            {
                new LivroEntity { Cod = 1, Titulo = "Book 1" },
                new LivroEntity { Cod = 2, Titulo = "Book 2" }
            };
            var assuntoEntities = new List<AssuntoEntity>
            {
                new AssuntoEntity { Cod = 1, Descricao = "Test Description" },
                new AssuntoEntity { Cod = 2, Descricao = "Another Description" }
            };
            var autorEntities = new List<AutorEntity>
            {
                new AutorEntity { Cod = 1, Nome = "John", Sobrenome = "Doe" },
                new AutorEntity { Cod = 2, Nome = "Jane", Sobrenome = "Doe" }
            };
            var canalPrecoEntities = new List<CanalPrecoEntity>
            {
                new CanalPrecoEntity { Cod = 1, CodLivro = 1, CodCanal = 1, Valor = 10.0m },
                new CanalPrecoEntity { Cod = 2, CodLivro = 2, CodCanal = 2, Valor = 20.0m }
            };
            var canalEntities = new List<CanalEntity>
            {
                new CanalEntity { Cod = 1, Nome = "Canal 1" },
                new CanalEntity { Cod = 2, Nome = "Canal 2" }
            };
            var mockLivroDbSet = CreateDbSetMock(livroEntities);
            var mockAssuntoDbSet = CreateDbSetMock(assuntoEntities);
            var mockAutorDbSet = CreateDbSetMock(autorEntities);
            var mockCanalPrecoDbSet = CreateDbSetMock(canalPrecoEntities);
            var mockCanalDbSet = CreateDbSetMock(canalEntities);

            mockContext.Setup(c => c.Set<LivroEntity>()).Returns(mockLivroDbSet.Object);
            mockContext.Setup(c => c.Livros).Returns(mockLivroDbSet.Object);

            mockContext.Setup(c => c.Set<AssuntoEntity>()).Returns(mockAssuntoDbSet.Object);
            mockContext.Setup(c => c.Assuntos).Returns(mockAssuntoDbSet.Object);

            mockContext.Setup(c => c.Set<AutorEntity>()).Returns(mockAutorDbSet.Object);
            mockContext.Setup(c => c.Autores).Returns(mockAutorDbSet.Object);

            mockContext.Setup(c => c.Set<CanalPrecoEntity>()).Returns(mockCanalPrecoDbSet.Object);
            mockContext.Setup(c => c.CanalPrecos).Returns(mockCanalPrecoDbSet.Object);

            mockContext.Setup(c => c.Set<CanalEntity>()).Returns(mockCanalDbSet.Object);
            mockContext.Setup(c => c.Canais).Returns(mockCanalDbSet.Object);

            return mockContext;
        }

        internal static Mock<MainContext>? Create(List<LivroEntity> livroEntities)
        {
            throw new NotImplementedException();
        }

        private static Mock<DbSet<TEntity>> CreateDbSetMock<TEntity>(List<TEntity> entities) where TEntity : class
        {
            var queryable = entities.AsQueryable();
            var mockDbSet = new Mock<DbSet<TEntity>>();

            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockDbSet;
        }
    }
}
