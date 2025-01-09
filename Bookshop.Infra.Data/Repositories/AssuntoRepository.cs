using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Infra.Data.Repositories
{
    public class AssuntoRepository : BaseRepository<AssuntoEntity>, IAssuntoRepository
    {
        public AssuntoRepository(MainContext context) : base(context) { }

        public async Task<AssuntoEntity> GetByDescricaoAsync(string descricao)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Descricao == descricao);
        }
    }

}
