using Bookshop.Domain.Entities;
using Bookshop.Domain.Interfaces.Repositories;
using Bookshop.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Infra.Data.Repositories
{
    public class CanalPrecoRepository : BaseRepository<CanalPrecoEntity>, ICanalPrecoRepository
    {
        public CanalPrecoRepository(MainContext context) : base(context) { }

        public override async Task<CanalPrecoEntity> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(l => l.Canais)
                .Include(l => l.Livros)
                .FirstOrDefaultAsync(l => l.Cod == id);
        }
    }
}
