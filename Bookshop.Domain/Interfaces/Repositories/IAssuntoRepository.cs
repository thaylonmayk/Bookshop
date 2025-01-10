using Bookshop.Domain.Entities;

namespace Bookshop.Domain.Interfaces.Repositories
{
    public interface IAssuntoRepository : IBaseRepository<AssuntoEntity>
    {
        Task<AssuntoEntity> GetByDescricaoAsync(string descricao);
    }
}
