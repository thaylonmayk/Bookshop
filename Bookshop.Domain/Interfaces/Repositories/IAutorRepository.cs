using Bookshop.Domain.Entities;

namespace Bookshop.Domain.Interfaces.Repositories
{
    public interface IAutorRepository : IBaseRepository<AutorEntity>
    {
        Task<AutorEntity> GetByNomeAndSobrenomeAsync(string nome, string sobrenome);
    }
}
